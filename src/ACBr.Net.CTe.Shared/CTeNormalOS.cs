// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : marcosgerene
// Created          : 01-09-2019
//
// Last Modified By : marcosgerene
// Last Modified On : 01-09-2019
// ***********************************************************************
// <copyright file="CTeNormalOS.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeNormalOS : GenericClone<CTeNormalOS>, IInfoCTe, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeNormalOS()
        {
            Cobr = new CTeCobranca();
            RefCTeCanc = string.Empty;
            InfCTeSub = new CTeOSInfCTeSub();
            InfModal = new CTeInfModal();
            Seg = new DFeCollection<CTeOSSeg>();
            InfDocRef = new DFeCollection<CTeOSInfDocRef>();
            InfServico = new CTeOSInfServico();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement("infServico", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeOSInfServico InfServico { get; set; }

        [DFeCollection("infDocRef", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CTeOSInfDocRef> InfDocRef { get; set; }

        [DFeCollection("seg", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CTeOSSeg> Seg { get; set; }

        [DFeElement("infModal", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeInfModal InfModal { get; set; }

        [DFeElement("infCteSub", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeOSInfCTeSub InfCTeSub { get; set; }

        [DFeElement(TipoCampo.Str, "refCTeCanc", Id = "", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string RefCTeCanc { get; set; }

        [DFeElement("cobr", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeCobranca Cobr { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeInfCTeSub()
        {
            return !InfCTeSub.ChCte.IsEmpty();
        }

        private bool ShouldSerializeRefCTeCanc()
        {
            return !RefCTeCanc.IsEmpty();
        }

        private bool ShouldSerializeCobr()
        {
            return !Cobr.Fat.NFat.IsEmpty();
        }

        #endregion Methods
    }
}