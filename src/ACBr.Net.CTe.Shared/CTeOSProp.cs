// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : marcosgerene
// Created          : 09-01-2019
//
// Last Modified By : marcosgerene
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="CTeVeicNovos.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2016 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeOSProp : GenericClone<CTeOSProp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "#008", Min = 11, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CPF { get; set; }

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "#009", Min = 14, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string CNPJ { get; set; }

        [DFeElement(TipoCampo.StrNumber, "TAF", Id = "#010", Min = 12, Max = 12, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string TAF { get; set; }

        [DFeElement(TipoCampo.StrNumber, "NroRegEstadual", Id = "#011", Min = 25, Max = 25, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NroRegEstadual { get; set; }

        [DFeElement(TipoCampo.Str, "xNome", Id = "#012", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNome { get; set; }

        [DFeElement(TipoCampo.Custom, "IE", Id = "#013", Min = 0, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

        [DFeElement(TipoCampo.Enum, "UF", Id = "#016", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeSiglaUF? UF { get; set; }

        [DFeElement(TipoCampo.Enum, "tpProp", Id = "#016", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public TpPropCTe TpProp { get; set; }

        #endregion Properties

        #region Methods

        private bool ShouldSerializeCPF()
        {
            return CNPJ.IsEmpty();
        }

        private bool ShouldSerializeCNPJ()
        {
            return CPF.IsEmpty();
        }

        private bool ShouldSerializeTAF()
        {
            return !TAF.IsEmpty();
        }

        private bool ShouldSerializeNroRegEstadual()
        {
            return !NroRegEstadual.IsEmpty();
        }

        private bool ShouldSerializeIE()
        {
            return !IE.IsEmpty();
        }

        private bool ShouldSerializeUF()
        {
            return UF.HasValue;
        }

        #endregion Methods
    }
}
