// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="CTeInutilizacaoServiceClient.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Extensions;

namespace ACBr.Net.CTe.Services
{
    public sealed class CTeInutilizacaoServiceClient : CTeServiceClient<ICTeInutilizacao>, ICTeInutilizacao
    {
        #region Constructors

        public CTeInutilizacaoServiceClient(CTeConfig config, X509Certificate2 certificado = null) :
            base(config, ServicoCTe.CTeConsultaProtocolo, certificado)
        {
            Schema = SchemaCTe.InutCTe;
            ArquivoEnvio = "ped-inu";
            ArquivoResposta = "inu";
        }

        #endregion Constructors

        #region Methods

        public InutilizaoResposta Inutilizacao(string cnpj, string aJustificativa, int ano, ModeloCTe modelo, int serie, int numeroInicial, int numeroFinal)
        {
            Guard.Against<ArgumentNullException>(cnpj.IsEmpty(), "Informar o número do CNPJ");
            Guard.Against<ArgumentException>(!cnpj.IsCNPJ(), "CNPJ inválido.");
            Guard.Against<ArgumentNullException>(aJustificativa.IsEmpty(), "Informar uma Justificativa para Inutilização de numeração do Conhecimento Eletronico.");
            Guard.Against<ArgumentException>(aJustificativa.Length < 15, "A Justificativa para Inutilização de numeração do Conhecimento Eletronico deve ter no minimo 15 caracteres.");

            lock (serviceLock)
            {
                var idInutilizacao = $"ID{Configuracoes.WebServices.UF.GetValue()}{cnpj.OnlyNumbers()}{modelo.GetValue()}" +
                                      $"{serie.ZeroFill(3)}{numeroInicial.ZeroFill(9)}{numeroFinal.ZeroFill(9)}";

                var request = new StringBuilder();
                request.Append($"<inutCTe xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"{Configuracoes.Geral.VersaoDFe.GetDescription()}\">");
                request.Append($"<infInut Id=\"{idInutilizacao}\">");
                request.Append($"<tpAmb>{Configuracoes.WebServices.Ambiente.GetValue()}</tpAmb>");
                request.Append("<xServ>INUTILIZAR</xServ>");
                request.Append($"<cUF>{Configuracoes.WebServices.UF.GetValue()}</cUF>");
                request.Append($"<ano>{ano}</ano>");
                request.Append($"<CNPJ>{cnpj}</CNPJ>");
                request.Append($"<mod>{modelo.GetValue()}</mod>");
                request.Append($"<serie>{serie.ZeroFill(3)}</serie>");
                request.Append($"<nCTIni>{numeroInicial.ZeroFill(9)}</nCTIni>");
                request.Append($"<nCTFin>{numeroFinal.ZeroFill(9)}</nCTFin>");
                request.Append($"<xJust>{aJustificativa}</xJust>");
                request.Append("</infInut>");
                request.Append("</inutCTe>");

                var dadosMsg = request.ToString();
                XmlSigning.AssinarXml(dadosMsg, "inutCTe", "infInut", ClientCredentials.ClientCertificate.Certificate);
                ValidateMessage(dadosMsg);

                var xml = new XmlDocument();
                xml.LoadXml(dadosMsg);

                var inValue = new InutilizacaoRequest(DefineHeader(), xml);
                var retVal = ((ICTeInutilizacao)(this)).CTeInutilizacao(inValue);

                var retorno = new InutilizaoResposta(dadosMsg, retVal.Result.OuterXml, EnvelopeSoap, RetornoWS);
                return retorno;
            }
        }

        InutilizacaoResponse ICTeInutilizacao.CTeInutilizacao(InutilizacaoRequest request)
        {
            return base.Channel.CTeInutilizacao(request);
        }

        #endregion Methods
    }
}