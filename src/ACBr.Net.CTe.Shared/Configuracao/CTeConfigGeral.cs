// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-11-2017
//
// Last Modified By : RFTD
// Last Modified On : 06-11-2017
// ***********************************************************************
// <copyright file="CTeConfigGeral.cs" company="ACBr.Net">
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
using ACBr.Net.Core;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe.Configuracao
{
    [TypeConverter(typeof(ACBrExpandableObjectConverter))]
    public sealed class CTeConfigGeral : DFeGeralConfigBase<ACBrCTe, CTeVersao>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CTeConfigGeral"/> class.
        /// </summary>
        internal CTeConfigGeral(ACBrCTe parent) : base(parent)
        {
            VersaoDFe = CTeVersao.v300;
            Mod = ModeloCTe.CTe;
        }

        #endregion Constructor

        #region Properties

        public ModeloCTe Mod { get; set; }

        #endregion Properties
    }
}