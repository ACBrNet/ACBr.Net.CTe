// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 06-23-2018
// ***********************************************************************
// <copyright file="CTeRecepcaoEventoServiceClient.cs" company="ACBr.Net">
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

using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Extensions;

namespace ACBr.Net.CTe.Services
{
    public sealed class CTeRecepcaoEventoServiceClient : CTeServiceClient<ICTeRecepcaoEvento>
    {
        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="certificado"></param>
        public CTeRecepcaoEventoServiceClient(CTeConfig config, X509Certificate2 certificado = null) :
            base(config, ServicoCTe.RecepcaoEvento, certificado)
        {
            Schema = SchemaCTe.EventoCTe;
            ArquivoEnvio = "ped-eve";
            ArquivoResposta = "eve";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="lote"></param>
        /// <param name="nSeqEvento"></param>
        /// <param name="chave"></param>
        /// <param name="cnpj"></param>
        /// <param name="evento"></param>
        /// <returns></returns>
        public RecepcaoEventoResposta RecepcaoEvento(int lote, int nSeqEvento, string chave, string cnpj, IEventoCTe evento)
        {
            Guard.Against<ArgumentNullException>(chave.IsEmpty(), nameof(chave));
            Guard.Against<ArgumentNullException>(cnpj.IsEmpty(), nameof(cnpj));
            Guard.Against<ArgumentNullException>(lote < 0, nameof(lote));
            Guard.Against<ArgumentNullException>(nSeqEvento < 0, nameof(nSeqEvento));
            Guard.Against<ArgumentNullException>(evento == null, nameof(evento));

            lock (serviceLock)
            {
                const DFeSaveOptions saveOptions = DFeSaveOptions.DisableFormatting | DFeSaveOptions.OmitDeclaration;

                CTeTipoEvento tipo;
                string xmlEvento;
                var date = DateTimeOffset.Now;
                var versao = Configuracoes.Geral.VersaoDFe.GetDescription();
                switch (evento)
                {
                    case CTeEvCancCTe evtCTe:
                        xmlEvento = evtCTe.GetXml(saveOptions);
                        tipo = CTeTipoEvento.Cancelamento;
                        GravarEvento(xmlEvento, $"{chave}-can-eve.xml", tipo, date.DateTime, cnpj);
                        ValidateMessage(xmlEvento, SchemaCTe.EvCancCTe);
                        break;

                    case CteEvCceCTe evtCTe:
                        xmlEvento = evtCTe.GetXml(saveOptions);
                        tipo = CTeTipoEvento.CartaCorrecao;
                        GravarEvento(xmlEvento, $"{chave}-cce-eve.xml", tipo, date.DateTime, cnpj);
                        ValidateMessage(xmlEvento, SchemaCTe.EvCCeCTe);
                        break;

                    case CTeEvEPEC evtCTe:
                        xmlEvento = evtCTe.GetXml(saveOptions);
                        tipo = CTeTipoEvento.EPEC;
                        GravarEvento(xmlEvento, $"{chave}-ped-epec.xml", tipo, date.DateTime, cnpj);
                        ValidateMessage(xmlEvento, SchemaCTe.EvEPECCTe);
                        break;

                    case CTeEvRegMultimodal evtCTe:
                        xmlEvento = evtCTe.GetXml(saveOptions);
                        tipo = CTeTipoEvento.RegistroMultiModal;
                        GravarEvento(xmlEvento, $"{chave}-rmulti-eve.xml", tipo, date.DateTime, cnpj);
                        ValidateMessage(xmlEvento, SchemaCTe.EvRegMultimodal);
                        break;

                    case CTeEvPrestDesacordo evtCTe:
                        xmlEvento = evtCTe.GetXml(saveOptions);
                        tipo = CTeTipoEvento.PrestacaoServicoDesacordo;
                        GravarEvento(xmlEvento, $"{chave}-desa-eve.xml", tipo, date.DateTime, cnpj);
                        ValidateMessage(xmlEvento, SchemaCTe.EvPrestDesacordo);
                        break;

                    default:
                        throw new ArgumentException("O evento informado é desconhecido.");
                }

                var request = new StringBuilder();
                request.Append($"<eventoCTe  xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"{versao}\">");
                request.Append($"<infEvento Id=\"{lote}\">");
                request.Append($"<cOrgao>{Configuracoes.WebServices.UF.GetValue()}</cOrgao>");
                request.Append($"<tpAmb>{Configuracoes.WebServices.Ambiente.GetValue()}</tpAmb>");
                request.Append($"<CNPJ>{cnpj}</CNPJ>");
                request.Append($"<chCTe>{chave}</chCTe>");
                request.Append($"<dhEvento>{date}</dhEvento>");
                request.Append($"<tpEvento>{tipo.GetValue()}</tpEvento>");
                request.Append($"<nSeqEvento>{nSeqEvento}</nSeqEvento>");
                request.Append($"<detEvento versaoEvento=\"{versao}\">");
                request.Append(xmlEvento);
                request.Append("</detEvento>");
                request.Append("</infEvento>");
                request.Append("</eventoCTe>");

                var dadosMsg = request.ToString();
                XmlSigning.AssinarXml(dadosMsg, "eventoCTe", "infEvento", ClientCredentials.ClientCertificate.Certificate);
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var inValue = new RecepcaoEventoRequest(DefineHeader(), doc);
                var retVal = Channel.RecepcaoEvento(inValue);

                var retorno = new RecepcaoEventoResposta(dadosMsg, retVal.Result.OuterXml, EnvelopeSoap, RetornoWS);
                GravarEvento(xmlEvento, $"{chave}-procEventoCTe.xml", tipo, date.DateTime, cnpj);
                return retorno;
            }
        }

        #endregion Methods
    }
}