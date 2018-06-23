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
using System.Runtime.Serialization;
using ACBr.Net.Core;
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            Guard.Against<ArgumentNullException>(path.IsEmpty(), nameof(path));
            Guard.Against<ArgumentException>(!File.Exists(path), "Arquivo não encontrado");

            Load(File.Open(path, FileMode.Open));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public void Load(Stream stream)
        {
            Guard.Against<ArgumentNullException>(stream == null, nameof(stream));
            Guard.Against<ArgumentException>(stream.Length == 0, "Stream vazio");

            using (var reader = new StreamReader(stream))
            {
                var conteudo = reader.ReadLine();
                Guard.Against<ACBrDFeException>(conteudo.IsEmpty(), "Não foi possivel ler o conteudo.");

                var cteProc = conteudo.Contains("cteProc") ? CTeProc.Load(conteudo) : new CTeProc { CTe = CTe.Load(conteudo) };
                Add(cteProc);
            }
        }

        /// <summary>
        /// Assina as CTe não autorizadas.
        /// </summary>
        public void Assinar()
        {
            var cert = Parent.Configuracoes.Certificados.ObterCertificado();

            try
            {
                foreach (var cte in NaoAutorizadas)
                {
                    cte.Assinar(cert);
                }
            }
            finally
            {
                cert.Reset();
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

                if (cte.InfCte.Ide.TpCTe == CTeTipo.Anulacao || cte.InfCte.Ide.TpCTe == CTeTipo.Complemento) continue;

                var xmlModal = ((CTeNormal)cte.InfCte.InfoCTe).InfModal.Modal.GetXml(DFeSaveOptions.DisableFormatting);
                SchemaCTe schema;

                switch (((CTeNormal)cte.InfCte.InfoCTe).InfModal.Modal)
                {
                    case CTeAereoModal _:
                        schema = SchemaCTe.CteModalAereo;
                        break;

                    case CTeAquavModal _:
                        schema = SchemaCTe.CteModalAquaviario;
                        break;

                    case CteDutoModal _:
                        schema = SchemaCTe.CteModalDutoviario;
                        break;

                    case CTeFerrovModal _:
                        schema = SchemaCTe.CteModalFerroviario;
                        break;

                    case CTeMultimodal _:
                        schema = SchemaCTe.CteMultiModal;
                        break;

                    case CTeRodoModal _:
                        schema = SchemaCTe.CteModalRodoviario;
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