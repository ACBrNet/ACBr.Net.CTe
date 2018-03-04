// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-15-2016
// ***********************************************************************
// <copyright file="CTeRetira.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Serializer;
using System.Linq;

namespace ACBr.Net.CTe
{
	public sealed class CTeCompl : GenericClone<CTeCompl>, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Constructors

		public CTeCompl()
		{
			ObsFisco = new DFeCollection<CTeObsFisco>();
			ObsCont = new DFeCollection<CTeObsCont>();
			Entrega = new CTeComplEntrega();
			Fluxo = new CTeFluxo();
		}

		#endregion Constructors

		#region Propriedades

		[DFeElement(TipoCampo.Str, "xCaracAd", Id = "#060", Min = 1, Max = 15, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string XCaracAd { get; set; }

		[DFeElement(TipoCampo.Str, "xCaracSer", Id = "#061", Min = 1, Max = 30, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string XCaracSer { get; set; }

		[DFeElement(TipoCampo.Str, "xEmi", Id = "#062", Min = 1, Max = 20, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string XEmi { get; set; }

		[DFeElement("fluxo", Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public CTeFluxo Fluxo { get; set; }

		[DFeElement("Entrega", Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public CTeComplEntrega Entrega { get; set; }

		[DFeElement(TipoCampo.Str, "origCalc", Id = "#088", Min = 2, Max = 40, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string OrigCalc { get; set; }

		[DFeElement(TipoCampo.Str, "destCalc", Id = "#089", Min = 2, Max = 40, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string DestCalc { get; set; }

		[DFeElement(TipoCampo.Str, "xObs", Id = "#090", Min = 1, Max = 2000, Ocorrencia = Ocorrencia.NaoObrigatoria)]
		public string XObs { get; set; }

		[DFeElement("ObsCont")]
		public DFeCollection<CTeObsCont> ObsCont { get; set; }

		[DFeElement("ObsFisco")]
		public DFeCollection<CTeObsFisco> ObsFisco { get; set; }

		#endregion Propriedades

		#region Methods

		private bool ShouldSerializeFluxo()
		{
			return !Fluxo.XOrig.IsEmpty() || !Fluxo.XDest.IsEmpty() ||
				   !Fluxo.XRota.IsEmail() || Fluxo.Pass.Any();
		}

		private bool ShouldSerializeEntrega()
		{
			return !Fluxo.XOrig.IsEmpty() || !Fluxo.XDest.IsEmpty() ||
				   !Fluxo.XRota.IsEmail() || Fluxo.Pass.Any();
		}

		#endregion Methods
	}
}