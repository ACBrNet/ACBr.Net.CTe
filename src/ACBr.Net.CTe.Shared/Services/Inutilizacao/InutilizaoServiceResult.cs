﻿// ***********************************************************************
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

using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Document;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe.Services
{
    [DFeRoot("retInutCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class InutilizaoServiceResult : DFeDocument<InutilizaoServiceResult>
    {
        #region Constructors

        public InutilizaoServiceResult()
        {
            InfInut = new InfInutRet();
            Signature = new DFeSignature();
        }

        #endregion Constructors

        #region Properties

        [DFeAttribute(TipoCampo.Str, "versao", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Versao { get; set; }

        [DFeElement("infInut", Ocorrencia = Ocorrencia.Obrigatoria)]
        public InfInutRet InfInut { get; set; }

        public DFeSignature Signature { get; set; }

        #endregion Properties
    }
}