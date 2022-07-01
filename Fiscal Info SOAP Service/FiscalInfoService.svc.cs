using FiscalInfoWebService.Controllers;
using FiscalInfoWebService.Models.InputModels;

namespace FiscalInfoWebService
{
    public class FiscalInfoService : IFiscalInfoService
    {
        public string GetFiscalInfo(string rif)
        {
            return new GetFiscalInfoController()
                   .Execute(new RIFInput(rif));
        }

        public string PostFiscalInfo(string rif,
                                    string businessName,
                                    string address)
        {
            return new PostFiscalInfoController()
                  .Execute(new FiscalInfoInput(rif,
                                               businessName,
                                               address));
        }

        public string ParseRIFFile(string fileEncodedInBase64)
        {
            return new ParseRIFFileController()
                   .Execute(new EncodedFileInput(fileEncodedInBase64));
        }

        public string ValidateRIF(string rif)
        {
            return new ValidateRIFController()
                   .Execute(new RIFInput(rif));
        }
    }
}
