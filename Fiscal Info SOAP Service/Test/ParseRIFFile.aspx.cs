using System;
using System.IO;
using System.Web.UI;

namespace FiscalInfoWebService.UI
{
    public partial class ParseRIFFileTest : Page
    {
        protected void ConvertButton_Click(object sender, EventArgs e)
        {
            BinaryReader binaryReader = new BinaryReader(RIFFileUpload.PostedFile.InputStream);
            byte[] bytes = binaryReader.ReadBytes((int)RIFFileUpload.PostedFile.InputStream.Length);
            EncodedFileTextbox.Text = Convert.ToBase64String(bytes);
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ResponseLabel.Text = new FiscalInfoService()
                                 .ParseRIFFile(EncodedFileTextbox.Text);
        }
    }
}