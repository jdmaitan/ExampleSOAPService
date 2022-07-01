using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;

namespace FiscalInfoWebService.Services
{
    public class EncodedPDFReader
    {
        private readonly string PDF_READER_EXE_PATH = $@"{HttpRuntime.AppDomainAppPath}pdftotext.exe";

        public string GetPDFText(string base64EncodedPDF)
        {
            string pdfFileText = string.Empty;

            string tempRootFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempRootFolder);
            string decodedPDFFilePath = GetRandomFilePath(tempRootFolder);
            string pdfTextFilePath = GetRandomFilePath(tempRootFolder);

            try
            {
                DecodePDF(base64EncodedPDF, decodedPDFFilePath);

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = PDF_READER_EXE_PATH,
                    Arguments = $@"-layout ""{decodedPDFFilePath}.pdf"" ""{pdfTextFilePath}.txt""",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (Process process = Process.Start(processStartInfo))
                {
                    process.WaitForExit(1000);

                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                    else
                    {
                        if (process.ExitCode == 0)
                        {
                            pdfFileText = File.ReadAllText($"{pdfTextFilePath}.txt", Encoding.GetEncoding("iso-8859-15"));
                        }
                        else
                        {
                            string stderr = process.StandardError.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                Directory.Delete(tempRootFolder, true);
            }

            return pdfFileText;
        }

        private string GetRandomFilePath(string rootFolderPath)
        {
            return Path.Combine(rootFolderPath, Guid.NewGuid().ToString());
        }

        private void DecodePDF(string base64EncodedPDF, string outputFilePath)
        {
            byte[] bytes = Convert.FromBase64String(base64EncodedPDF);

            string rifFilePath = $"{outputFilePath}.pdf";

            File.WriteAllBytes(rifFilePath, bytes);
        }
    }
}