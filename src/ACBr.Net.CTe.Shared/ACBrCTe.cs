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
using System.Linq;
using System.Net;
using System.Threading;

#if !NETSTANDARD2_0

using System.Drawing;

#endif

using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Logging;
using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe
{
#if !NETSTANDARD2_0

    [ToolboxBitmap(typeof(ACBrCTe), "ACBr.Net.CTe.ACBrCTe.bmp")]
#endif

    public sealed class ACBrCTe : ACBrComponent, IACBrLog
    {
        #region Fields

        private StatusACBrCTe status;
        private SecurityProtocolType securityProtocol;

        #endregion Fields

        #region Events

        /// <summary>
        /// Evento disparado quando o status do componente muda.
        /// </summary>
        public event EventHandler<EventArgs> StatusChanged;

        #endregion Events

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

        /// <summary>
        /// Retorna a situação do componente.
        /// </summary>
        public StatusACBrCTe Status
        {
            get => status;
            internal set
            {
                if (status == value) return;

                status = value;
                StatusChanged.Raise(this, EventArgs.Empty);
            }
        }

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
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            Conhecimentos.Assinar();
            Conhecimentos.Validar();

            RecepcaoCTeResposta recepcao;

            try
            {
                Status = StatusACBrCTe.CTeRecepcao;
                using (var cliente = new CTeRecepcaoServiceClient(Configuracoes, cert))
                {
                    recepcao = cliente.RecepcaoLote(Conhecimentos.NaoAutorizadas, lote);
                }
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
                Status = StatusACBrCTe.CTeIdle;
            }

            Thread.Sleep((int)Configuracoes.WebServices.AguardarConsultaRet);
            var retRecepcao = ConsultaRecibo(recepcao.Resultado.InfRec.NRec);

            if (imprimir && Conhecimentos.Autorizados.Any())
            {
            }

            return new EnviarCTeResposta() { RecepcaoResposta = recepcao, RetRecepcaoResposta = retRecepcao };
        }

        /// <summary>
        /// Consulta o recibo da CTe para saber se o mesmo ofi processado.
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns></returns>
        public CTeRetRecepcaoResposta ConsultaRecibo(string recibo)
        {
            Guard.Against<ArgumentException>(recibo.IsEmpty(), nameof(recibo));

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusACBrCTe.CTeRetRecepcao;
                CTeRetRecepcaoResposta retRecepcao;
                using (var cliente = new CTeRetRecepcaoServiceClient(Configuracoes, cert))
                {
                    var tentativas = 0;
                    var intervaloTentativas = Math.Max(Configuracoes.WebServices.IntervaloTentativas, 1000);

                    do
                    {
                        retRecepcao = cliente.RetRecepcao(recibo);
                        tentativas++;
                        Thread.Sleep(intervaloTentativas);
                    } while (retRecepcao.Resultado.CStat != 104 && tentativas < Configuracoes.WebServices.Tentativas);
                }

                if (retRecepcao.Resultado.CStat != 104) return retRecepcao;

                foreach (var protCTe in retRecepcao.Resultado.ProtCTe)
                {
                    var cteProc = Conhecimentos.SingleOrDefault(x => x.CTe.InfCte.Id.OnlyNumbers() == protCTe.InfProt.ChCTe.OnlyNumbers());
                    if (cteProc == null) continue;
                    if (Configuracoes.Geral.ValidarDigest)
                    {
                        Guard.Against<ACBrDFeException>(!protCTe.InfProt.DigVal.IsEmpty() &&
                                                        protCTe.InfProt.DigVal != cteProc.CTe.Signature.SignedInfo.Reference.DigestValue,
                            $"DigestValue do documento {cteProc.CTe.InfCte.Id} não confere.");
                    }

                    cteProc.ProtCTe = protCTe;
                    if (!Configuracoes.Arquivos.Salvar) continue;
                    if (Configuracoes.Arquivos.SalvarApenasCTeProcessados && !cteProc.Processado) continue;

                    var data = Configuracoes.Arquivos.EmissaoPathCTe ? cteProc.CTe.InfCte.Ide.DhEmi : DateTime.Now;
                    var pathArquivo = Configuracoes.Arquivos.GetPathCTe(data, cteProc.CTe.InfCte.Emit.CNPJ, cteProc.CTe.InfCte.Ide.Mod);
                    var nomeArquivo = $"{cteProc.CTe.InfCte.Id}-cte.xml";

                    cteProc.Save(Path.Combine(pathArquivo, nomeArquivo), DFeSaveOptions.DisableFormatting);
                }

                return retRecepcao;
            }
            catch (Exception exception)
            {
                this.Log().Error("[ConsultaRecibo]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = StatusACBrCTe.CTeIdle;
            }
        }

        /// <summary>
        /// Metodo para checar a situação da CTe pela chave.
        /// </summary>
        /// <returns>A situação do serviço de CTe</returns>
        public ConsultaCTeResposta Consultar(string chave = "")
        {
            Guard.Against<ArgumentException>(chave.IsEmpty() && !Conhecimentos.NaoAutorizadas.Any(), "ERRO: Nenhum CT-e ou Chave Informada!");

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusACBrCTe.CTeConsulta;

                using (var cliente = new CTeConsultaServiceClient(Configuracoes, cert))
                {
                    if (!chave.IsEmpty())
                    {
                        Conhecimentos.Clear();
                        return cliente.Consulta(chave);
                    }
                    else
                    {
                        var ret = cliente.Consulta(Conhecimentos.NaoAutorizadas.First().InfCte.Id.OnlyNumbers());

                        return ret;
                    }
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
                Status = StatusACBrCTe.CTeIdle;
            }
        }

        /// <summary>
        /// Metodo para checar a situação do serviço de CTe.
        /// </summary>
        /// <returns>A situação do serviço de CTe</returns>
        public ConsultaStatusResposta ConsultarSituacaoServico()
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusACBrCTe.CTeStatusServico;

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
                Status = StatusACBrCTe.CTeIdle;
            }
        }

        #region Override

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            status = StatusACBrCTe.CTeIdle;
            securityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
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