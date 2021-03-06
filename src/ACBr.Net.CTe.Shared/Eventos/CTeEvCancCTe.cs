// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2017
//
// Last Modified By : RFTD
// Last Modified On : 10-22-2017
// ***********************************************************************
// <copyright file="CTeEvCancCTe.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Document;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe.Eventos
{
    [DFeRoot("evCancCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTeEvCancCTe : DFeDocument<CTeEvCancCTe>, IEventoCTe, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        /// <summary>
		/// Inicializa uma nova inst�ncia da classe <see cref="CTeEvCancCTe"/> class.
		/// </summary>
        public CTeEvCancCTe()
        {
            DescEvento = "Cancelamento";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Define/retorna a descri��o do evento, n�o alterar.
        /// </summary>
        [DFeElement(TipoCampo.Str, "descEvento", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DescEvento { get; set; }

        [DFeElement(TipoCampo.Str, "nProt", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NProt { get; set; }

        [DFeElement(TipoCampo.Str, "xJust", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XJust { get; set; }

        #endregion Properties
    }
}