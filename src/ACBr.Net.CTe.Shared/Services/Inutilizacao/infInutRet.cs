// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-12-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-12-2016
// ***********************************************************************
// <copyright file="InutilizaoServiceResult.cs" company="ACBr.Net">
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

namespace ACBr.Net.CTe.Services
{
    public sealed class InfInutRet : GenericClone<InfInutRet>
    {
        #region Properties

        /// <summary>
        ///     DR04 - Identificador da TAG a ser assinada, somente precisa ser informado se a UF assinar a resposta.
        ///     Em caso de assinatura da resposta pela SEFAZ preencher o campo com o Nro do Protocolo, precedido com o literal
        ///     “ID”.
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     DR05 - Identificação do Ambiente: 1 – Produção / 2 – Homologação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoAmbiente TpAmb { get; set; }

        /// <summary>
        ///     Versão do Aplicativo que processou o pedido de inutilização.
        ///     A versão deve ser iniciada com a sigla da UF nos casos de WS próprio ou a sigla SCAN, SVAN ou SVRS nos demais
        ///     casos.
        /// </summary>
        [DFeElement(TipoCampo.Str, "verAplic", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerAplic { get; set; }

        /// <summary>
        ///     DR07 - Código do status da resposta (vide item 5.1.1).
        /// </summary>
        [DFeElement(TipoCampo.Int, "cStat", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CStat { get; set; }

        /// <summary>
        ///     DR08 - Descrição literal do status da resposta.
        /// </summary>
        [DFeElement(TipoCampo.Str, "xMotivo", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMotivo { get; set; }

        /// <summary>
        ///     DR09 - Código da UF que atendeu a solicitação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCodUF CUF { get; set; }

        /// <summary>
        ///     DR10 - Ano de inutilização da numeração
        /// </summary>
        [DFeElement(TipoCampo.Int, "ano", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? Ano { get; set; }

        /// <summary>
        ///     DR11 - CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     DR12 - Modelo da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Min = 2, Max = 2, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public ModeloCTe? Mod { get; set; }

        /// <summary>
        ///     DR13 - Série da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Int, "serie", Min = 3, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? Serie { get; set; }

        /// <summary>
        ///     DR14 - Número da NF-e inicial a ser inutilizada
        /// </summary>
        [DFeElement(TipoCampo.Int, "nCTIni", Min = 9, Max = 9, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NCtIni { get; set; }

        /// <summary>
        ///     DR15 - Número da NF-e final a ser inutilizada
        /// </summary>
        [DFeElement(TipoCampo.Int, "nCTFin", Min = 9, Max = 9, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public int? NCtFin { get; set; }

        /// <summary>
        ///     DR16 - Data e hora de processamento
        ///     Formato = AAAA-MM-DDTHH:MM:SS Preenchido com data e hora da gravação no Banco de Dados em caso de Confirmação.
        ///     Em caso de Rejeição, com data e hora do recebimento do Pedido.
        /// </summary>
        [DFeElement(TipoCampo.DatHor, "dhRecbto", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DateTime? DhRecbto { get; set; }

        /// <summary>
        ///     DR17 - Número do Protocolo de Inutilização (vide item 5.6).
        /// </summary>
        [DFeElement(TipoCampo.Str, "nProt", Min = 1, Max = 255, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string NProt { get; set; }

        #endregion Properties
    }
}