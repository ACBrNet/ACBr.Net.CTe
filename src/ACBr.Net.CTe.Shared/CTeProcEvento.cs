// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 03-13-2018
//
// Last Modified By : RFTD
// Last Modified On : 03-13-2018
// ***********************************************************************
// <copyright file="CTeProcEvento.cs" company="ACBr.Net">
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

using System.ComponentModel;
using System.IO;
using System.Text;
using ACBr.Net.Core.Generics;
using ACBr.Net.CTe.Eventos;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    [DFeRoot("procEventoCTe", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public sealed class CTeProcEvento : GenericClone<CTeProcEvento>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructor

        public CTeProcEvento()
        {
            EventoCTe = new CTeEventoCTe();
            RetEventoCTe = new CTeRetEventoCTe();
        }

        #endregion Constructor

        #region Properties

        [DFeAttribute(TipoCampo.Enum, "versao", Min = 1, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeVersao Versao { get; set; }

        [DFeAttribute(TipoCampo.Str, "ipTransmissor", Min = 1, Max = 255, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IpTransmissor { get; set; }

        public CTeEventoCTe EventoCTe { get; set; }

        public CTeRetEventoCTe RetEventoCTe { get; set; }

        [DFeIgnore] public string Xml { get; protected set; }

        #endregion Properties

        #region Methods

        /// <summary>Carrega o documento.</summary>
        /// <param name="document">The document.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>TDocument.</returns>
        public static CTeProcEvento Load(string document, Encoding encoding = null)
        {
            var dfeSerializer = DFeSerializer.CreateSerializer<CTeProcEvento>();
            if (encoding != null)
                dfeSerializer.Options.Encoding = encoding;
            var str = File.Exists(document) ? File.ReadAllText(document, dfeSerializer.Options.Encoding) : document;
            var ret = dfeSerializer.Deserialize(document);
            ret.Xml = str;
            return ret;
        }

        /// <summary>Carrega o documento.</summary>
        /// <param name="document">The document.</param>
        /// <param name="encoding"></param>
        /// <returns>CTeProcEvento.</returns>
        public static CTeProcEvento Load(Stream document, Encoding encoding = null)
        {
            var dfeSerializer = DFeSerializer.CreateSerializer<CTeProcEvento>();
            if (encoding != null)
                dfeSerializer.Options.Encoding = encoding;
            using (var streamReader = new StreamReader(document, dfeSerializer.Options.Encoding))
            {
                document.Position = 0L;
                var end = streamReader.ReadToEnd();
                var ret = dfeSerializer.Deserialize(end);
                ret.Xml = end;
                return ret;
            }
        }

        /// <summary>Retorna o Xml do documento.</summary>
        /// <param name="options">The options.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>System.String.</returns>
        public string GetXml(DFeSaveOptions options = DFeSaveOptions.None, Encoding encoding = null)
        {
            using (var memoryStream = new MemoryStream())
            {
                Save(memoryStream, options, encoding);
                using (var streamReader = new StreamReader(memoryStream))
                    return streamReader.ReadToEnd();
            }
        }

        /// <summary>Salva o documento.</summary>
        /// <param name="path">The path.</param>
        /// <param name="options">The options.</param>
        /// <param name="encoding">The encoding.</param>
        public void Save(string path, DFeSaveOptions options = DFeSaveOptions.None, Encoding encoding = null)
        {
            var dfeSerializer = DFeSerializer.CreateSerializer<CTeProcEvento>();
            if (!options.HasFlag(DFeSaveOptions.None))
            {
                dfeSerializer.Options.RemoverAcentos = options.HasFlag(DFeSaveOptions.RemoveAccents);
                dfeSerializer.Options.RemoverEspacos = options.HasFlag(DFeSaveOptions.RemoveSpaces);
                dfeSerializer.Options.FormatarXml = !options.HasFlag(DFeSaveOptions.DisableFormatting);
                dfeSerializer.Options.OmitirDeclaracao = options.HasFlag(DFeSaveOptions.OmitDeclaration);
            }

            if (encoding != null)
                dfeSerializer.Options.Encoding = encoding;
            dfeSerializer.Serialize((object)this, path);
            Xml = File.ReadAllText(path, dfeSerializer.Options.Encoding);
        }

        /// <summary>Salva o documento.</summary>
        /// <param name="stream">The stream.</param>
        /// <param name="options">The options.</param>
        /// <param name="encoding">The encoding.</param>
        public void Save(Stream stream, DFeSaveOptions options = DFeSaveOptions.None, Encoding encoding = null)
        {
            var dfeSerializer = DFeSerializer.CreateSerializer<CTeProcEvento>();
            if (!options.HasFlag(DFeSaveOptions.None))
            {
                dfeSerializer.Options.RemoverAcentos = options.HasFlag(DFeSaveOptions.RemoveAccents);
                dfeSerializer.Options.RemoverEspacos = options.HasFlag(DFeSaveOptions.RemoveSpaces);
                dfeSerializer.Options.FormatarXml = !options.HasFlag(DFeSaveOptions.DisableFormatting);
                dfeSerializer.Options.OmitirDeclaracao = options.HasFlag(DFeSaveOptions.OmitDeclaration);
            }

            if (encoding != null)
                dfeSerializer.Options.Encoding = encoding;
            dfeSerializer.Serialize((object)this, stream);
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                stream.Position = 0L;
                using (var streamReader = new StreamReader(memoryStream, dfeSerializer.Options.Encoding))
                    Xml = streamReader.ReadToEnd();
            }
        }

        #endregion Methods
    }
}