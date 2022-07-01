using FiscalInfoWebService.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace FiscalInfoWebService.Services
{
    public class RIFTextParser
    {
        public Taxpayer GetTaxpayer(string rifFileText)
        {
            string rif = string.Empty;
            string businessName = string.Empty;
            string address = string.Empty;
            bool multipleAddressLines = false;

            try
            {
                string[] textLines = rifFileText.Split('\n');

                foreach (string lineText in textLines)
                {
                    if (IsAnEmptyLine(lineText))
                    {
                        continue;
                    }

                    if (IsRIFAndBusinessNameLine(lineText))
                    {
                        rif = GetRIF(lineText);
                        businessName = GetBusinessName(lineText);
                    }

                    #region Address processing
                    //Second line
                    if (!string.IsNullOrEmpty(address) && !multipleAddressLines)
                    {
                        address += $" {lineText.Substring(0, lineText.LastIndexOf("FECHA DE VENCIMIENTO:"))}"; //Must clean expiration date part
                        multipleAddressLines = true;
                        continue;
                    }

                    //Address end verification
                    if (!string.IsNullOrEmpty(address) && multipleAddressLines)
                    {
                        string[] addressEndKeywords = { "contribuyente", "posee", "firmas", "personales" };
                        bool addressEndReached = false;

                        foreach (string keyword in addressEndKeywords)
                        {
                            if (lineText.Contains(keyword)) //Address end reached
                            {
                                addressEndReached = true;
                                address = CleanAddress(address);
                                break;
                            }
                        }

                        if (addressEndReached)
                        {
                            break;
                        }

                        //Third line. Should be enough for address
                        if (lineText != string.Empty)
                        {
                            address += lineText;
                            address = CleanAddress(address);
                            break;
                        }
                    }

                    //First line
                    if (IsFiscalAddressFirstLine(lineText))
                    {
                        address = lineText.Replace("DOMICILIO FISCAL", "");
                    }
                    #endregion
                }

                if (string.IsNullOrEmpty(rif)
                || string.IsNullOrEmpty(businessName)
                || string.IsNullOrEmpty(address))
                {
                    return null;
                }
                else
                {
                    return new Taxpayer
                    {
                        RIF = rif,
                        BusinessName = businessName,
                        Address = address
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        private bool IsAnEmptyLine(string lineText)
        {
            return lineText == "\r";
        }

        private bool IsRIFAndBusinessNameLine(string lineText)
        {
            return lineText.Contains("FECHA DE INSCRIP");
        }

        private string GetRIF(string lineText)
        {
            string[] lineWords = lineText.Split(' ');
            return lineWords[0];
        }

        private string GetBusinessName(string lineText)
        {
            string[] lineWords = lineText.Split(' ');
            string businessName = string.Join(" ", lineWords.Skip(1));
            return businessName.Substring(0, businessName.IndexOf("FECHA")).Trim();
        }

        private bool IsFiscalAddressFirstLine(string lineText)
        {
            return lineText.Contains("DOMICILIO FISCAL");
        }

        private string CleanAddress(string input)
        {
            string output = input.Replace("\r", "");
            output = new Regex("[ ]{2,}", RegexOptions.None).Replace(output, " "); //Cleaning of whitespaces
            output = output.Trim();
            return output;
        }
    }
}