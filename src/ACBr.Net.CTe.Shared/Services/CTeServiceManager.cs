// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 06-22-2018
// ***********************************************************************
// <copyright file="CTeServiceManager.cs" company="ACBr.Net">
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
using System.IO;
using System.Linq;
using System.Reflection;
using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    /// <summary>
    /// Classe responsavel pelo gerenciamento das url dos serviço da CTe.
    /// </summary>
    public static class CTeServiceManager
    {
        #region Constructors

        static CTeServiceManager()
        {
            Servicos = new DFeServices<TipoServicoCTe, CTeVersao>
            {
                Webservices = new DFeCollection<DFeServiceInfo<TipoServicoCTe, CTeVersao>>
                {
                    new DFeServiceInfo<TipoServicoCTe, CTeVersao>
                    {
                        Versao = CTeVersao.v300,
                        Tipo = DFeTipoServico.CTe,
                        TipoEmissao = DFeTipoEmissao.Normal
                    },
                    new DFeServiceInfo<TipoServicoCTe, CTeVersao>
                    {
                        Versao = CTeVersao.v300,
                        Tipo = DFeTipoServico.CTe,
                        TipoEmissao = DFeTipoEmissao.DPEC
                    },
                    new DFeServiceInfo<TipoServicoCTe, CTeVersao>
                    {
                        Versao = CTeVersao.v300,
                        Tipo = DFeTipoServico.CTe,
                        TipoEmissao = DFeTipoEmissao.SVCRS
                    },
                    new DFeServiceInfo<TipoServicoCTe, CTeVersao>
                    {
                        Versao = CTeVersao.v300,
                        Tipo = DFeTipoServico.CTe,
                        TipoEmissao = DFeTipoEmissao.SVCSP
                    }
                }
            };

            var dataSource = (from DFeSiglaUF value in Enum.GetValues(typeof(DFeSiglaUF))
                              where !value.IsIn(DFeSiglaUF.EX, DFeSiglaUF.AN, DFeSiglaUF.SU)
                              orderby value.GetDescription()
                              select value).ToArray();

            var servicoCTes = (from TipoServicoCTe value in Enum.GetValues(typeof(TipoServicoCTe)) select value).ToArray();

            foreach (var serviceInfo in Servicos.Webservices)
            {
                foreach (var siglaUF in dataSource)
                {
                    var ambiente = serviceInfo.Ambientes.AddNew();
                    ambiente.UF = siglaUF;
                    ambiente.Ambiente = DFeTipoAmbiente.Homologacao;

                    foreach (var servicoCTe in servicoCTes)
                    {
                        ambiente.Enderecos.Add(servicoCTe, "");
                    }

                    ambiente = serviceInfo.Ambientes.AddNew();
                    ambiente.UF = siglaUF;
                    ambiente.Ambiente = DFeTipoAmbiente.Producao;

                    foreach (var servicoCTe in servicoCTes)
                    {
                        ambiente.Enderecos.Add(servicoCTe, "");
                    }
                }
            }

            Load();
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        /// Lista de serviços CTe.
        /// </summary>
        public static DFeServices<TipoServicoCTe, CTeVersao> Servicos { get; private set; }

        #endregion Propriedades

        #region Methods

        /// <summary>
        /// Retorna a url do serviço de acordo com os dados passados.
        /// </summary>
        /// <param name="versao">Versão da CTe</param>
        /// <param name="uf">UF do serviço</param>
        /// <param name="tipo">Tipo de serviço</param>
        /// <param name="tipoEmissao"></param>
        /// <param name="ambiente">Ambiente</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetServiceAndress(CTeVersao versao, DFeSiglaUF uf, TipoServicoCTe tipo, DFeTipoEmissao tipoEmissao, DFeTipoAmbiente ambiente)
        {
            switch (tipo)
            {
                case TipoServicoCTe.DistribuicaoDFe:
                    switch (ambiente)
                    {
                        case DFeTipoAmbiente.Homologacao: return @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmxhttps://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx";
                        default: return @"https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmxhttps://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx";
                    }

                default:
                    var service = Servicos[versao, tipoEmissao];
                    Guard.Against<ACBrException>(service == null, "Versão ou tipo de emissão não encontrada no arquivo de serviços.");
                    return service[ambiente, uf]?[tipo];
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
            Servicos.Save(stream);
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
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("ACBr.Net.CTe.Resources.services.cte"))
                {
                    if (stream != null)
                    {
                        buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                    }
                }
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

            Servicos = DFeServices<TipoServicoCTe, CTeVersao>.Load(stream);
        }

        /// <summary>
        /// Função para importar endereços no padrão de Ini do ACBr.
        /// </summary>
        /// <param name="path">Caminho para o ini</param>
        public static void ImportIniACBr(string path = "")
        {
            using (var stream = File.OpenRead(path))
            {
                ImportIniACBr(stream);
            }
        }

        /// <summary>
        /// Função para importar endereços no padrão de Ini do ACBr.
        /// </summary>
        /// <param name="stream"></param>
        public static void ImportIniACBr(Stream stream)
        {
            var ini = ACBrIniFile.Load(stream);

            var dataSource = (from DFeSiglaUF value in Enum.GetValues(typeof(DFeSiglaUF))
                              where !value.IsIn(DFeSiglaUF.EX, DFeSiglaUF.AN, DFeSiglaUF.SU)
                              orderby value.GetDescription()
                              select value).ToArray();

            var servicoCTes =
                (from TipoServicoCTe value in Enum.GetValues(typeof(TipoServicoCTe)) select value).ToArray();

            var getVersion = new Func<CTeVersao, string>(versao =>
            {
                switch (versao)
                {
                    case CTeVersao.v200: return "2.00";
                    case CTeVersao.v300: return "3.00";
                    default: throw new ArgumentOutOfRangeException(nameof(versao), versao, null);
                }
            });

            // Emissão Normal
            foreach (var codUf in dataSource)
            {
                var sectionName = $"CTe_{codUf}_H";

                var sessao = ini[sectionName];
                var sessaoUsar = ini[sectionName];

                while (sessaoUsar.ContainsKey("Usar"))
                {
                    sessaoUsar = ini[sessaoUsar["Usar"]];
                }

                foreach (var service in servicoCTes)
                {
                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao.ContainsKey(key) ? sessao[key] : sessaoUsar.ContainsKey(key) ? sessaoUsar[key] : "";

                    Servicos[CTeVersao.v300, DFeTipoEmissao.Normal][DFeTipoAmbiente.Homologacao, codUf].Enderecos[service] = url;
                }

                sectionName = $"CTe_{codUf.GetDescription()}_P";
                sessao = ini[sectionName];
                sessaoUsar = ini[sectionName];

                while (sessaoUsar.ContainsKey("Usar"))
                {
                    sessaoUsar = ini[sessaoUsar["Usar"]];
                }

                foreach (var service in servicoCTes)
                {
                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao.ContainsKey(key) ? sessao[key] :
                        sessaoUsar.ContainsKey(key) ? sessaoUsar[key] : "";

                    Servicos[CTeVersao.v300, DFeTipoEmissao.Normal][DFeTipoAmbiente.Producao, codUf]
                        .Enderecos[service] = url;
                }
            }

            //Emissão DPEC
            foreach (var codUf in dataSource)
            {
                var sectionName = string.Empty;

                switch (codUf)
                {
                    case DFeSiglaUF.PE:
                    case DFeSiglaUF.SP:
                    case DFeSiglaUF.MS:
                    case DFeSiglaUF.MT:
                        sectionName = "CTe_SVC-RS_";
                        break;

                    default:
                        sectionName = "CTe_SVC-SP_";
                        break;
                }

                var sessao = ini[sectionName + "H"];
                foreach (var service in servicoCTes)
                {
                    if (service == TipoServicoCTe.RecepcaoEventoAN) continue;

                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao[key];

                    Servicos[CTeVersao.v300, DFeTipoEmissao.DPEC][DFeTipoAmbiente.Homologacao, codUf][service] = url;
                }

                sessao = ini[sectionName + "P"];
                foreach (var service in servicoCTes)
                {
                    if (service == TipoServicoCTe.RecepcaoEventoAN) continue;

                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao[key];

                    Servicos[CTeVersao.v300, DFeTipoEmissao.DPEC][DFeTipoAmbiente.Producao, codUf][service] = url;
                }
            }

            //Emissão SVCRS
            foreach (var codUf in dataSource)
            {
                var sectionName = "CTe_SVC-RS_";

                var sessao = ini[sectionName + "H"];
                foreach (var service in servicoCTes)
                {
                    if (service == TipoServicoCTe.RecepcaoEventoAN) continue;

                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao[key];

                    Servicos[CTeVersao.v300, DFeTipoEmissao.SVCRS][DFeTipoAmbiente.Homologacao, codUf][service] = url;
                }

                sessao = ini[sectionName + "P"];
                foreach (var service in servicoCTes)
                {
                    if (service == TipoServicoCTe.RecepcaoEventoAN) continue;

                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao[key];

                    Servicos[CTeVersao.v300, DFeTipoEmissao.SVCRS][DFeTipoAmbiente.Producao, codUf][service] = url;
                }
            }

            //Emissão SVCSP
            foreach (var codUf in dataSource)
            {
                var sectionName = "CTe_SVC-SP_";

                var sessao = ini[sectionName + "H"];
                foreach (var service in servicoCTes)
                {
                    if (service == TipoServicoCTe.RecepcaoEventoAN) continue;

                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao[key];

                    Servicos[CTeVersao.v300, DFeTipoEmissao.SVCSP][DFeTipoAmbiente.Homologacao, codUf][service] = url;
                }

                sessao = ini[sectionName + "P"];
                foreach (var service in servicoCTes)
                {
                    if (service == TipoServicoCTe.RecepcaoEventoAN) continue;

                    var key = $"{service.GetDescription()}_{getVersion(CTeVersao.v300)}";
                    var url = sessao[key];

                    Servicos[CTeVersao.v300, DFeTipoEmissao.SVCSP][DFeTipoAmbiente.Producao, codUf][service] = url;
                }
            }
        }

        #endregion Methods
    }
}