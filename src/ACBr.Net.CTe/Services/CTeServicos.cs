// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="CTeServicos.cs" company="ACBr.Net">
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

using ACBr.Net.DFe.Core.Common;
using System;
using System.Collections.Generic;

namespace ACBr.Net.CTe.Services
{
	[Serializable]
	public sealed class CTeServicos
	{
		#region Constructors

		public CTeServicos()
		{
			Homologacao = new Dictionary<TipoUrlServico, string>(8)
			{
				{ TipoUrlServico.RecepcaoEvento, string.Empty },
				{ TipoUrlServico.CTeRecepcao, string.Empty },
				{ TipoUrlServico.CTeRetRecepcao, string.Empty },
				{ TipoUrlServico.CTeInutilizacao, string.Empty },
				{ TipoUrlServico.CTeConsultaProtocolo,string.Empty },
				{ TipoUrlServico.CTeStatusServico,string.Empty },
				{ TipoUrlServico.CTeConsultaCadastro,string.Empty },
				{ TipoUrlServico.RecepcaoEventoNacional, string.Empty }
			};
			Producao = new Dictionary<TipoUrlServico, string>(8)
			{
				{ TipoUrlServico.RecepcaoEvento, string.Empty },
				{ TipoUrlServico.CTeRecepcao, string.Empty },
				{ TipoUrlServico.CTeRetRecepcao, string.Empty },
				{ TipoUrlServico.CTeInutilizacao, string.Empty },
				{ TipoUrlServico.CTeConsultaProtocolo,string.Empty },
				{ TipoUrlServico.CTeStatusServico,string.Empty },
				{ TipoUrlServico.CTeConsultaCadastro,string.Empty },
				{ TipoUrlServico.RecepcaoEventoNacional, string.Empty }
			};
		}

		#endregion Constructors

		#region Propriedades

		public DFeCodUF UF { get; set; }

		public Dictionary<TipoUrlServico, string> Homologacao { get; private set; }

		public Dictionary<TipoUrlServico, string> Producao { get; private set; }

		#endregion Propriedades
	}
}