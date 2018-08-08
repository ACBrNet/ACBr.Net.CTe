// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="CTeRecepcaoServiceClient.cs" company="ACBr.Net">
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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.CTe.Configuracao;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe.Services
{
    public sealed class CTeRecepcaoServiceClient : CTeServiceClient<ICteRecepcao>
    {
        #region Constructors

        public CTeRecepcaoServiceClient(CTeConfig config, X509Certificate2 certificado = null) :
            base(config, ServicoCTe.CTeRecepcao, certificado)
        {
            Schema = SchemaCTe.EnviCTe;
            ArquivoEnvio = "env-lot";
            ArquivoResposta = "rec";
        }

        #endregion Constructors

        #region Methods

        public RecepcaoCTeResposta RecepcaoLote(CTe[] ctes, string loteId)
        {
            Guard.Against<ArgumentNullException>(ctes == null, nameof(ctes));
            Guard.Against<ArgumentNullException>(loteId.IsEmpty(), "Número do lote vazio.");
            Guard.Against<ArgumentException>(!loteId.IsNumeric(), "Número do lote incorreto.");
            Guard.Against<ArgumentException>(ctes.Length < 1, "Precisa de pelo menos 1 conhecimento por lote.");
            Guard.Against<ArgumentException>(ctes.Length > 50, "So pode enviar 50 conhecimentos por lote.");

            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<enviCTe xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"{Configuracoes.Geral.VersaoDFe.GetDescription()}\">");
                request.Append($"<idLote>{loteId}</idLote>");

                var saveOptions = DFeSaveOptions.DisableFormatting | DFeSaveOptions.OmitDeclaration;
                if (Configuracoes.Geral.RetirarAcentos) saveOptions |= DFeSaveOptions.RemoveAccents;
                if (Configuracoes.Geral.RetirarEspacos) saveOptions |= DFeSaveOptions.RemoveSpaces;

                foreach (var cte in ctes)
                {
                    var cteXml = cte.Xml.IsEmpty() ? cte.GetXml(saveOptions) : cte.Xml;
                    GravarCTe(cteXml, cte.GetXmlName(), cte.InfCTe.Ide.DhEmi.DateTime, cte.InfCTe.Emit.CNPJ, cte.InfCTe.Ide.Mod);
                    request.Append(cteXml.Trim());
                }

                request.Append("</enviCTe>");

                var dadosMsg = request.ToString();

                Guard.Against<ACBrDFeException>(dadosMsg.Length > (500 * 1024),
                    $"Tamanho do XML de Dados superior a 500 Kbytes. Tamanho atual: {(dadosMsg.Length / 1024M).Trunc()} Kbytes");

                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var inValue = new RecepcaoRequest(DefineHeader(), doc);
                var retVal = Channel.RecepcaoLote(inValue);
                var retorno = new RecepcaoCTeResposta(dadosMsg, retVal.Result.OuterXml, EnvelopeSoap, RetornoWS);
                return retorno;
            }
        }

        #endregion Methods
    }
}