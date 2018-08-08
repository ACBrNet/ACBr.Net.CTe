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
    public sealed class ConhecimentosCollection : DFeCollection<CTeProc>
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

        /// <summary>
        /// Retorna as CTe não autorizadas.
        /// </summary>
        public CTe[] NaoAutorizadas
        {
            get { return this.Where(x => x.ProtCTe.InfProt.NProt.IsEmpty()).Select(x => x.CTe).ToArray(); }
        }

        /// <summary>
        /// Retornas as CTe Autorizadas.
        /// </summary>
        public CTeProc[] Autorizados
        {
            get { return this.Where(x => !x.ProtCTe.InfProt.NProt.IsEmpty()).ToArray(); }
        }

        #endregion Properties

        #region Methods

        /// <summary>Adds an object to the end of the <see cref="T:ACBr.Net.DFe.Core.Collection.DFeCollection`1" />.</summary>
        /// <param name="item">The object to be added to the end of the <see cref="T:ACBr.Net.DFe.Core.Collection.DFeCollection`1" />. The value can be null for reference types.</param>
        public void Add(CTe item)
        {
            Add(new CTeProc { CTe = item });
        }

        /// <summary>Inserts an element into the <see cref="T:ACBr.Net.DFe.Core.Collection.DFeCollection`1" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="!:DFeCollection&lt;T&gt;.Count" />.</exception>
        public void Insert(int index, CTe item)
        {
            Insert(index, new CTeProc { CTe = item });
        }

        /// <summary>Inserts the elements of a collection into the <see cref="T:ACBr.Net.DFe.Core.Collection.DFeCollection`1" /> at the specified index.</summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the <see cref="T:ACBr.Net.DFe.Core.Collection.DFeCollection`1" />. The collection itself cannot be null, but it can contain elements that are null, if type <paramref name="T" /> is a reference type.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="collection" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is greater than <see cref="!:DFeCollection&lt;T&gt;.Count" />.</exception>
        public void InsertRange(int index, IEnumerable<CTe> collection)
        {
            InsertRange(index, collection.Select(x => new CTeProc { CTe = x }));
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
            Guard.Against<ACBrDFeException>(!xml.Contains("cteProc") && !xml.Contains("CTe"), "Carregamento falhou: Arquivo xml incorreto.");

            var cteProc = xml.Contains("cteProc") ? CTeProc.Load(xml) : new CTeProc { CTe = CTe.Load(xml) };
            Add(cteProc);
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
            foreach (var cte in NaoAutorizadas)
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

            foreach (var cte in NaoAutorizadas)
            {
                var xml = cte.GetXml(DFeSaveOptions.DisableFormatting);
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