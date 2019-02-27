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
    [DFeSignInfoElement("infCte")]
    [DFeRoot("CTeOS", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTeOS : DFeSignDocument<CTeOS>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

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
            return $"{InfCTe.Id.OnlyNumbers()}-cteos.xml";
        }

        #endregion Methods
    }
}