// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2017
//
// Last Modified By : RFTD
// Last Modified On : 10-22-2017
// ***********************************************************************
// <copyright file="CTeInfEventoEnv.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe.Eventos
{
    public sealed class CTeInfEventoEnv : GenericClone<CTeInfEventoEnv>
    {
        /// <summary>
        ///     HP07 - Grupo de informações do registro do Evento
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     HP08 - Código do órgão de recepção do Evento.
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cOrgao", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCodUF Orgao { get; set; }

        /// <summary>
        ///     HP09 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoAmbiente Ambiente { get; set; }

        /// <summary>
        ///     HP10 - CNPJ do autor do Evento
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     HP12 - Chave de Acesso da NF-e vinculada ao Evento
        /// </summary>
        [DFeElement(TipoCampo.Str, "chCTe", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Chave { get; set; }

        /// <summary>
        ///     HP13 - Data e hora do evento no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [DFeElement(TipoCampo.DatHorTz, "dhEvento", Min = 19, Max = 19, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhEvento { get; set; }

        /// <summary>
        ///     HP14 - Código do evento
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpEvento", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeTipoEvento TpEvento { get; set; }

        /// <summary>
        ///     HP15 - Sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        [DFeElement(TipoCampo.Int, "nSeqEvento", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NSeqEvento { get; set; }

        /// <summary>
        ///     HP17 - Informações do Pedido de Cancelamento
        /// </summary>
        [DFeElement("detEvento", Ocorrencia = Ocorrencia.Obrigatoria)]
        public DetEvento detEvento { get; set; }
    }
}