namespace FiscalInfoWebService.Data
{
    public class SpecificMessage
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public int GeneralMessageID { get; set; }

        public virtual GeneralMessage GeneralMessage { get; set; }
    }
}