// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="CTeStatusServicoServiceClient.cs" company="ACBr.Net">
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

using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Service;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace ACBr.Net.CTe.Services.StatusServico
{
	public sealed class CTeStatusServicoServiceClient : DFeWebserviceBase<ICTeStatusServico>, ICTeStatusServico
	{
		#region Constructors

		public CTeStatusServicoServiceClient(string url, TimeSpan? timeOut = null, X509Certificate2 certificado = null) : base(url, timeOut, certificado)
		{
		}

		#endregion Constructors

		#region Methods

		public string StatusServico(CTeWsCabecalho cabecalho, string mensagem)
		{
			Guard.Against<ArgumentNullException>(cabecalho == null, nameof(cabecalho));
			Guard.Against<ArgumentNullException>(mensagem.IsEmpty(), nameof(mensagem));

			var xml = new XmlDocument();
			xml.LoadXml(mensagem);

			var inValue = new StatusServicoRequest(cabecalho, xml);
			var retVal = ((ICTeStatusServico)this).StatusServico(inValue);
			return retVal.Result.OuterXml;
		}

		StatusServicoResponse ICTeStatusServico.StatusServico(StatusServicoRequest request)
		{
			return Channel.StatusServico(request);
		}

		#endregion Methods
	}
}