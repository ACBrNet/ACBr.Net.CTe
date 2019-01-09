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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ACBr.Net.Core.Extensions;
using ACBr.Net.CTe.Configuracao;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    public abstract class CTeServiceClient<TService> : DFeServiceClient<CTeConfig, ACBrCTe, CTeConfigGeral,
        CTeVersao, CTeConfigWebServices, CTeConfigCertificados, CTeConfigArquivos, SchemaCTe, TService> where TService : class
    {
        #region Constructors

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="config"></param>
        ///  <param name="service"></param>
        ///  <param name="certificado"></param>
        protected CTeServiceClient(CTeConfig config, ServicoCTe service, X509Certificate2 certificado = null) :
            base(config, CTeServiceManager.GetServiceAndress(config.Geral.VersaoDFe, (DFeSiglaUF)config.WebServices.UF,
                    service, config.Geral.FormaEmissao, config.WebServices.Ambiente), certificado)
        {
        }

        #endregion Constructors

        #region Propriedades

        /// <summary>
        /// Retorna o certificado usado no serviço.
        /// </summary>
        /// <value>The certificado.</value>
        public X509Certificate2 Certificado => ClientCredentials?.ClientCertificate.Certificate ??
                                               Configuracoes.Certificados.ObterCertificado();

        #endregion Propriedades

        #region Methods

        /// <summary>
        /// Retorna o cabeçalho para usar no envelope SOAP.
        /// </summary>
        /// <returns></returns>
        protected virtual DFeWsCabecalho DefineHeader()
        {
            var versao = Configuracoes.Geral.VersaoDFe.GetDescription();
            return new DFeWsCabecalho
            {
                CUf = (int)Configuracoes.WebServices.UF,
                VersaoDados = versao,
            };
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

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + conteudoArquivo.RemoverDeclaracaoXml();
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

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + conteudoArquivo.RemoverDeclaracaoXml();
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

            conteudoArquivo = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + conteudoArquivo.RemoverDeclaracaoXml();
            nomeArquivo = Path.Combine(Configuracoes.Arquivos.GetPathInu(data, cnpj), nomeArquivo);
            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        #endregion Methods
    }
}