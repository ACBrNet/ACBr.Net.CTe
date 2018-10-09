using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;


namespace ACBr.Net.CTe
{
    /// <summary>
    ///   TRespTec - Informações do Responsável Técnico pela emissão do DF-e
    /// </summary>
    public sealed class CTeInfRespTec : GenericClone<CTeInfRespTec>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeInfRespTec()
        {            
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        [DFeElement(TipoCampo.Custom, "xContato", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNome { get; set; }

        [DFeElement(TipoCampo.Str, "email", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Email { get; set; }

        [DFeElement(TipoCampo.Str, "fone", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        [DFeElement(TipoCampo.Int, "idCSRT", Min = 3, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? IdCSRT { get; set; }

        [DFeElement(TipoCampo.Custom, "hashCSRT", Min = 28, Max = 28, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string HashCSRT { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeEmail() => !string.IsNullOrEmpty(Email);

        private bool ShouldSerializeFone() => !string.IsNullOrEmpty(Fone);

        private bool ShouldSerializeIdCSRT() => IdCSRT != null;

        private bool ShouldSerializeHashCSRT() => !string.IsNullOrEmpty(HashCSRT);

        #endregion Methods
    }
}
