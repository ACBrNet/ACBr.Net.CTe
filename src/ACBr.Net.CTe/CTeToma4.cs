// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-14-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-14-2016
// ***********************************************************************
// <copyright file="CTeToma4.cs" company="ACBr.Net">
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
using ACBr.Net.CTe.Properties;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;
using PropertyChanged;

namespace ACBr.Net.CTe
{
	[ImplementPropertyChanged]
	public sealed class CTeToma4 : GenericClone<CTeToma4>, ICTeTomador
	{
		#region Constructors

		public CTeToma4()
		{
			EnderToma = new CTeEndereco();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Enum, "toma", Id = "#038", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeTomador Toma { get; set; }

		[DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "#039", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CPF { get; set; }

		[DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "#040", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CNPJ { get; set; }

		[DFeElement(TipoCampo.Custom, "IE", Id = "#041", Min = 0, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string IE { get; set; }

		[DFeElement(TipoCampo.Str, "xNome", Id = "#042", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XNome { get; set; }

		[DFeElement(TipoCampo.Str, "xFant", Id = "#043", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XFant { get; set; }

		[DFeElement(TipoCampo.StrNumber, "fone", Id = "#044", Min = 7, Max = 12, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Fone { get; set; }

		[DFeElement("enderToma", Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeEndereco EnderToma { get; set; }

		[DFeElement(TipoCampo.Str, "email", Id = "#043", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string Email { get; set; }

		#endregion Propriedades

		#region Methods

		private bool ShouldSerializeCPF()
		{
			return CNPJ.IsEmpty();
		}

		private bool ShouldSerializeCNPJ()
		{
			return CPF.IsEmpty();
		}

		private string SerializeIE()
		{
			return IE.Trim().ToUpper() == Resources.CTeIEIsento ? IE.Trim().ToUpper() : IE.OnlyNumbers();
		}

		private object DeserializeIE(string value)
		{
			return value;
		}

		#endregion Methods
	}
}