using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe.Services
{
	public abstract class CTeResultaBase<T> : DFeDocument<T> where T : class
	{
		[DFeAttribute(TipoCampo.Str, "versao", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Versao { get; set; }

		[DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DFeTipoAmbiente TipoAmbiente { get; set; }

		[DFeElement(TipoCampo.Str, "verAplic", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string VersaoAplicacao { get; set; }

		[DFeElement(TipoCampo.Int, "cStat", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
		public int CStat { get; set; }

		[DFeElement(TipoCampo.Str, "xMotivo", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Motivo { get; set; }

		[DFeElement(TipoCampo.Enum, "cUF", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DFeCodUF UF { get; set; }
	}
}