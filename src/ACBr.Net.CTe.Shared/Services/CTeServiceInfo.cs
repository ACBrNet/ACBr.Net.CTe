﻿// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="CTeServiceInfo.cs" company="ACBr.Net">
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
using System.Collections.Generic;

namespace ACBr.Net.CTe.Services
{
    [Serializable]
    public sealed class CTeServiceInfo
    {
        #region Fields

        private readonly Dictionary<ServicoCTe, string> serviceUrl;

        #endregion Fields

        #region Constructors

        public CTeServiceInfo()
        {
            serviceUrl = new Dictionary<ServicoCTe, string>(9)
            {
                { ServicoCTe.RecepcaoEvento, string.Empty },
                { ServicoCTe.CTeRecepcao, string.Empty },
                { ServicoCTe.CTeRetRecepcao, string.Empty },
                { ServicoCTe.CTeInutilizacao, string.Empty },
                { ServicoCTe.CTeConsultaProtocolo, string.Empty },
                { ServicoCTe.CTeStatusServico, string.Empty },
                { ServicoCTe.CTeConsultaCadastro, string.Empty },
                { ServicoCTe.RecepcaoEventoAN, string.Empty },
                { ServicoCTe.CTeRecepcaoOS, string.Empty }
            };
        }

        #endregion Constructors

        #region Propriedades

        public string this[ServicoCTe service]
        {
            get => serviceUrl[service];
            set => serviceUrl[service] = value;
        }

        #endregion Propriedades
    }
}