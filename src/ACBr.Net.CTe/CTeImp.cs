// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-15-2016
// ***********************************************************************
// <copyright file="CTeImp.cs" company="ACBr.Net">
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

using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;
using PropertyChanged;

namespace ACBr.Net.CTe
{
	[ImplementPropertyChanged]
	public sealed class CTeImp : GenericClone<CTeImp>
	{
		#region Constructors

		public CTeImp()
		{
			ICMSUFFim = new CTeICMSUFFim();
			ICMS = new CTeICMS();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement("ICMS", Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeICMS ICMS { get; set; }

		[DFeElement(TipoCampo.De2, "vTotTrib", Id = "#250", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
		public decimal VTotTrib { get; set; }

		[DFeElement(TipoCampo.Str, "infAdFisco", Id = "#251", Min = 1, Max = 2000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string InfAdFisco { get; set; }

		[DFeElement("ICMSUFFim", Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public CTeICMSUFFim ICMSUFFim { get; set; }

		#endregion Propriedades

		#region Methods

		private bool ShouldSerializeICMSUFFim()
		{
			return ICMSUFFim.PICMSInterPart > 0;
		}

		#endregion Methods
	}
}