// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-07-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-07-2016
// ***********************************************************************
// <copyright file="CTeRefNF.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Serializer;
using System;
using System.ComponentModel;

namespace ACBr.Net.CTe
{
	public sealed class CTeRefNF : GenericClone<CTeRefNF>, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Propriedades

		[DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "#399", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CPF { get; set; }

		[DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "#400", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string CNPJ { get; set; }

		[DFeElement(TipoCampo.Str, "mod", Id = "#401", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Mod { get; set; }

		[DFeElement(TipoCampo.Int, "serie", Id = "#402", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
		public int Serie { get; set; }

		[DFeElement(TipoCampo.Int, "subserie", Id = "#403", Min = 1, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public int? Subserie { get; set; }

		[DFeElement(TipoCampo.Int, "nro", Id = "#404", Min = 1, Max = 6, Ocorrencia = Ocorrencia.Obrigatoria)]
		public int Nro { get; set; }

		[DFeElement(TipoCampo.De2, "valor", Id = "#405", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal Valor { get; set; }

		[DFeElement(TipoCampo.Dat, "dEmi", Id = "#406", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DateTime DEmi { get; set; }

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

		#endregion Methods
	}
}