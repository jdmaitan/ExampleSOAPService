using FiscalInfoWebService.Models.InputModels;
using FiscalInfoWebService.Services;

namespace FiscalInfoWebService.Controllers
{
    public abstract class BaseController
    {
        private ServiceStatusChecker serviceStatusChecker;
        protected MessageService messageService;
        protected ValidationService validationService;

        public BaseController()
        {
            serviceStatusChecker = new ServiceStatusChecker();
            messageService = new MessageService();
            validationService = new ValidationService();
        }

        public string Execute(BaseInput input)
        {
            string responseMessage = string.Empty;

            try
            {
                if (!serviceStatusChecker.ServiceIsRunning())
                {
                    return messageService.GetResponseMessage(500);
                }

                if (!input.IsValid(ref responseMessage))
                {
                    return responseMessage;
                }

                if (!DataValidationsPassed(input, ref responseMessage))
                {
                    return responseMessage;
                }

                return DoSpecificOperation(input);
            }
            catch
            {
                return MessageService.WebServiceErrorMessage;
            }
        }

        protected abstract bool DataValidationsPassed(BaseInput baseInput, ref string responseMessage);

        protected abstract string DoSpecificOperation(BaseInput baseInput);
    }
}