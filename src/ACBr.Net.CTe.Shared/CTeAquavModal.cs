// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-21-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-21-2016
// ***********************************************************************
// <copyright file="CTeAquavModal.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Document;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    [DFeRoot("aquav")]
    public sealed class CTeAquavModal : DFeDocument<CTeAquavModal>, ICTeModal, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeAquavModal()
        {
            DetCont = new DFeCollection<AquavDetCont>();
            Balsa = new DFeCollection<CTeBalsa>();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.De2, "vPrest", Id = "#02", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VPrest { get; set; }

        [DFeElement(TipoCampo.De2, "vAFRMM", Id = "#03", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VAFRMM { get; set; }

        [DFeElement(TipoCampo.Str, "xNavio", Id = "#04", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNavio { get; set; }

        [DFeCollection("balsa", Id = "#05", Min = 0, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CTeBalsa> Balsa { get; set; }

        [DFeElement(TipoCampo.Int, "nViag", Id = "#07", Min = 1, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NViag { get; set; }

        [DFeElement(TipoCampo.Enum, "direc", Id = "#08", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeDirecao Direc { get; set; }

        [DFeElement(TipoCampo.Str, "irin", Id = "#09", Min = 1, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Irin { get; set; }

        [DFeCollection("detCont", Id = "#10", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<AquavDetCont> DetCont { get; set; }

        #endregion Propriedades
    }
}