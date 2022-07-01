using FiscalInfoWebService.Data;
using FiscalInfoWebService.Models.InputModels;
using FiscalInfoWebService.Services;

namespace FiscalInfoWebService.Controllers
{
    public class PostFiscalInfoController : BaseController
    {
        private DataService dataService;

        private Taxpayer Customer { get; set; }

        public PostFiscalInfoController()
        {
            dataService = new DataService();
        }

        protected override bool DataValidationsPassed(BaseInput baseInput, ref string responseMessage)
        {
            FiscalInfoInput input = baseInput as FiscalInfoInput;

            if (!validationService.HasAValidCustomerRIFPrefix(input.RIF))
            {
                responseMessage = messageService.GetResponseMessage(405);
                return false;
            }


            return true;
        }

        protected override string DoSpecificOperation(BaseInput baseInput)
        {
            FiscalInfoInput input = baseInput as FiscalInfoInput;

            Customer = dataService.GetTaxpayerAsync(input.RIF).Result;

            if (Customer == null)
            {
                return CreateCustomer(input);
            }
            else
            {
                return UpdateCustomer(input);
            }
        }

        private string CreateCustomer(FiscalInfoInput input)
        {
            int result;

            result = dataService.CreateTaxpayerAsync(input).Result;

            if (result <= 0)
            {
                return messageService.GetResponseMessage(501);
            }

            return messageService.GetResponseMessage(200);
        }

        private string UpdateCustomer(FiscalInfoInput input)
        {
            int result;

            result = dataService.UpdateCustomerAsync(input).Result;

            if (result <= 0)
            {
                return messageService.GetResponseMessage(502);
            }

            return messageService.GetResponseMessage(201);
        }
    }
}