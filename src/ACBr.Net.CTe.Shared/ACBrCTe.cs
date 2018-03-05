// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 07-23-2016
//
// Last Modified By : RFTD
// Last Modified On : 07-23-2016
// ***********************************************************************
// <copyright file="ACBrCTe.cs" company="ACBr.Net">
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

#if !NETSTANDARD2_0

using System.Drawing;

#endif

using System.Text;
using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Logging;
using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe
{
#if !NETSTANDARD2_0

    [ToolboxBitmap(typeof(ACBrCTe), "ACBr.Net.CTe.ACBrCTe.bmp")]
#endif

    public sealed class ACBrCTe : ACBrComponent, IACBrLog
    {
        #region Internal Types

        private enum TipoArquivo
        {
            CTe,
            Evento
        }

        #endregion Internal Types

        #region Propriedades

        /// <summary>
        /// Retorna as configurações do ACBrCTe.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CTeConfig Configuracoes { get; private set; }

        /// <summary>
        /// Retorna a lista de CTes para processamento.
        /// </summary>
        [Browsable(false)]
        public CTeCollection Conhecimentos { get; private set; }

        #endregion Propriedades

        #region Methods

        /// <summary>
        /// Metodo para checar a situação da CTe pela chave.
        /// </summary>
        /// <returns>A situação do serviço de CTe</returns>
        public ConsultaCTeResposta Consultar(string chave)
        {
            Guard.Against<ArgumentNullException>(chave == null, nameof(chave));
            Guard.Against<ArgumentException>(chave.IsEmpty(), nameof(chave));

            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                var versao = Configuracoes.Geral.VersaoCTe.GetDescription();
                using (var cliente = new CTeConsultaServiceClient(Configuracoes, cert))
                {
                    var cabecalho = new CTeWsCabecalho
                    {
                        CUf = (int)Configuracoes.WebServices.Uf,
                        VersaoDados = versao,
                    };

                    var request = new StringBuilder();
                    request.Append($"<consSitCTe xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"{versao}\">");
                    request.Append($"<tpAmb>{(Configuracoes.WebServices.Ambiente == DFeTipoAmbiente.Producao ? 1 : 2)}</tpAmb>");
                    request.Append("<xServ>CONSULTAR</xServ>");
                    request.Append($"<chCTe>{chave}</chCTe>");
                    request.Append("</consSitCTe>");

                    var requestXml = request.ToString();
                    var ret = cliente.Consulta(cabecalho, requestXml, $"CTe_Consultar_{DateTime.Now:YYYYMMDDHHmmssfff}_{chave}");

                    return ret;
                }
            }
            catch (Exception exception)
            {
                this.Log().Error("[Consultar]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
            }
        }

        /// <summary>
        /// Metodo para checar a situação do serviço de CTe.
        /// </summary>
        /// <returns>A situação do serviço de CTe</returns>
        public ConsultaStatusResposta ConsultarSituacaoServico()
        {
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                var versao = Configuracoes.Geral.VersaoCTe.GetDescription();
                using (var cliente = new CTeStatusServicoServiceClient(Configuracoes, cert))
                {
                    var cabecalho = new CTeWsCabecalho()
                    {
                        CUf = (int)Configuracoes.WebServices.Uf,
                        VersaoDados = versao,
                    };

                    var request = new StringBuilder();
                    request.Append($"<consStatServCte xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"{versao}\">");
                    request.Append($"<tpAmb>{(Configuracoes.WebServices.Ambiente == DFeTipoAmbiente.Producao ? 1 : 2)}</tpAmb>");
                    request.Append("<xServ>STATUS</xServ>");
                    request.Append("</consStatServCte>");

                    var requestXml = request.ToString();
                    var ret = cliente.StatusServico(cabecalho, requestXml, $"CTe_ConsultarSituacao_{DateTime.Now:YYYYMMDDHHmmssfff}");
                    return ret;
                }
            }
            catch (Exception exception)
            {
                this.Log().Error("[ConsultarSituacaoServico]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
            }
        }

        #region Private

        private void GravarArquivoEmDisco(TipoArquivo tipo, string conteudoArquivo, string nomeArquivo, DateTime? data = null)
        {
            switch (tipo)
            {
                case TipoArquivo.CTe:
                    nomeArquivo = Path.Combine(Configuracoes.Arquivos.GetPathCTe(data ?? DateTime.Today), nomeArquivo);
                    break;

                case TipoArquivo.Evento:
                    nomeArquivo = Path.Combine(Configuracoes.Arquivos.GetPathEvento(data ?? DateTime.Today), nomeArquivo);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), tipo, null);
            }

            File.WriteAllText(nomeArquivo, conteudoArquivo, Encoding.UTF8);
        }

        #endregion Private

        #region Override

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            Configuracoes = new CTeConfig();
            Conhecimentos = new CTeCollection();
        }

        /// <inheritdoc />
        protected override void OnDisposing()
        {
        }

        #endregion Override

        #endregion Methods
    }
}