// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : marcosgerene
// Created          : 09-01-2019
//
// Last Modified By : marcosgerene
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="CTeVeicNovos.cs" company="ACBr.Net">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ACBr.Net.CTe
{
    public sealed class CTeOSVeic : GenericClone<CTeOSVeic>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        [DFeElement(TipoCampo.Str, "placa", Id = "#005", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Placa { get; set; }

        [DFeElement(TipoCampo.StrNumber, "RENAVAM", Id = "#006", Min = 9, Max = 11, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Renavam { get; set; }

        [DFeElement(TipoCampo.Enum, "UF", Id = "#016", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeSiglaUF UF { get; set; }

        #endregion Properties

        #region Methods

        private bool ShouldSerializeRenavam()
        {
            return !Renavam.IsEmpty();
        }
        
        #endregion Methods
    }
}
