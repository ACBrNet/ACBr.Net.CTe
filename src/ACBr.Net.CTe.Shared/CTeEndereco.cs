// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-14-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-14-2016
// ***********************************************************************
// <copyright file="CTeEndereco.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
	public sealed class CTeEndereco : GenericClone<CTeEndereco>, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Properties

		[DFeElement(TipoCampo.Str, "xLgr", Id = "#046", Min = 2, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XLgr { get; set; }

		[DFeElement(TipoCampo.Str, "nro", Id = "#047", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Nro { get; set; }

		[DFeElement(TipoCampo.Str, "xCpl", Id = "#048", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XCpl { get; set; }

		[DFeElement(TipoCampo.Str, "xBairro", Id = "#049", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XBairro { get; set; }

		[DFeElement(TipoCampo.Int, "cMun", Id = "#050", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
		public int CMun { get; set; }

		[DFeElement(TipoCampo.Str, "xMun", Id = "#051", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XMun { get; set; }

		[DFeElement(TipoCampo.StrNumber, "CEP", Id = "#052", Min = 8, Max = 8, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CEP { get; set; }

		[DFeElement(TipoCampo.Enum, "UF", Id = "#053", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DFeSiglaUF UF { get; set; }

		[DFeElement(TipoCampo.Int, "cPais", Id = "#054", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
		public int CPais { get; set; }

		[DFeElement(TipoCampo.Str, "xPais", Id = "#055", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XPais { get; set; }

		#endregion Properties
	}
}