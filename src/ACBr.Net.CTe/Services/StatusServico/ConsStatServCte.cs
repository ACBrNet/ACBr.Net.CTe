// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="ConsStatServCte.cs" company="ACBr.Net">
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
using System;
using System.Xml.Serialization;

namespace ACBr.Net.CTe.Services.StatusServico
{
	[Serializable]
	[XmlType(TypeName = "consStatServCte")]
	public sealed class ConsStatServCte
	{
		public ConsStatServCte()
		{
		}

		public ConsStatServCte(DFeTipoAmbiente ambiente, string versao)
		{
			Ambiente = ambiente == DFeTipoAmbiente.Producao ? 1 : 2;
			Versao = versao;
			xServ = "STATUS";
		}

		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }

		[XmlElement(ElementName = "tpAmb", Order = 0)]
		public int Ambiente { get; set; }

		[XmlElement(ElementName = "xServ", Order = 1)]
		public string xServ { get; set; }
	}
}