using System.ServiceModel;

namespace ACBr.Net.CTe.Services
{
	public abstract class ResponseBase
	{
		#region Propriedades

		[MessageHeader(Name = "cteCabecMsg", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao")]
		public CTeWsCabecalho Cabecalho;

		#endregion Propriedades
	}
}