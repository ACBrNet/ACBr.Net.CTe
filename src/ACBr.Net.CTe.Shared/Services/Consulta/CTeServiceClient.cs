// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 03-04-2018
//
// Last Modified By : RFTD
// Last Modified On : 03-04-2018
// ***********************************************************************
// <copyright file="CTeServiceClient.cs" company="ACBr.Net">
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    public abstract class CTeServiceClient<T> : DFeSoap12ServiceClientBase<T> where T : class
    {
        #region Fields

        protected readonly object serviceLock;

        #endregion Fields

        #region Constructors

        protected CTeServiceClient(CTeConfig config, ServicoCTe service, X509Certificate2 certificado = null) :
            base(CTeServiceManager.GetServiceAndress(config.Geral.VersaoDFe, config.WebServices.UF, service, config.WebServices.Ambiente),
                config.WebServices.TimeOut, certificado)
        {
            serviceLock = new object();
            Config = config;
        }

        #endregion Constructors

        #region Properties

        public CTeConfig Config { get; }

        public SchemaCTe Schema { get; protected set; }

        public string ArquivoEnvio { get; protected set; }

        public string ArquivoResposta { get; protected set; }

        public string EnvelopeSoap { get; protected set; }

        public string RetornoWS { get; protected set; }

        #endregion Properties

        #region Methods

        protected virtual CTeWsCabecalho DefineHeader()
        {
            var versao = Config.Geral.VersaoDFe.GetDescription();
            return new CTeWsCabecalho
            {
                CUf = (int)Config.WebServices.UF,
                VersaoDados = versao,
            };
        }

        protected virtual void ValidateMessage(string xml)
        {
            var schemaFile = Config.Arquivos.GetSchema(Schema);
            XmlSchemaValidation.ValidarXml(xml, schemaFile, out var erros, out string[] _);

            Guard.Against<ACBrDFeException>(erros.Any(), "Erros de validação do xml." +
                                                         $"{(Config.Geral.ExibirErroSchema ? Environment.NewLine + erros.AsString() : "")}");
        }

        /// <summary>
        /// Salvar o arquivo xml do CTe no disco de acordo com as propriedades.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="conteudoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="data"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected void GravarCTe(string conteudoArquivo, string nomeArquivo, DateTime data, string cnpj, ModeloCTe modelo)
        {
            if (!Config.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + conteudoArquivo;
            nomeArquivo = Path.Combine(Config.Arquivos.GetPathCTe(data, cnpj, modelo), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        /// <summary>
        /// Salvar o arquivo xml do Evento no disco de acordo com as propriedades.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="conteudoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="data"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected void GravarEvento(string conteudoArquivo, string nomeArquivo, CTeTipoEvento evento, DateTime data, string cnpj)
        {
            if (!Config.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + conteudoArquivo;
            nomeArquivo = Path.Combine(Config.Arquivos.GetPathEvento(evento, cnpj, data), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        /// <summary>
        /// Salvar o arquivo xml no disco de acordo com as propriedades.
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="conteudoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="data"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected void GravarSoap(string conteudoArquivo, string nomeArquivo)
        {
            if (Config.WebServices.Salvar == false) return;

            nomeArquivo = Path.Combine(Config.Arquivos.PathSalvar, nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        /// <inheritdoc />
        protected override void BeforeSendDFeRequest(string message)
        {
            EnvelopeSoap = message;
            GravarSoap(message, $"{DateTime.Now:yyyyMMddHHmmsszzz}_{ArquivoEnvio}_env.xml");
        }

        /// <inheritdoc />
        protected override void AfterReceiveDFeReply(string message)
        {
            RetornoWS = message;
            GravarSoap(message, $"{DateTime.Now:yyyyMMddHHmmsszzz}_{ArquivoResposta}_ret.xml");
        }

        #endregion Methods
    }
}