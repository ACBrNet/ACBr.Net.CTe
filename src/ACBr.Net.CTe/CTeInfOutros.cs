// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-16-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-17-2016
// ***********************************************************************
// <copyright file="CTeInfOutros.cs" company="ACBr.Net">
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
using System;
using System.Xml.Serialization;

namespace ACBr.Net.CTe
{
	[ImplementPropertyChanged]
	public sealed class CTeInfOutros : GenericClone<CTeInfOutros>, ICTeInfDoc
	{
		#region Constructors

		public CTeInfOutros()
		{
			Infos = new DFeCollection<IInfoUnidade>();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Enum, "tpDoc", Id = "#320", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
		public CTeTipoDocumento TpDoc { get; set; }

		[DFeElement(TipoCampo.Str, "descOutros", Id = "#321", Min = 1, Max = 100, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string DescOutros { get; set; }

		[DFeElement(TipoCampo.Str, "nDoc", Id = "#322", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string NDoc { get; set; }

		[XmlElement(Order = 3, ElementName = "dEmi")]
		[DFeElement(TipoCampo.Dat, "dEmi", Id = "#323", Min = 10, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public DateTime? DEmi { get; set; }

		[DFeElement(TipoCampo.De2, "vDocFisc", Id = "#324", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public decimal? VDocFisc { get; set; }

		[DFeElement(TipoCampo.Dat, "dPrev", Id = "#325", Min = 10, Max = 10, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public DateTime? DPrev { get; set; }

		[DFeItem(typeof(CTeTUnidCarga), "infUnidCarga")]
		[DFeItem(typeof(CTeUnidadeTransp), "infUnidTransp")]
		public DFeCollection<IInfoUnidade> Infos { get; set; }

		#endregion Propriedades
	}
}