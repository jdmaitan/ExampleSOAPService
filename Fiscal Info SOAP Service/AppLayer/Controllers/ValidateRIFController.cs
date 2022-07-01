using FiscalInfoWebService.Models.InputModels;

namespace FiscalInfoWebService.Controllers
{
    public class ValidateRIFController : BaseController
    {
        protected override bool DataValidationsPassed(BaseInput input, ref string responseMessage)
        {
            return true;
        }

        protected override string DoSpecificOperation(BaseInput input)
        {
            return messageService.GetResponseMessage(203);
        }
    }
}