// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-22-2016
// ***********************************************************************
// <copyright file="CTeFerrovEnv.cs" company="ACBr.Net">
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
	public sealed class CTeFerrovEnv : GenericClone<CTeFerrovEnv>, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Constructors

		public CTeFerrovEnv()
		{
			EnderFerro = new CTeEndereco();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "#10", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CNPJ { get; set; }

		[DFeElement(TipoCampo.Str, "cInt", Id = "#11", Min = 1, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string CInt { get; set; }

		[DFeElement(TipoCampo.StrNumber, "IE", Id = "#12", Min = 2, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string IE { get; set; }

		[DFeElement(TipoCampo.Str, "xNome", Id = "#13", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string XNome { get; set; }

		[DFeElement("enderFerro", Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeEndereco EnderFerro { get; set; }

		#endregion Propriedades
	}
}