namespace FiscalInfoWebService.Models.InputModels
{
    public class EncodedFileInput : BaseInput
    {
        public string EncodedFile { get; set; } = string.Empty;

        public EncodedFileInput(string encodedFile)
        {
            EncodedFile = encodedFile;
        }

        public override bool IsValid(ref string responseMessage)
        {
            return inputValidationService.IsAValidBase64String(EncodedFile, ref responseMessage);
        }
    }
}