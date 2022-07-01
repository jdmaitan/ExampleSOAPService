using FiscalInfoWebService.Services;

namespace FiscalInfoWebService.Models.InputModels
{
    public abstract class BaseInput
    {
        protected InputValidationService inputValidationService;

        public BaseInput()
        {
            inputValidationService = new InputValidationService();
        }

        public abstract bool IsValid(ref string responseMessage);
    }
}