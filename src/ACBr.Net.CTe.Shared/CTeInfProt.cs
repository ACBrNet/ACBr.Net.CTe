// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-07-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-07-2016
// ***********************************************************************
// <copyright file="CTeInfProt.cs" company="ACBr.Net">
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

using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;
using System;
using System.ComponentModel;

namespace ACBr.Net.CTe
{
    public sealed class CTeInfProt : GenericClone<CTeInfProt>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeAttribute(TipoCampo.Str, "ID", Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoAmbiente TpAmb { get; set; }

        [DFeElement(TipoCampo.Str, "verAplic", Id = "", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerAplic { get; set; }

        [DFeElement(TipoCampo.Str, "chCTe", Id = "", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string ChCTe { get; set; }

        [DFeElement(TipoCampo.DatHor, "dhRecbto", Id = "", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTime DhRecbto { get; set; }

        [DFeElement(TipoCampo.Str, "nProt", Id = "", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NProt { get; set; }

        [DFeElement(TipoCampo.Str, "digVal", Id = "", Min = int.MinValue, Max = int.MaxValue, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DigVal { get; set; }

        [DFeElement(TipoCampo.Int, "cStat", Id = "", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        [DFeElement(TipoCampo.Str, "xMotivo", Id = "", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMotivo { get; set; }

        #endregion Propriedades
    }
}