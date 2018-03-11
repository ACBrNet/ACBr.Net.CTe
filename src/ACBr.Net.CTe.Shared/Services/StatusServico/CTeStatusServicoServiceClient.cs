// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 11-10-2016
//
// Last Modified By : RFTD
// Last Modified On : 11-10-2016
// ***********************************************************************
// <copyright file="CTeStatusServicoServiceClient.cs" company="ACBr.Net">
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

using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe.Services
{
    public sealed class CTeStatusServicoServiceClient : CTeServiceClient<ICTeStatusServico>, ICTeStatusServico
    {
        #region Constructors

        public CTeStatusServicoServiceClient(CTeConfig config, X509Certificate2 certificado = null) :
            base(config, ServicoCTe.CTeStatusServico, certificado)
        {
            Schema = SchemaCTe.ConsStatServCTe;
            ArquivoEnvio = "ped-sta";
            ArquivoResposta = "sta";
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Consulta a situação do serviço da CTe.
        /// </summary>
        /// <returns></returns>
        public ConsultaStatusResposta StatusServico()
        {
            lock (serviceLock)
            {
                var request = new StringBuilder();
                request.Append($"<consStatServCte xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"{Config.Geral.VersaoDFe.GetDescription()}\">");
                request.Append($"<tpAmb>{(Config.WebServices.Ambiente == DFeTipoAmbiente.Producao ? 1 : 2)}</tpAmb>");
                request.Append("<xServ>STATUS</xServ>");
                request.Append("</consStatServCte>");

                var dadosMsg = request.ToString();
                ValidateMessage(dadosMsg);

                var doc = new XmlDocument();
                doc.LoadXml(dadosMsg);

                var inValue = new StatusServicoRequest(DefineHeader(), doc);
                var retVal = ((ICTeStatusServico)this).StatusServico(inValue);

                var retorno = new ConsultaStatusResposta(dadosMsg, retVal.Result.OuterXml, EnvelopeSoap, RetornoWS);
                return retorno;
            }
        }

        StatusServicoResponse ICTeStatusServico.StatusServico(StatusServicoRequest request)
        {
            return Channel.StatusServico(request);
        }

        #endregion Methods
    }
}