// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-22-2018
//
// Last Modified By : RFTD
// Last Modified On : 06-22-2018
// ***********************************************************************
// <copyright file="CTeEvPrestDesacordo.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    [DFeRoot("evPrestDesacordo", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTeEvPrestDesacordo : DFeDocument<CTeEvPrestDesacordo>, IEventoCTe, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CTeEvRegMultimodal"/> class.
        /// </summary>
        public CTeEvPrestDesacordo()
        {
            DescEvento = "Prestacao do Servico em Desacordo";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Define/retorna a descrição do evento, não alterar.
        /// </summary>
        [DFeElement(TipoCampo.Str, "descEvento", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string DescEvento { get; set; }

        [DFeElement(TipoCampo.Custom, "indDesacordoOper", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public bool IndDesacordoOper { get; set; }

        [DFeElement(TipoCampo.Str, "xObs", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Obs { get; set; }

        #endregion Properties

        #region Methods

        private string SerializeIndDesacordoOper()
        {
            return IndDesacordoOper ? "1" : "0";
        }

        private object DeserializeIndDesacordoOper(string value)
        {
            return value == "1";
        }

        #endregion Methods
    }
}