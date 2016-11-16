// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="ServiceManager.cs" company="ACBr.Net">
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

using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ACBr.Net.CTe.Services
{
	public static class ServiceManager
	{
		#region Constructors

		static ServiceManager()
		{
			Servicos = new Dictionary<CTeVersao, List<CTeServicos>>(2)
			{
				{CTeVersao.v200, new List<CTeServicos>(27)},
				{CTeVersao.v300, new List<CTeServicos>(27)}
			};

			Load();
		}

		#endregion Constructors

		#region Propriedades

		public static Dictionary<CTeVersao, List<CTeServicos>> Servicos { get; }

		#endregion Propriedades

		#region Methods

		public static ServiceInfo GetServiceAndress(CTeVersao versao, DFeCodUF uf, TipoUrlServico tipo, DFeTipoAmbiente ambiente)
		{
			var services = Servicos[versao].SingleOrDefault(x => x.UF == uf);

			Guard.Against<ACBrException>(services == null, "UF não encontrada no arquivo de serviços");

			switch (ambiente)
			{
				case DFeTipoAmbiente.Producao:
					return services.Producao[tipo];

				case DFeTipoAmbiente.Homologacao:
					return services.Homologacao[tipo];

				default:
					throw new ArgumentOutOfRangeException(nameof(ambiente), ambiente, null);
			}
		}

		/// <summary>
		/// Salva o arquivo de serviços.
		/// </summary>
		/// <param name="path">Caminho para salvar o arquivo</param>
		public static void Save(string path = "services.cte")
		{
			Guard.Against<ArgumentNullException>(path == null, "Path invalido.");

			if (File.Exists(path))
			{
				File.Delete(path);
			}

			using (var fileStream = new FileStream(path, FileMode.CreateNew))
			{
				Save(fileStream);
			}
		}

		/// <summary>
		/// Salva o arquivo de serviços.
		/// </summary>
		/// <param name="stream">O stream.</param>
		public static void Save(Stream stream)
		{
			using (var zip = new GZipStream(stream, CompressionMode.Compress))
			{
				var formatter = new BinaryFormatter();
				formatter.Serialize(zip, Servicos.ToArray());
			}
		}

		/// <summary>
		/// Carrega o arquivo de serviços.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="clean">if set to <c>true</c> [clean].</param>
		public static void Load(string path = "")
		{
			byte[] buffer = null;
			if (path.IsEmpty())
			{
				buffer = Properties.Resources.Services;
			}
			else if (File.Exists(path))
			{
				buffer = File.ReadAllBytes(path);
			}

			Guard.Against<ArgumentException>(buffer == null, "Arquivo de serviços não encontrado");

			using (var stream = new MemoryStream(buffer))
			{
				Load(stream);
			}
		}

		/// <summary>
		/// Carrega o arquivo de serviços.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="clean">if set to <c>true</c> [clean].</param>
		public static void Load(Stream stream)
		{
			Guard.Against<ArgumentException>(stream == null, "Arquivo de serviços não encontrado");

			using (var zip = new GZipStream(stream, CompressionMode.Decompress))
			{
				var formatter = new BinaryFormatter();
				var itens = (KeyValuePair<CTeVersao, List<CTeServicos>>[])formatter.Deserialize(zip);

				Servicos.Clear();
				foreach (var item in itens)
				{
					Servicos.Add(item.Key, item.Value);
				}
			}
		}

		#endregion Methods
	}
}