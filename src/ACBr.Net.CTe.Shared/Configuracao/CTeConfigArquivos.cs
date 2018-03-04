// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-11-2017
//
// Last Modified By : RFTD
// Last Modified On : 06-11-2017
// ***********************************************************************
// <copyright file="CTeConfigArquivos.cs" company="ACBr.Net">
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

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using ACBr.Net.Core;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe
{
	[TypeConverter(typeof(ACBrExpandableObjectConverter))]
	public sealed class CTeConfigArquivos : DFeArquivosConfigBase, INotifyPropertyChanged
	{
		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion Events

		#region Fields

		private CTeConfig parent;

		#endregion Fields

		#region Constructor

		/// <summary>
		/// Inicializa uma nova instancia da classe <see cref="CTeConfigArquivos"/>.
		/// </summary>
		internal CTeConfigArquivos(CTeConfig parent)
		{
			this.parent = parent;
			Salvar = false;
			AdicionarLiteral = false;
			PastaMensal = false;

			var path = Assembly.GetExecutingAssembly().GetPath();
			if (!path.IsEmpty())
			{
				PathCTe = Path.Combine(path, "CTe");
				PathLote = Path.Combine(path, "Lote");
				PathEvento = Path.Combine(path, "Evento");
			}
			else
			{
				PathCTe = string.Empty;
				PathLote = string.Empty;
				PathEvento = string.Empty;
			}
		}

		#endregion Constructor

		#region Properties

		/// <summary>
		/// Gets or sets the path n fe.
		/// </summary>
		/// <value>The path n fe.</value>
		[Browsable(true)]
		public string PathCTe { get; set; }

		/// <summary>
		/// Gets or sets the path lote.
		/// </summary>
		/// <value>The path lote.</value>
		[Browsable(true)]
		public string PathLote { get; set; }

		/// <summary>
		/// Gets or sets the path lote.
		/// </summary>
		/// <value>The path lote.</value>
		[Browsable(true)]
		public string PathEvento { get; set; }

		#endregion Properties

		#region Methods

		public string GetPathCTe(DateTime data)
		{
			var dir = string.IsNullOrEmpty(PathCTe.Trim()) ? parent.Geral.PathSalvar : PathCTe;

			if (PastaMensal)
				dir = Path.Combine(dir, data.ToString("yyyyMM"));

			if (AdicionarLiteral && !dir.EndsWith("CTe"))
				dir = Path.Combine(dir, "CTe");

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			return dir;
		}

		public string GetPathLote()
		{
			var dir = string.IsNullOrEmpty(PathLote.Trim()) ? parent.Geral.PathSalvar : PathLote;

			if (PastaMensal)
				dir = Path.Combine(dir, DateTime.Now.ToString("yyyyMM"));

			if (AdicionarLiteral && !dir.EndsWith("Lote"))
				dir = Path.Combine(dir, "Lote");

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			return dir;
		}

		public string GetPathEvento(DateTime data)
		{
			var dir = string.IsNullOrEmpty(PathEvento.Trim()) ? parent.Geral.PathSalvar : PathEvento;

			if (PastaMensal)
				dir = Path.Combine(dir, data.ToString("yyyyMM"));

			if (AdicionarLiteral && !dir.EndsWith("Evento"))
				dir = Path.Combine(dir, "Evento");

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			return dir;
		}

		#endregion Methods
	}
}