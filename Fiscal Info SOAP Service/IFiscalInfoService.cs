using System.ServiceModel;

namespace FiscalInfoWebService
{
    [ServiceContract(Namespace = "http://SOAPExample.com/", Name = "FiscalInfoService")]
    public interface IFiscalInfoService
    {
        [OperationContract]
        string GetFiscalInfo(string rif);


        [OperationContract]
        string PostFiscalInfo(string rif,
                              string businessName,
                              string address);

        [OperationContract]
        string ParseRIFFile(string fileEncodedInBase64);

        [OperationContract]
        string ValidateRIF(string rif);
    }
}
