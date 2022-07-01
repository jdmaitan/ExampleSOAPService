using System.Collections.Generic;

namespace FiscalInfoWebService.Data
{
    public class GeneralMessage
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public virtual ICollection<SpecificMessage> SpecificMessages { get; set; }
    }
}