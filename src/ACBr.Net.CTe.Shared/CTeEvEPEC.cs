// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-22-2018
//
// Last Modified By : RFTD
// Last Modified On : 06-26-2018
// ***********************************************************************
// <copyright file="CTeEvEPEC.cs" company="ACBr.Net">
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
using System.ComponentModel;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    [DFeRoot("evEPECCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTeEvEPEC : DFeDocument<CTeEvEPEC>, IEventoCTe, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CTeEvEPEC"/> class.
        /// </summary>
        public CTeEvEPEC()
        {
            DescEvento = "EPEC";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Define/retorna a descrição do evento, não alterar.
        /// </summary>
        [DFeElement(TipoCampo.Str, "descEvento", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DescEvento { get; set; }

        /// <summary>
		/// Define/retorna a justificativa.
		/// </summary>
        [DFeElement(TipoCampo.Str, "xJust", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XJust { get; set; }

        [DFeElement(TipoCampo.De2, "vICMS", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VICMS { get; set; }

        [DFeElement(TipoCampo.De2, "vTPrest", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VTPrest { get; set; }

        [DFeElement(TipoCampo.De2, "vCarga", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VCarga { get; set; }

        [DFeElement("toma4", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeTomadorEPEC Tomador { get; set; }

        [DFeElement(TipoCampo.Enum, "modal", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeModal Modal { get; set; }

        [DFeElement(TipoCampo.Enum, "UFIni", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeSiglaUF UFIni { get; set; }

        [DFeElement(TipoCampo.Enum, "UFFim", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeSiglaUF UFFim { get; set; }

        [DFeElement(TipoCampo.Enum, "tpCTe", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeTipo TpCTe { get; set; }

        [DFeElement(TipoCampo.DatHorTz, "dhEmi", Min = 25, Max = 25, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhEmi { get; set; }

        #endregion Properties
    }
}