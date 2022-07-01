using System;
using System.IO;
using System.Web.UI;

namespace FiscalInfoWebService.UI
{
    public partial class PostFiscalInfoTest : Page
    {
        protected void ConvertButton_Click(object sender, EventArgs e)
        {
            BinaryReader binaryReader = new BinaryReader(RIFFileUpload.PostedFile.InputStream);
            byte[] bytes = binaryReader.ReadBytes((int)RIFFileUpload.PostedFile.InputStream.Length);
            string codedFile = Convert.ToBase64String(bytes);


            string serviceResponse = new FiscalInfoService()
                                    .ParseRIFFile(codedFile);

            string[] splitResponse = serviceResponse.Split(new[] { "||" }, StringSplitOptions.None);

            if (splitResponse[0] == "1")
            {
                string[] readParameters = splitResponse[2].Split(new[] { ";;" }, StringSplitOptions.None);
                RIFTextBox.Text = readParameters[0];
                BusinessNameTextBox.Text = readParameters[1];
                AddressTextBox.Text = readParameters[2];
            }
            else
            {
                ResponseLabel.Text = serviceResponse;
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ResponseLabel.Text = new FiscalInfoService()
                                 .PostFiscalInfo(RIFTextBox.Text,
                                                BusinessNameTextBox.Text,
                                                AddressTextBox.Text);
        }
    }
}