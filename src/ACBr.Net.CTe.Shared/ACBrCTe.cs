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
using System.Net;

#if !NETSTANDARD2_0

using System.Drawing;

#endif

using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Logging;
using ACBr.Net.CTe.Services;

namespace ACBr.Net.CTe
{
#if !NETSTANDARD2_0

    [ToolboxBitmap(typeof(ACBrCTe), "ACBr.Net.CTe.ACBrCTe.bmp")]
#endif

    public sealed class ACBrCTe : ACBrComponent, IACBrLog
    {
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
        public ConhecimentosCollection Conhecimentos { get; private set; }

        #endregion Propriedades

        #region Methods

        /// <summary>
        /// Metodo para enviar a CTe carregadas na coleção.
        /// </summary>
        /// <param name="lote">Número do lote.</param>
        /// <param name="imprimir">True se deve imprimir os CTe retornado com sucesso.</param>
        /// <returns></returns>
        public EnviarCTeResposta Enviar(int lote, bool imprimir = true)
        {
            return Enviar(lote.ToString(), imprimir);
        }

        /// <summary>
        /// Metodo para enviar a CTe carregadas na coleção.
        /// </summary>
        /// <param name="lote">Número do lote.</param>
        /// <param name="imprimir">True se deve imprimir os CTe retornado com sucesso.</param>
        /// <returns></returns>
        public EnviarCTeResposta Enviar(string lote, bool imprimir)
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Conhecimentos.Assinar();
                Conhecimentos.Validar();

                RecepcaoCTeResposta retRecpcao;
                using (var cliente = new CTeRecepcaoServiceClient(Configuracoes, cert))
                {
                    retRecpcao = cliente.RecepcaoLote(Conhecimentos.NaoAutorizadas, lote);
                }

                return new EnviarCTeResposta() { RecepcaoResposta = retRecpcao };
            }
            catch (Exception exception)
            {
                this.Log().Error("[EnviarCTe]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
                ServicePointManager.SecurityProtocol = oldProtocol;
            }
        }

        /// <summary>
        /// Metodo para checar a situação da CTe pela chave.
        /// </summary>
        /// <returns>A situação do serviço de CTe</returns>
        public ConsultaCTeResposta Consultar(string chave)
        {
            Guard.Against<ArgumentNullException>(chave == null, nameof(chave));
            Guard.Against<ArgumentException>(chave.IsEmpty(), nameof(chave));

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                using (var cliente = new CTeConsultaServiceClient(Configuracoes, cert))
                {
                    return cliente.Consulta(chave);
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
                ServicePointManager.SecurityProtocol = oldProtocol;
            }
        }

        /// <summary>
        /// Metodo para checar a situação do serviço de CTe.
        /// </summary>
        /// <returns>A situação do serviço de CTe</returns>
        public ConsultaStatusResposta ConsultarSituacaoServico()
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                using (var cliente = new CTeStatusServicoServiceClient(Configuracoes, cert))
                {
                    return cliente.StatusServico();
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
                ServicePointManager.SecurityProtocol = oldProtocol;
            }
        }

        #region Override

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            Configuracoes = new CTeConfig(this);
            Conhecimentos = new ConhecimentosCollection(this);
        }

        /// <inheritdoc />
        protected override void OnDisposing()
        {
        }

        #endregion Override

        #endregion Methods
    }
}