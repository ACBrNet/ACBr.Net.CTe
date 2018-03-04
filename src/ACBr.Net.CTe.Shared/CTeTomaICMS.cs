// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-24-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-24-2016
// ***********************************************************************
// <copyright file="CTeInfCteSub.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
	public sealed class CTeTomaICMS
	{
		#region Propriedades

		[DFeElement(TipoCampo.StrNumber, "refCte", Id = "#395", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string RefCte { get; set; }

		[DFeElement("refNF", Id = "#398", Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeRefNF RefNF { get; set; }

		[DFeElement(TipoCampo.StrNumber, "refNFe", Id = "#397", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string RefNFe { get; set; }

		#endregion Propriedades

		#region Methods

		private bool ShouldSerializeRefCte()
		{
			return !RefCte.IsEmpty() && RefNFe.IsEmpty();
		}

		private bool ShouldSerializeRefNF()
		{
			return RefCte.IsEmpty() && RefNFe.IsEmpty();
		}

		private bool ShouldSerializeRefNFe()
		{
			return RefCte.IsEmpty() && !RefNFe.IsEmpty();
		}

		#endregion Methods
	}
}