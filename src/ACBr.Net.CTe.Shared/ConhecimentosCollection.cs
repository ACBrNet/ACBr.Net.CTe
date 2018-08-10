// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 03-09-2018
// ***********************************************************************
// <copyright file="ConhecimentosCollection.cs" company="ACBr.Net">
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe
{
    public sealed class ConhecimentosCollection : DFeCollection<CTe>
    {
        #region Constructors

        internal ConhecimentosCollection(ACBrCTe parent)
        {
            Parent = parent;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Retorna a classe ACBrCTe parente da coleção.
        /// </summary>
        public ACBrCTe Parent { get; }

        #endregion Properties

        #region Methods

        /// <inheritdoc />
        public override CTe AddNew()
        {
            var instance = base.AddNew();
            instance.InfCTe.Versao = Parent.Configuracoes.Geral.VersaoDFe;
            return instance;
        }

        /// <summary>
        /// Carrega a CTe informada.
        /// </summary>
        /// <param name="path">Caminho da CTe.</param>
        public void Load(string path)
        {
            Guard.Against<ArgumentNullException>(path.IsEmpty(), nameof(path));

            var xml = File.Exists(path) ? File.ReadAllText(path, Encoding.UTF8) : path;
            LoadXml(xml);
        }

        /// <summary>
        /// Carrega a CTe informada.
        /// </summary>
        /// <param name="stream">Stream da CTe.</param>
        public void Load(Stream stream)
        {
            Guard.Against<ArgumentNullException>(stream == null, nameof(stream));
            Guard.Against<ArgumentException>(stream.Length == 0, "Stream vazio");

            using (var reader = new StreamReader(stream))
            {
                LoadXml(reader.ReadToEnd());
            }
        }

        private void LoadXml(string xml)
        {
            Guard.Against<ACBrDFeException>(xml.IsEmpty(), "Carregamento falhou: Não foi possivel ler o conteudo.");
            Guard.Against<ACBrDFeException>(!xml.Contains("</CTe>"), "Carregamento falhou: Arquivo xml incorreto.");
            Add(CTe.Load(xml));
        }

        /// <summary>
        /// Assina as CTe não autorizadas.
        /// </summary>
        public void Assinar()
        {
            var cert = Parent.Configuracoes.Certificados.ObterCertificado();

            var saveOptions = DFeSaveOptions.DisableFormatting | DFeSaveOptions.OmitDeclaration;
            if (Parent.Configuracoes.Geral.RetirarAcentos) saveOptions |= DFeSaveOptions.RemoveAccents;
            if (Parent.Configuracoes.Geral.RetirarEspacos) saveOptions |= DFeSaveOptions.RemoveSpaces;

            try
            {
                Assinar(cert, saveOptions);
            }
            finally
            {
                cert.Reset();
            }
        }

        /// <summary>
        /// Assina as CTe não autorizadas.
        /// </summary>
        /// <param name="certificado">O certificado.</param>
        /// <param name="options"></param>
        public void Assinar(X509Certificate2 certificado, DFeSaveOptions options)
        {
            foreach (var cte in this)
            {
                cte.Assinar(certificado, options);
            }
        }

        /// <summary>
        /// Valida a CTe de acordo com o Schema.
        /// </summary>
        public void Validar()
        {
            var listaErros = new List<string>();

            var pathSchemaCTe = Parent.Configuracoes.Arquivos.GetSchema(SchemaCTe.CTe);

            foreach (var cte in this)
            {
                var xml = cte.GetXml();
                XmlSchemaValidation.ValidarXml(xml, pathSchemaCTe, out var erros, out _);

                listaErros.AddRange(erros);

                if (cte.InfCTe.Ide.TpCTe == CTeTipo.Anulacao || cte.InfCTe.Ide.TpCTe == CTeTipo.Complemento) continue;

                var xmlModal = ((CTeNormal)cte.InfCTe.InfoCTe).InfModal.Modal.GetXml(DFeSaveOptions.DisableFormatting);
                SchemaCTe schema;

                switch (((CTeNormal)cte.InfCTe.InfoCTe).InfModal.Modal)
                {
                    case CTeAereoModal _:
                        schema = SchemaCTe.CTeModalAereo;
                        break;

                    case CTeAquavModal _:
                        schema = SchemaCTe.CTeModalAquaviario;
                        break;

                    case CTeDutoModal _:
                        schema = SchemaCTe.CTeModalDutoviario;
                        break;

                    case CTeFerrovModal _:
                        schema = SchemaCTe.CTeModalFerroviario;
                        break;

                    case CTeMultimodal _:
                        schema = SchemaCTe.CTeMultiModal;
                        break;

                    case CTeRodoModal _:
                        schema = SchemaCTe.CTeModalRodoviario;
                        break;

                    default:
                        continue;
                }

                var pathSchemaModal = Parent.Configuracoes.Arquivos.GetSchema(schema);
                XmlSchemaValidation.ValidarXml(xmlModal, pathSchemaModal, out var errosModal, out _);
                listaErros.AddRange(errosModal);
            }

            Guard.Against<ACBrDFeValidationException>(listaErros.Any(), "Erros de validação do xml." +
                                            $"{(Parent.Configuracoes.Geral.ExibirErroSchema ? Environment.NewLine + listaErros.AsString() : "")}");
        }

        #endregion Methods
    }
}