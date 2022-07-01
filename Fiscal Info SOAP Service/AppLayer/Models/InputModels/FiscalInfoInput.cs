namespace FiscalInfoWebService.Models.InputModels
{
    public class FiscalInfoInput : BaseInput
    {
        public string RIF { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public FiscalInfoInput(string rif,
                               string businessName,
                               string address)
        {
            RIF = rif.ToUpper();
            BusinessName = businessName;
            Address = address;
        }

        public override bool IsValid(ref string responseMessage)
        {
            return inputValidationService.IsAValidRIFAsync(RIF, ref responseMessage)
                && inputValidationService.IsAValidBusinessName(BusinessName, ref responseMessage)
                && inputValidationService.IsAValidAddress(Address, ref responseMessage);
        }
    }
}