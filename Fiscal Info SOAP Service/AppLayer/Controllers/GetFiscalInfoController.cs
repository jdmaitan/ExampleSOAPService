using FiscalInfoWebService.Data;
using FiscalInfoWebService.Models.InputModels;
using FiscalInfoWebService.Services;

namespace FiscalInfoWebService.Controllers
{
    public class GetFiscalInfoController : BaseController
    {
        private DataService dataService;

        private Taxpayer Customer { get; set; }

        public GetFiscalInfoController()
        {
            dataService = new DataService();
        }

        protected override bool DataValidationsPassed(BaseInput baseInput, ref string responseMessage)
        {
            RIFInput input = baseInput as RIFInput;

            if (!validationService.HasAValidCustomerRIFPrefix(input.RIF))
            {
                responseMessage = messageService.GetResponseMessage(405);
                return false;
            }

            Customer = dataService.GetTaxpayerAsync(input.RIF).Result;

            if (Customer == null)
            {
                responseMessage = messageService.GetResponseMessage(400);
                return false;
            }

            return true;
        }

        protected override string DoSpecificOperation(BaseInput input)
        {
            return messageService.GetResponseMessage(202, $"{Customer.RIF};;{Customer.BusinessName};;{Customer.Address}");
        }
    }
}