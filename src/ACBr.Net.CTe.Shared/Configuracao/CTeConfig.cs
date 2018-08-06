// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-11-2017
//
// Last Modified By : RFTD
// Last Modified On : 06-11-2017
// ***********************************************************************
// <copyright file="CTeConfig.cs" company="ACBr.Net">
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

using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe.Configuracao
{
    public sealed class CTeConfig : DFeConfigBase<ACBrCTe, CTeConfigGeral, CTeVersao, CTeConfigWebServices, CTeConfigCertificados, CTeConfigArquivos, SchemaCTe>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CTeConfig"/> class.
        /// </summary>
        public CTeConfig(ACBrCTe parent) : base(parent)
        {
        }

        #endregion Constructor

        /// <inheritdoc />
        protected override void CreateConfigs()
        {
            Geral = new CTeConfigGeral(Parent);
            WebServices = new CTeConfigWebServices(Parent);
            Certificados = new CTeConfigCertificados(Parent);
            Arquivos = new CTeConfigArquivos(Parent);
        }
    }
}