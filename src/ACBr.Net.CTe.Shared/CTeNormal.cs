// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-16-2016
// ***********************************************************************
// <copyright file="CTeNormal.cs" company="ACBr.Net">
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

namespace ACBr.Net.CTe
{
    public sealed class CTeNormal : GenericClone<CTeNormal>, IInfoCTe, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeNormal()
        {
            InfServVinc = new DFeCollection<CTeInfCTeMultimodal>();
            InfGlobalizado = new CTeInfGlobalizado();
            InfCTeSub = new CTeInfCTeSub();
            Cobr = new CTeCobranca();
            VeicNovos = new DFeCollection<CTeVeicNovos>();
            InfModal = new CTeInfModal();
            DocAnt = new DFeCollection<CTeEmiDocAnt>();
            InfDoc = new DFeCollection<ICTeInfDoc>();
            InfCarga = new CTeInfCarga();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement("infCarga", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeInfCarga InfCarga { get; set; }

        [DFeItem(typeof(CTeInfNF), "infNF")]
        [DFeItem(typeof(CTeInfNFe), "infNFe")]
        [DFeItem(typeof(CTeInfOutros), "infOutros")]
        [DFeCollection("infDoc", Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<ICTeInfDoc> InfDoc { get; set; }

        [DFeItem(typeof(CTeEmiDocAnt), "emiDocAnt")]
        [DFeCollection("docAnt", Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<CTeEmiDocAnt> DocAnt { get; set; }

        [DFeElement("infModal", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeInfModal InfModal { get; set; }

        [DFeCollection("veicNovos", Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<CTeVeicNovos> VeicNovos { get; set; }

        [DFeElement("cobr", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeCobranca Cobr { get; set; }

        [DFeElement("infCteSub", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeInfCTeSub InfCTeSub { get; set; }

        [DFeElement("infGlobalizado", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeInfGlobalizado InfGlobalizado { get; set; }

        [DFeItem(typeof(CTeInfCTeMultimodal), "infCTeMultimodal")]
        [DFeCollection("infServVinc", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CTeInfCTeMultimodal> InfServVinc { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeInfGlobalizado()
        {
            return !InfGlobalizado.XObs.IsEmpty();
        }

        #endregion Methods
    }
}