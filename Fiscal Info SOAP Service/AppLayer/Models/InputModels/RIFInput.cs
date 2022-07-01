namespace FiscalInfoWebService.Models.InputModels
{
    public class RIFInput : BaseInput
    {
        public string RIF { get; set; } = string.Empty;

        public RIFInput(string rif)
        {
            RIF = rif.ToUpper();
        }

        public override bool IsValid(ref string responseMessage)
        {
            return inputValidationService.IsAValidRIFAsync(RIF, ref responseMessage);
        }
    }
}