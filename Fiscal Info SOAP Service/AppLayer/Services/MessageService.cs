using FiscalInfoWebService.Data;

namespace FiscalInfoWebService.Services
{
    public class MessageService
    {
        private DataService dataService;

        public static string WebServiceErrorMessage => "5 || Service Failure || Error proccesing operation";

        public MessageService()
        {
            dataService = new DataService();
        }

        public string GetResponseMessage(int specificMessageCode, string additionalMessage = "")
        {
            try
            {
                SpecificMessage specificMessage = dataService.GetResponseMessage(specificMessageCode).Result;

                if (specificMessage == null)
                {
                    return WebServiceErrorMessage;
                }

                return $"{specificMessage.GeneralMessageID} || {specificMessage.GeneralMessage.Message} || {specificMessageCode}: {specificMessage.Message}{additionalMessage}";
            }
            catch
            {
                return WebServiceErrorMessage;
            }
        }
    }
}