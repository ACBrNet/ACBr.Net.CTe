// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2017
//
// Last Modified By : RFTD
// Last Modified On : 10-22-2017
// ***********************************************************************
// <copyright file="CTeRetInfEvento.cs" company="ACBr.Net">
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

using System;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe.Services
{
    public sealed class CTeRetInfEvento
    {
        #region Properties

        [DFeAttribute(TipoCampo.Str, "id", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoAmbiente TipoAmbiente { get; set; }

        [DFeElement(TipoCampo.Enum, "cUF", Min = 1, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCodUF UF { get; set; }

        [DFeElement(TipoCampo.Str, "verAplic", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VersaoAplicacao { get; set; }

        [DFeElement(TipoCampo.Int, "cOrgao", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Orgao { get; set; }

        [DFeElement(TipoCampo.Int, "cStat", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        [DFeElement(TipoCampo.Str, "xMotivo", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Motivo { get; set; }

        [DFeElement(TipoCampo.Str, "chCTe", Min = 44, Max = 44, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string ChaveCTe { get; set; }

        [DFeElement(TipoCampo.Enum, "tpEvento", Min = 6, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeTipoEvento? TipoEvento { get; set; }

        [DFeElement(TipoCampo.Str, "xEvento", Min = 4, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Evento { get; set; }

        [DFeElement(TipoCampo.Int, "nSeqEvento", Min = 4, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NumeroSeqEvento { get; set; }

        [DFeElement(TipoCampo.DatHorTz, "dhRegEvento", Min = 25, Max = 25, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTimeOffset? DhRegEvento { get; set; }

        [DFeElement(TipoCampo.Str, "nProt", Min = 1, Max = 255, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Protocolo { get; set; }

        #endregion Properties
    }
}