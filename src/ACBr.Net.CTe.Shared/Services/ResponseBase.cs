using System.ServiceModel;
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    [MessageContract]
    public abstract class ResponseBase
    {
        #region Propriedades

        [MessageHeader(Name = "cteCabecMsg")]
        public DFeWsCabecalho Cabecalho;

        #endregion Propriedades
    }
}