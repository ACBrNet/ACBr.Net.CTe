﻿// ***********************************************************************
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
using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Logging;
using ACBr.Net.CTe.Configuracao;
using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Extensions;
using ACBr.Net.DFe.Core.Service;

#if !NETSTANDARD2_0

using System.Drawing;

#endif

namespace ACBr.Net.CTe
{
#if !NETSTANDARD2_0

    [ToolboxBitmap(typeof(ACBrCTe), "ACBr.Net.CTe.ACBrCTe.bmp")]
#endif

    public sealed class ACBrCTe : ACBrComponent, IACBrLog
    {
        #region Fields

        private StatusCTe status;
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
        public StatusCTe Status
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
        /// Metodo para enviar a(s) CTe(d) carregada(s) na coleção.
        /// </summary>
        /// <param name="lote">Número do lote.</param>
        /// <param name="imprimir">Verdadeiro para imprimir o(s) CTe(s) retornado(s) com sucesso.</param>
        /// <returns></returns>
        public EnviarCTeResposta Enviar(int lote, bool imprimir = true)
        {
            return Enviar(lote.ToString(), imprimir);
        }

        /// <summary>
        /// Metodo para enviar a(s) CTe(d) carregada(s) na coleção.
        /// </summary>
        /// <param name="lote">Número do lote.</param>
        /// <param name="imprimir">Verdadeiro para imprimir o(s) CTe(s) retornado(s) com sucesso.</param>
        /// <returns></returns>
        public EnviarCTeResposta Enviar(string lote, bool imprimir = true)
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            var saveOptions = DFeSaveOptions.DisableFormatting | DFeSaveOptions.OmitDeclaration;
            if (Configuracoes.Geral.RetirarAcentos) saveOptions |= DFeSaveOptions.RemoveAccents;
            if (Configuracoes.Geral.RetirarEspacos) saveOptions |= DFeSaveOptions.RemoveSpaces;

            Conhecimentos.Assinar(cert, saveOptions);
            Conhecimentos.Validar();

            RecepcaoCTeResposta recepcao;

            try
            {
                Status = StatusCTe.Recepcao;
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
                Status = StatusCTe.EmEspera;
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
        public RetRecepcaoResposta ConsultaRecibo(string recibo)
        {
            Guard.Against<ArgumentException>(recibo.IsEmpty(), nameof(recibo));

            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusCTe.RetRecepcao;
                RetRecepcaoResposta retRecepcao;
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

                    var data = Configuracoes.Arquivos.EmissaoPathCTe ? cteProc.CTe.InfCte.Ide.DhEmi.DateTime : DateTime.Now;
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
                Status = StatusCTe.EmEspera;
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
                Status = StatusCTe.Consulta;

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
                Status = StatusCTe.EmEspera;
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
                Status = StatusCTe.StatusServico;

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
                Status = StatusCTe.EmEspera;
            }
        }

        /// <summary>
        /// Metodo para inutilizar uma faixa de numeração do CTe.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="aJustificativa"></param>
        /// <param name="ano"></param>
        /// <param name="modelo"></param>
        /// <param name="serie"></param>
        /// <param name="numeroInicial"></param>
        /// <param name="numeroFinal"></param>
        /// <returns></returns>
        public InutilizaoResposta Inutilizacao(string cnpj, string aJustificativa, int ano, ModeloCTe modelo, int serie,
            int numeroInicial, int numeroFinal)
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusCTe.Inutilizacao;

                using (var cliente = new CTeInutilizacaoServiceClient(Configuracoes, cert))
                {
                    return cliente.Inutilizacao(cnpj, aJustificativa, ano, modelo, serie, numeroInicial, numeroFinal);
                }
            }
            catch (Exception exception)
            {
                this.Log().Error("[Inutilizacao]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = StatusCTe.EmEspera;
            }
        }

        /// <summary>
        /// Metodo para enviar eventos da CTe.
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="nSeqEvento"></param>
        /// <param name="chave"></param>
        /// <param name="cnpj"></param>
        /// <param name="evento"></param>
        /// <returns></returns>
        public RecepcaoEventoResposta EnviarEvento(int lote, int nSeqEvento, string chave, string cnpj, IEventoCTe evento)
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusCTe.Evento;

                using (var cliente = new CTeRecepcaoEventoServiceClient(Configuracoes, cert))
                {
                    return cliente.RecepcaoEvento(lote, nSeqEvento, chave, cnpj, evento);
                }
            }
            catch (Exception exception)
            {
                this.Log().Error("[EnviarEvento]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = StatusCTe.EmEspera;
            }
        }

        /// <summary>
        /// Consulta Cadastro do contribuinte na SEFAZ.
        /// </summary>
        /// <returns></returns>
        public DFeConsultaCadastroResposta ConsultaCadastro(DFeCodUF uf, string cpfCNPJ = "", string ie = "")
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            ServicePointManager.SecurityProtocol = securityProtocol;
            var cert = Configuracoes.Certificados.ObterCertificado();

            try
            {
                Status = StatusCTe.Cadastro;

                using (var cliente = new ConsultaCadastroServiceClient(Configuracoes, uf, cert))
                {
                    return cliente.ConsultaCadastro(uf.ToSiglaUF(), uf, cpfCNPJ, ie);
                }
            }
            catch (Exception exception)
            {
                this.Log().Error("[ConsultaCadastro]", exception);
                throw;
            }
            finally
            {
                cert.Reset();
                ServicePointManager.SecurityProtocol = oldProtocol;
                Status = StatusCTe.EmEspera;
            }
        }

        #region Override

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            status = StatusCTe.EmEspera;
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