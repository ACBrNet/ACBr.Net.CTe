using System.Security.Cryptography.X509Certificates;
using ACBr.Net.CTe.Configuracao;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Extensions;
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    public sealed class ConsultaCadastroServiceClient : DFeConsultaCadastroServiceClient<CTeConfig, ACBrCTe,
        CTeConfigGeral, CTeVersao, CTeConfigWebServices, CTeConfigCertificados, CTeConfigArquivos, SchemaCTe>
    {
        public ConsultaCadastroServiceClient(CTeConfig config, DFeCodUF uf, X509Certificate2 certificado = null) :
            base(config, CTeServiceManager.GetServiceAndress(config.Geral.VersaoDFe, uf.ToSiglaUF(),
                TipoServicoCTe.CTeConsultaCadastro, config.Geral.FormaEmissao, config.WebServices.Ambiente), certificado)
        {
        }
    }
}