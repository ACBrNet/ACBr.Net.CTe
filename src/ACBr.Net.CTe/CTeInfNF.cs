// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-16-2016
// ***********************************************************************
// <copyright file="CTeInfNF.cs" company="ACBr.Net">
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
using System;
using System.Xml.Serialization;

namespace ACBr.Net.CTe
{
	[ImplementPropertyChanged]
	public sealed class CTeInfNF : GenericClone<CTeInfNF>, ICTeInfDoc
	{
		#region Constructors

		public CTeInfNF()
		{
			Infos = new DFeCollection<object>();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Str, "nRoma", Id = "#263", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string NRoma { get; set; }

		[DFeElement(TipoCampo.Str, "nPed", Id = "#264", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string NPed { get; set; }

		[DFeElement(TipoCampo.Enum, "mod", Id = "#265", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DFeModeloNF Mod { get; set; }

		[DFeElement(TipoCampo.Str, "serie", Id = "#266", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string Serie { get; set; }

		[DFeElement(TipoCampo.StrNumber, "nDoc", Id = "#267", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
		public string NDoc { get; set; }

		[DFeElement(TipoCampo.Dat, "dEmi", Id = "#268", Min = 10, Max = 10, Ocorrencia = Ocorrencia.Obrigatoria)]
		public DateTime DEmi { get; set; }

		[XmlElement(Order = 6, ElementName = "vBC")]
		[DFeElement(TipoCampo.De2, "vBC", Id = "#269", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VBC { get; set; }

		[DFeElement(TipoCampo.De2, "vICMS", Id = "#270", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VICMS { get; set; }

		[DFeElement(TipoCampo.De2, "vBCST", Id = "#271", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VBCST { get; set; }

		[DFeElement(TipoCampo.De2, "vST", Id = "#272", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VST { get; set; }

		[DFeElement(TipoCampo.De2, "vProd", Id = "#273", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VProd { get; set; }

		[DFeElement(TipoCampo.De2, "vNF", Id = "#274", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
		public decimal VNF { get; set; }

		[DFeElement(TipoCampo.Int, "nCFOP", Id = "#275", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
		public int NCFOP { get; set; }

		[DFeElement(TipoCampo.De3, "nPeso", Id = "#276", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public decimal NPeso { get; set; }

		[DFeElement(TipoCampo.Str, "PIN", Id = "#277", Min = 2, Max = 9, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string PIN { get; set; }

		[DFeElement(TipoCampo.Dat, "dPrev", Id = "#278", Min = 10, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public DateTime? DPrev { get; set; }

		[DFeItem(typeof(CTeTUnidCarga), "infUnidCarga")]
		[DFeItem(typeof(CTeUnidadeTransp), "infUnidTransp")]
		public DFeCollection<IInfoUnidade> Infos { get; set; }

		#endregion Propriedades
	}
}