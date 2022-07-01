namespace FiscalInfoWebService.Services
{
    public class ServiceStatusChecker
    {
        private DataService dataService;

        public ServiceStatusChecker()
        {
            dataService = new DataService();
        }

        public bool ServiceIsRunning()
        {
            return dataService.GetServiceStatus().Result;
        }
    }
}