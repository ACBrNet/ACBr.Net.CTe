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
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CTeServiceClient<T> : DFeSoap12ServiceClientBase<T> where T : class
    {
        #region Fields

        protected readonly object serviceLock;

        #endregion Fields

        #region Constructors

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="config"></param>
        ///  <param name="service"></param>
        ///  <param name="certificado"></param>
        protected CTeServiceClient(CTeConfig config, ServicoCTe service, X509Certificate2 certificado = null) :
            base(CTeServiceManager.GetServiceAndress(config.Geral.VersaoDFe, config.WebServices.UF, service, config.WebServices.Ambiente),
                config.WebServices.TimeOut, certificado)
        {
            serviceLock = new object();
            Configuracoes = config;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///
        /// </summary>
        public CTeConfig Configuracoes { get; }

        /// <summary>
        ///
        /// </summary>
        public SchemaCTe Schema { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public string ArquivoEnvio { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public string ArquivoResposta { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public string EnvelopeSoap { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public string RetornoWS { get; protected set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retorna o cabeçalho para usar no envelope SOAP.
        /// </summary>
        /// <returns></returns>
        protected virtual CTeWsCabecalho DefineHeader()
        {
            var versao = Configuracoes.Geral.VersaoDFe.GetDescription();
            return new CTeWsCabecalho
            {
                CUf = (int)Configuracoes.WebServices.UF,
                VersaoDados = versao,
            };
        }

        /// <summary>
        /// Função para validar a menssagem a ser enviada para o webservice.
        /// </summary>
        /// <param name="xml"></param>
        protected virtual void ValidateMessage(string xml)
        {
            ValidateMessage(xml, Schema);
        }

        /// <summary>
        /// Função para validar a menssagem a ser enviada para o webservice.
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="schema"></param>
        protected virtual void ValidateMessage(string xml, SchemaCTe schema)
        {
            var schemaFile = Configuracoes.Arquivos.GetSchema(schema);
            XmlSchemaValidation.ValidarXml(xml, schemaFile, out var erros, out _);

            Guard.Against<ACBrDFeValidationException>(erros.Any(), "Erros de validação do xml." +
                                                         $"{(Configuracoes.Geral.ExibirErroSchema ? Environment.NewLine + erros.AsString() : "")}");
        }

        /// <summary>
        /// Salvar o arquivo xml do CTe no disco de acordo com as propriedades.
        /// </summary>
        /// <param name="conteudoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="data"></param>
        /// <param name="cnpj"></param>
        /// <param name="modelo"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected void GravarCTe(string conteudoArquivo, string nomeArquivo, DateTime data, string cnpj, ModeloCTe modelo)
        {
            if (!Configuracoes.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + conteudoArquivo;
            nomeArquivo = Path.Combine(Configuracoes.Arquivos.GetPathCTe(data, cnpj, modelo), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        /// <summary>
        /// Salvar o arquivo xml do Evento no disco de acordo com as propriedades.
        /// </summary>
        /// <param name="conteudoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="evento"></param>
        /// <param name="data"></param>
        /// <param name="cnpj"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected void GravarEvento(string conteudoArquivo, string nomeArquivo, CTeTipoEvento evento, DateTime data, string cnpj)
        {
            if (!Configuracoes.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + conteudoArquivo;
            nomeArquivo = Path.Combine(Configuracoes.Arquivos.GetPathEvento(evento, cnpj, data), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        /// <summary>
        /// Salva o arquivo xml da inutilização no disco de acordo com as propriedades.
        /// </summary>
        /// <param name="conteudoArquivo"></param>
        /// <param name="nomeArquivo"></param>
        /// <param name="data"></param>
        /// <param name="cnpj"></param>
        protected void GravarInutilizacao(string conteudoArquivo, string nomeArquivo, DateTime data, string cnpj)
        {
            if (!Configuracoes.Arquivos.Salvar) return;

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + conteudoArquivo;
            nomeArquivo = Path.Combine(Configuracoes.Arquivos.GetPathInu(data, cnpj), nomeArquivo);
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
            if (Configuracoes.WebServices.Salvar == false) return;

            nomeArquivo = Path.Combine(Configuracoes.Arquivos.PathSalvar, nomeArquivo);
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