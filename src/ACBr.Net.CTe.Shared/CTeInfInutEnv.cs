// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 03-15-2018
//
// Last Modified By : RFTD
// Last Modified On : 06-22-2018
// ***********************************************************************
// <copyright file="CTeInfInutEnv.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeInfInutEnv : GenericClone<CTeInfInutEnv>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeInfInutEnv()
        {
            XServ = "INUTILIZAR";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     DP04 - Identificador da TAG a ser assinada formada com Código da UF + Ano (2 posições) + CNPJ + modelo + série +
        ///     nro inicial e nro final precedida do literal “ID”
        /// </summary>
        [DFeAttribute(TipoCampo.Str, "Id", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        /// <summary>
        ///     DP05 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        [DFeElement(TipoCampo.Enum, "tpAmb", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoAmbiente TpAmb { get; set; }

        /// <summary>
        ///     DP06 - Serviço solicitado: "INUTILIZAR"
        /// </summary>
        [DFeElement(TipoCampo.Str, "xServ", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XServ { get; set; }

        /// <summary>
        ///     DP07 - Código da UF do solicitante
        /// </summary>
        [DFeElement(TipoCampo.Enum, "cUF", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCodUF CUF { get; set; }

        /// <summary>
        ///     DP08 - Ano de inutilização da numeração
        /// </summary>
        [DFeElement(TipoCampo.Int, "ano", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Ano { get; set; }

        /// <summary>
        ///     DP09 - CNPJ do emitente
        /// </summary>
        [DFeElement(TipoCampo.Str, "CNPJ", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        /// <summary>
        ///     DP10 - Modelo da NF-e (55)
        /// </summary>
        [DFeElement(TipoCampo.Enum, "mod", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public ModeloCTe Mod { get; set; }

        /// <summary>
        ///     DP11 - Série da NF-e
        /// </summary>
        [DFeElement(TipoCampo.Int, "serie", Min = 1, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Serie { get; set; }

        /// <summary>
        ///     DP12 - Número da NF-e inicial a ser inutilizada
        /// </summary>
        [DFeElement(TipoCampo.Int, "nCTIni", Min = 2, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public long NCtIni { get; set; }

        /// <summary>
        ///     DP13 - Número da NF-e final a ser inutilizada
        /// </summary>
        [DFeElement(TipoCampo.Int, "nCTFin", Min = 2, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
        public long NCtFin { get; set; }

        /// <summary>
        ///     DP14 - Informar a justificativa do pedido de inutilização
        /// </summary>
        [DFeElement(TipoCampo.Str, "xJust", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XJust { get; set; }

        #endregion Properties
    }
}