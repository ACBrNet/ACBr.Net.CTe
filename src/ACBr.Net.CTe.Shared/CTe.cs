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
using System.Security.Cryptography.X509Certificates;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Document;

namespace ACBr.Net.CTe
{
    [DFeRoot("CTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTe : DFeDocument<CTe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTe()
        {
            Signature = new DFeSignature();
            InfCte = new InfCte();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement("infCte", Ocorrencia = Ocorrencia.Obrigatoria)]
        public InfCte InfCte { get; set; }

        public DFeSignature Signature { get; set; }

        [DFeIgnore]
        public bool Cancelada { get; set; }

        #endregion Propriedades

        #region Methods

        public void Assinar(X509Certificate2 certificado)
        {
            Guard.Against<ArgumentNullException>(certificado == null, "Certificado n�o pode ser nulo.");

            InfCte.Id = "CTe" + ChaveDFe.Gerar(InfCte.Ide.CUF, InfCte.Ide.DhEmi,
                            InfCte.Emit.CNPJ, (int)InfCte.Ide.Mod, InfCte.Ide.Serie,
                            InfCte.Ide.NCT, InfCte.Ide.TpEmis, InfCte.Ide.CCT);

            var xml = GetXml(DFeSaveOptions.DisableFormatting);
            xml = XmlSigning.AssinarXml(xml, "CTe", "infCte", certificado);
            Signature = DFeSignature.Load(xml);
        }

        internal string GetXmlName()
        {
            Guard.Against<ACBrDFeException>(InfCte.Id.IsEmpty(), "Chave do CTe inv�lida. Imposs�vel salvar XML.");

            return $"{InfCte.Id}-cte.xml";
        }

        #endregion Methods
    }
}

#pragma warning restore