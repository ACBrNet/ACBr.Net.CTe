// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2017
//
// Last Modified By : RFTD
// Last Modified On : 21-06-2018
// ***********************************************************************
// <copyright file="CTeTipoEvento.cs" company="ACBr.Net">
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

namespace ACBr.Net.CTe
{
    /// <summary>
    /// Tipos de eventos da CTe
    /// </summary>
    public enum CTeTipoEvento
    {
        /// <summary>
        /// 110110 - Carta de Correção
        /// </summary>
        [DFeEnum("110110")]
        [Description("110110 - Carta de Correção")]
        CartaCorrecao = 110110,

        /// <summary>
        /// 110111 - Cancelamento
        /// </summary>
        [DFeEnum("110111")]
        [Description("110111 - Cancelamento")]
        Cancelamento = 110111,

        /// <summary>
        /// 110113 - EPEC
        /// </summary>
        [DFeEnum("110113")]
        [Description("110113 - EPEC")]
        EPEC = 110113,

        /// <summary>
        /// 110160 - Registros do Multimodal
        /// </summary>
        [DFeEnum("110160")]
        [Description("110160 - Registros do Multimodal")]
        RegistroMultiModal = 110160,

        /// <summary>
        /// 110170 - Informações da GTV
        /// </summary>
        [DFeEnum("110170")]
        [Description("110170 - Informações da GTV")]
        InfoGTV = 110170,

        /// <summary>
        /// 310620 - Registro de Passagem
        /// </summary>
        [DFeEnum("310620")]
        [Description("310620 - Registro de Passagem")]
        RegistroPassagem = 310620,

        /// <summary>
        /// 510620 - Registro de Passagem Automático
        /// </summary>
        [DFeEnum("510620")]
        [Description("510620 - Registro de Passagem Automático")]
        RegistroPassagemAuto = 510620,

        /// <summary>
        /// 310610 - MDF-e Autorizado
        /// </summary>
        [DFeEnum("310610")]
        [Description("310610 - MDF-e Autorizado")]
        MDFeAutorizado = 310610,

        /// <summary>
        /// 310611 - MDF-e Cancelado
        /// </summary>
        [DFeEnum("310611")]
        [Description("310611 - MDF-e Cancelado")]
        MDFeCancelado = 310610,

        /// <summary>
        /// 240130 - Autorizado CT-e Complementar
        /// </summary>
        [DFeEnum("240130")]
        [Description("240130 - Autorizado CT-e Complementar")]
        AutorizadoCTeComplementar = 240130,

        /// <summary>
        /// 240131 - Cancelado CT-e Complementar
        /// </summary>
        [DFeEnum("240131")]
        [Description("240131 - Cancelado CT-e Complementar")]
        CanceladoCTeComplementar = 240131,

        /// <summary>
        /// 240140 - CT-e de Substituição
        /// </summary>
        [DFeEnum("240140")]
        [Description("240140 - CT-e de Substituição")]
        CTeSubstituicao = 240140,

        /// <summary>
        /// 240150 - CT-e de Anulação
        /// </summary>
        [DFeEnum("240150")]
        [Description("240150 - CT-e de Anulação")]
        CTeAnulacao = 240150,

        /// <summary>
        /// 240160 - Liberação de EPEC
        /// </summary>
        [DFeEnum("240160")]
        [Description("240160 - Liberação de EPEC")]
        LiberacaoEPEC = 240160,

        /// <summary>
        /// 240170 - Liberação Prazo Cancelamento
        /// </summary>
        [DFeEnum("240170")]
        [Description("240170 - Liberação Prazo Cancelamento")]
        LiberacaoPrazoCancelamento = 240170,

        /// <summary>
        /// 440130 - Autorizado Redespacho
        /// </summary>
        [DFeEnum("440130")]
        [Description("440130 - Autorizado Redespacho")]
        AutorizadoRedespacho = 440130,

        /// <summary>
        /// 440140 - Autorizado Redespacho Intermediário
        /// </summary>
        [DFeEnum("440140")]
        [Description("440140 - Autorizado Redespacho Intermediário")]
        AutorizadoRedespachoIntermediario = 440140,

        /// <summary>
        /// 440150 - Autorizado Subcontratação
        /// </summary>
        [DFeEnum("440150")]
        [Description("440150 - Autorizado Subcontratação")]
        AutorizadoSubcontratacao = 440150,

        /// <summary>
        /// 440160 - Autorizado Serviço Vinculado Multimodal
        /// </summary>
        [DFeEnum("440160")]
        [Description("440160 - Autorizado Serviço Vinculado Multimodal")]
        AutorizadoServicoVinculadoMultimodal = 440160,

        /// <summary>
        /// 610110 - Prestação do Serviço em Desacordo
        /// </summary>
        [DFeEnum("610110")]
        [Description("610110 - Prestação do Serviço em Desacordo")]
        PrestacaoServicoDesacordo = 610110
    }
}