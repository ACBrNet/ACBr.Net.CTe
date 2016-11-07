// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-22-2016
// ***********************************************************************
// <copyright file="CTeTrafMut.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Serializer;
using PropertyChanged;
using System.Xml.Serialization;

namespace ACBr.Net.CTe
{
	[ImplementPropertyChanged]
	public sealed class CTeTrafMut : GenericClone<CTeTrafMut>
	{
		#region Constructors

		public CTeTrafMut()
		{
			FerroEnv = new DFeCollection<CTeFerrovEnv>();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Enum, "respFat", Id = "#04", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeTrafegoMutuo RespFat { get; set; }

		[DFeElement(TipoCampo.Enum, "ferrEmi", Id = "#05", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeTrafegoMutuo FerrEmi { get; set; }

		[DFeElement(TipoCampo.De2, "vFrete", Id = "#06", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VFrete { get; set; }

		[DFeElement(TipoCampo.Str, "chCTeFerroOrigem", Id = "#07", Min = 44, Max = 44, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string ChCTeFerroOrigem { get; set; }

		[XmlElement("ferroEnv", ElementName = "ferroEnv")]
		public DFeCollection<CTeFerrovEnv> FerroEnv { get; set; }

		#endregion Propriedades
	}
}