// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-21-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-21-2016
// ***********************************************************************
// <copyright file="CTeInfManu.cs" company="ACBr.Net">
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
using System.ComponentModel;

namespace ACBr.Net.CTe
{
	public enum CTeInfManu
	{
		[DFeEnum("01")]
		[Description("Certificado do expedidor para embarque de animal vivo")]
		AnimalVivo,

		[DFeEnum("02")]
		[Description("")]
		ArtigoPerigosoRequirido,

		[DFeEnum("03")]
		[Description("")]
		SomenteCargueira,

		[DFeEnum("04")]
		[Description("Artigo perigoso - declara��o do expedidor n�o requerida")]
		ArtigoPerigosoNaoRequirido,

		[DFeEnum("05")]
		[Description("Artigo perigoso em quantidade isenta")]
		ArtigoPerigosoIsenta,

		[DFeEnum("06")]
		[Description("Gelo seco para refrigera��o (especificar no campo observa��es a quantidade)")]
		GeloSeco,

		[DFeEnum("07")]
		[Description("N�o restrito (especificar a Disposi��o Especial no campo observa��es)")]
		NaoRestrito,

		[DFeEnum("08")]
		[Description("Artigo perigoso em carga consolidada(especificar a quantidade no campo observa��es)")]
		ArtigoPerigosoCarga,

		[DFeEnum("09")]
		[Description("Autoriza��o da autoridade governamental anexa(especificar no campo observa��es)")]
		AutorizacaoGoverno,

		[DFeEnum("10")]
		[Description("Baterias de �ons de l�tio em conformidade com a Se��o II da PI965 � CAO")]
		BateriasIonsPI965,

		[DFeEnum("11")]
		[Description("Baterias de �ons de l�tio em conformidade com a Se��o II da PI966 � CAO")]
		BateriasIonsPI966,

		[DFeEnum("12")]
		[Description("Baterias de �ons de l�tio em conformidade com a Se��o II da PI967 � CAO")]
		BateriasIonsPI967,

		[DFeEnum("13")]
		[Description("Baterias de �ons de l�tio em conformidade com a Se��o II da PI968 � CAO")]
		BateriasIonsPI968,

		[DFeEnum("14")]
		[Description("Baterias de �ons de l�tio em conformidade com a Se��o II da PI969 � CAO")]
		BateriasIonsPI969,

		[DFeEnum("15")]
		[Description("Baterias de �ons de l�tio em conformidade com a Se��o II da PI970 � CAO")]
		BateriasIonsPI970,

		[DFeEnum("99")]
		[Description("Outro (especificar no campo observa��es)")]
		Outro,
	}
}