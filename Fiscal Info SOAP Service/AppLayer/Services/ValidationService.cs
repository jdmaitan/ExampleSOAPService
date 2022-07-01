namespace FiscalInfoWebService.Services
{
    public class ValidationService
    {
        DataService intranetDbService;

        public ValidationService()
        {
            intranetDbService = new DataService();
        }

        public bool HasAValidCustomerRIFPrefix(string RIF)
        {
            return intranetDbService
                   .GetValidRIFPrefixes().Result
                   .Contains(RIF[0].ToString());
        }
    }
}