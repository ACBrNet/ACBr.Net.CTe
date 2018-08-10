// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-12-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-12-2016
// ***********************************************************************
// <copyright file="CTe.cs" company="ACBr.Net">
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
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Document;

namespace ACBr.Net.CTe
{
    [DFeSignInfoElement("infCte")]
    [DFeRoot("CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTe : DFeSignDocument<CTe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTe()
        {
            Signature = new DFeSignature();
            InfCTe = new CTeInfCTe();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement("infCte", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeInfCTe InfCTe { get; set; }

        [DFeIgnore]
        public bool Cancelada { get; set; }

        [DFeIgnore]
        public bool Assinado => ShouldSerializeSignature();

        #endregion Propriedades

        #region Methods

        /// <summary>
        /// Assina o CTe.
        /// </summary>
        /// <param name="certificado">The certificado.</param>
        /// <param name="saveOptions">The save options.</param>
        public void Assinar(X509Certificate2 certificado, DFeSaveOptions saveOptions)
        {
            Guard.Against<ArgumentNullException>(certificado == null, "Certificado não pode ser nulo.");
            Guard.Against<ArgumentException>(!Enum.IsDefined(typeof(DFeSaveOptions), saveOptions), "Valor não encontrado no enum.");

            if (InfCTe.Id.IsEmpty() || InfCTe.Id.Length < 44)
            {
                var chave = ChaveDFe.Gerar(InfCTe.Ide.CUF, InfCTe.Ide.DhEmi.DateTime,
                    InfCTe.Emit.CNPJ, (int)InfCTe.Ide.Mod, InfCTe.Ide.Serie,
                    InfCTe.Ide.NCT, InfCTe.Ide.TpEmis, InfCTe.Ide.CCT);

                InfCTe.Id = $"CTe{chave.Chave}";
                InfCTe.Ide.CDV = chave.Digito;
            }

            AssinarDocumento(certificado, saveOptions, false, SignDigest.SHA1);
        }

        public bool ValidarAssinatura(bool gerarXml = true)
        {
            Guard.Against<ACBrDFeException>(!Assinado, "Documento não esta assinado.");
            return ValidarAssinaturaDocumento(gerarXml);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string GetXmlName()
        {
            return $"{InfCTe.Id.OnlyNumbers()}-cte.xml";
        }

        #endregion Methods
    }
}

#pragma warning restore