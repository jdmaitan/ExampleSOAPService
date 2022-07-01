using System;
using System.Web.UI;

namespace FiscalInfoWebService.UI
{
    public partial class ValidateRIFTest : Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ResponseLabel.Text = new FiscalInfoService()
                                 .ValidateRIF(RIFTextBox.Text);
        }
    }
}