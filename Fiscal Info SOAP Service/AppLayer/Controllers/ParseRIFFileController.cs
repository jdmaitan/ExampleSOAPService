using FiscalInfoWebService.Data;
using FiscalInfoWebService.Models.InputModels;
using FiscalInfoWebService.Services;

namespace FiscalInfoWebService.Controllers
{
    public class ParseRIFFileController : BaseController
    {
        private EncodedPDFReader encodedPDFReader;
        private RIFTextParser rifTextParser;
        public ParseRIFFileController()
        {
            encodedPDFReader = new EncodedPDFReader();
            rifTextParser = new RIFTextParser();
        }

        protected override bool DataValidationsPassed(BaseInput baseInput, ref string responseMessage)
        {
            return true;
        }

        protected override string DoSpecificOperation(BaseInput baseInput)
        {
            EncodedFileInput input = baseInput as EncodedFileInput;

            string pdfFileText = encodedPDFReader.GetPDFText(input.EncodedFile);

            if (string.IsNullOrEmpty(pdfFileText))
            {
                return messageService.GetResponseMessage(503);
            }

            Taxpayer taxpayer = rifTextParser.GetTaxpayer(pdfFileText);

            if (taxpayer == null)
            {
                return messageService.GetResponseMessage(413);
            }

            return messageService.GetResponseMessage(204, $"{taxpayer.RIF};;{taxpayer.BusinessName};;{taxpayer.Address}");
        }
    }
}