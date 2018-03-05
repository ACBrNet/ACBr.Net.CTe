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
using ACBr.Net.DFe.Core.Service;

namespace ACBr.Net.CTe.Services
{
    public abstract class CTeServiceClient<T> : DFeSoap12ServiceClientBase<T> where T : class
    {
        #region Fields

        protected readonly object serviceLock;
        protected string xmlEnvio;
        protected string xmlRetorno;
        protected string xmlFileName;

        #endregion Fields

        #region Constructors

        protected CTeServiceClient(CTeConfig config, ServicoCTe service, X509Certificate2 certificado = null) :
            base(CTeServiceManager.GetServiceAndress(config.Geral.VersaoCTe, config.WebServices.Uf, service, config.WebServices.Ambiente),
                config.WebServices.TimeOut, certificado)
        {
            serviceLock = new object();
            Config = config;
        }

        #endregion Constructors

        #region Properties

        public CTeConfig Config { get; }

        #endregion Properties

        #region Methods

        protected void GravarArquivoEmDisco(string conteudoArquivo, string nomeArquivo)
        {
            if (Config.Geral.Salvar == false) return;

            var path = Config.Arquivos.GetPathLote();
            var filePath = Path.Combine(path, nomeArquivo);
            if (Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(filePath, conteudoArquivo, Encoding.UTF8);
        }

        protected override void BeforeSendDFeRequest(string message)
        {
            xmlEnvio = message;
            GravarArquivoEmDisco(message, $"{xmlFileName}_env.xml");
        }

        protected override void AfterReceiveDFeReply(string message)
        {
            xmlRetorno = message;
            GravarArquivoEmDisco(message, $"{xmlFileName}_ret.xml");
        }

        #endregion Methods
    }
}