// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-16-2016
// ***********************************************************************
// <copyright file="CTeTUnidCarga.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;
using PropertyChanged;

namespace ACBr.Net.CTe
{
	[ImplementPropertyChanged]
	public sealed class CTeTUnidCarga : GenericClone<CTeTUnidCarga>, IInfoUnidade
	{
		#region Constructors

		public CTeTUnidCarga()
		{
			LacUnidCarga = new DFeCollection<CTeLacre>();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Enum, "tpUnidCarga", Id = "#285", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DFeUnidCarga TpUnidCarga { get; set; }

		[DFeElement(TipoCampo.Str, "idUnidCarga", Id = "#286", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string IdUnidCarga { get; set; }

		[DFeElement("lacUnidCarga", Ocorrencia = Ocorrencia.Obrigatoria)]
		public DFeCollection<CTeLacre> LacUnidCarga { get; set; }

		[DFeElement(TipoCampo.De2, "qtdRat", Id = "#289", Min = 1, Max = 5, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public decimal? QtdRat { get; set; }

		#endregion Propriedades
	}
}