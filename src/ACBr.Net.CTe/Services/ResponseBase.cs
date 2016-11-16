using System.ServiceModel;

namespace ACBr.Net.CTe.Services
{
	[MessageContract]
	public abstract class ResponseBase
	{
		#region Propriedades

		[MessageHeader(Name = "cteCabecMsg")]
		public CTeWsCabecalho Cabecalho;

		#endregion Propriedades
	}
}