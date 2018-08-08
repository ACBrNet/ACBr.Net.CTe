using System.ComponentModel;
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeEnderecoEmit : GenericClone<CTeEnderecoEmit>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        [DFeElement(TipoCampo.Str, "xLgr", Id = "#046", Min = 2, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XLgr { get; set; }

        [DFeElement(TipoCampo.Str, "nro", Id = "#047", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Nro { get; set; }

        [DFeElement(TipoCampo.Str, "xCpl", Id = "#048", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XCpl { get; set; }

        [DFeElement(TipoCampo.Str, "xBairro", Id = "#049", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XBairro { get; set; }

        [DFeElement(TipoCampo.Int, "cMun", Id = "#050", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CMun { get; set; }

        [DFeElement(TipoCampo.Str, "xMun", Id = "#051", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMun { get; set; }

        [DFeElement(TipoCampo.StrNumber, "CEP", Id = "#052", Min = 8, Max = 8, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CEP { get; set; }

        [DFeElement(TipoCampo.Enum, "UF", Id = "#053", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeSiglaUF UF { get; set; }

        [DFeElement(TipoCampo.Str, "fone", Id = "#054", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        #endregion Properties
    }
}