// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-23-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-24-2016
// ***********************************************************************
// <copyright file="CTeFaturamento.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
	public sealed class CTeFaturamento : GenericClone<CTeFaturamento>, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Properties

		[DFeElement(TipoCampo.Str, "nFat", Id = "#386", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string NFat { get; set; }

		[DFeElement(TipoCampo.De2, "vOrig", Id = "#387", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
		public decimal VOrig { get; set; }

		[DFeElement(TipoCampo.De2, "vDesc", Id = "#388", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
		public decimal VDesc { get; set; }

		[DFeElement(TipoCampo.De2, "vLiq", Id = "#389", Min = 1, Max = 15, Ocorrencia = Ocorrencia.MaiorQueZero)]
		public decimal VLiq { get; set; }

		#endregion Properties
	}
}