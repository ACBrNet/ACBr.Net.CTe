// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-19-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-19-2016
// ***********************************************************************
// <copyright file="CTeRodoOcc.cs" company="ACBr.Net">
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
using System;
using System.ComponentModel;

namespace ACBr.Net.CTe
{
	public sealed class CTeRodoOcc : GenericClone<CTeRodoOcc>, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Constructors

		public CTeRodoOcc()
		{
			EmiOcc = new CTeEmiOcc();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Str, "serie", Id = "#07", Min = 1, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string Serie { get; set; }

		[DFeElement(TipoCampo.Int, "nOcc", Id = "#08", Min = 1, Max = 6, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public int NOcc { get; set; }

		[DFeElement(TipoCampo.Dat, "dEmi", Id = "#09", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DateTime DEmi { get; set; }

		[DFeElement("emiOcc", Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeEmiOcc EmiOcc { get; set; }

		#endregion Propriedades
	}
}