using System;
using System.Web.UI;

namespace FiscalInfoWebService.UI
{
    public partial class GetFiscalInfoTest : Page
    {
        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            ResponseLabel.Text = new FiscalInfoService()
                                 .GetFiscalInfo(RIFTextBox.Text);
        }
    }
}