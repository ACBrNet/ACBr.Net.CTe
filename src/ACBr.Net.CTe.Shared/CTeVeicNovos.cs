using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
	public sealed class CTeVeicNovos : GenericClone<CTeVeicNovos>
	{
		[DFeElement(TipoCampo.Str, "chassi", Id = "#378", Min = 17, Max = 17, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Chassi { get; set; }

		[DFeElement(TipoCampo.Str, "cCor", Id = "#379", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CCor { get; set; }

		[DFeElement(TipoCampo.Str, "xCor", Id = "#380", Min = 1, Max = 40, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XCor { get; set; }

		[DFeElement(TipoCampo.Str, "cMod", Id = "#381", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CMod { get; set; }

		[DFeElement(TipoCampo.De2, "vUnit", Id = "#382", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VUnit { get; set; }

		[DFeElement(TipoCampo.De2, "vFrete", Id = "#383", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VFrete { get; set; }
	}
}