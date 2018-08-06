// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-11-2017
//
// Last Modified By : RFTD
// Last Modified On : 06-11-2017
// ***********************************************************************
// <copyright file="CTeConfigArquivos.cs" company="ACBr.Net">
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
using System.IO;
using ACBr.Net.Core;
using ACBr.Net.Core.Extensions;
using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe.Configuracao
{
    [TypeConverter(typeof(ACBrExpandableObjectConverter))]
    public sealed class CTeConfigArquivos : DFeArquivosConfigBase<ACBrCTe, SchemaCTe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructor

        /// <inheritdoc />
        /// <summary>
        /// Inicializa uma nova instancia da classe <see cref="T:ACBr.Net.CTe.Configuracao.CTeConfigArquivos" />.
        /// </summary>
        internal CTeConfigArquivos(ACBrCTe parent) : base(parent)
        {
            EmissaoPathCTe = false;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the path n fe.
        /// </summary>
        /// <value>The path n fe.</value>
        [Browsable(true)]
        public string PathCTe { get; set; }

        /// <summary>
        /// Gets or sets the path lote.
        /// </summary>
        /// <value>The path lote.</value>
        [Browsable(true)]
        public string PathInu { get; set; }

        /// <summary>
        /// Gets or sets the path lote.
        /// </summary>
        /// <value>The path lote.</value>
        [Browsable(true)]
        public string PathEvento { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Browsable(true)]
        public bool EmissaoPathCTe { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Browsable(true)]
        public bool SalvarApenasCTeProcessados { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Retorna o caminho onde será salvo os CTes.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cnpj"></param>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public string GetPathCTe(DateTime? data = null, string cnpj = "", ModeloCTe modelo = ModeloCTe.CTe)
        {
            string modeloDscr;
            switch (modelo)
            {
                case ModeloCTe.CTeOS:
                    modeloDscr = "CTeOS";
                    break;

                default:
                    modeloDscr = "CTe";
                    break;
            }

            return GetPath(PathCTe, modeloDscr, cnpj, data, modeloDscr);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public string GetPathInu(DateTime? data = null, string cnpj = "")
        {
            return GetPath(PathInu, "Inu", cnpj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="cnpj"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetPathEvento(CTeTipoEvento tipo, string cnpj = "", DateTime? data = null)
        {
            var result = GetPath(PathEvento, "Evento", cnpj, data);

            if (AdicionarLiteral)
                result = Path.Combine(result, tipo.GetDescription());

            if (!Directory.Exists(result))
                Directory.CreateDirectory(result);

            return result;
        }

        /// <inheritdoc />
        protected override void ArquivoServicoChange()
        {
            CTeServiceManager.Load(ArquivoServicos);
        }

        /// <inheritdoc />
        public override string GetSchema(SchemaCTe schema)
        {
            if (SchemasCache.ContainsKey(schema)) return SchemasCache[schema];

            var schemaPath = "";
            var versao = Parent.Configuracoes.Geral.VersaoDFe;
            switch (schema)
            {
                case SchemaCTe.CTe:
                    schemaPath = Path.Combine(PathSchemas, $"cte_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CTeOS:
                    schemaPath = Path.Combine(PathSchemas, $"cteOS_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CancCTe:
                    schemaPath = Path.Combine(PathSchemas, $"evCancCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.InutCTe:
                    schemaPath = Path.Combine(PathSchemas, $"inutCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EventoCTe:
                    schemaPath = Path.Combine(PathSchemas, $"eventoCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ProcCTe:
                    schemaPath = Path.Combine(PathSchemas, $"procCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ProcEventoCTe:
                    schemaPath = Path.Combine(PathSchemas, $"procEventoCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ConsSitCTe:
                    schemaPath = Path.Combine(PathSchemas, $"consSitCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ConsStatServCTe:
                    schemaPath = Path.Combine(PathSchemas, $"consStatServCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ConsCad:
                    schemaPath = Path.Combine(PathSchemas, "consCad_v2.00.xsd");
                    break;

                case SchemaCTe.CteModalAereo:
                    schemaPath = Path.Combine(PathSchemas, $"cteModalAereo_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CteModalAquaviario:
                    schemaPath = Path.Combine(PathSchemas, $"cteModalAquaviario_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CteModalDutoviario:
                    schemaPath = Path.Combine(PathSchemas, $"cteModalDutoviario_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CteModalFerroviario:
                    schemaPath = Path.Combine(PathSchemas, $"cteModalFerroviario_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CteModalRodoviario:
                    schemaPath = Path.Combine(PathSchemas, $"cteModalRodoviario_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.CteMultiModal:
                    schemaPath = Path.Combine(PathSchemas, $"cteMultiModal_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EvEPECCTe:
                    schemaPath = Path.Combine(PathSchemas, $"evEPECCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EvCancCTe:
                    schemaPath = Path.Combine(PathSchemas, $"evCancCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EvRegMultimodal:
                    schemaPath = Path.Combine(PathSchemas, $"evRegMultimodal_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EvCCeCTe:
                    schemaPath = Path.Combine(PathSchemas, $"evCCeCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.DistDFeInt:
                    schemaPath = Path.Combine(PathSchemas, "distDFeInt_v1.00.xsd");
                    break;

                case SchemaCTe.CteModalRodoviarioOS:
                    schemaPath = Path.Combine(PathSchemas, $"cteModalRodoviarioOS_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EvPrestDesacordo:
                    schemaPath = Path.Combine(PathSchemas, $"evPrestDesacordo_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EvGTV:
                    schemaPath = Path.Combine(PathSchemas, $"evGTV_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ProcCTeOS:
                    schemaPath = Path.Combine(PathSchemas, $"procCTeOS_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.EnviCTe:
                    schemaPath = Path.Combine(PathSchemas, $"enviCTe_v{versao.GetDescription()}.xsd");
                    break;

                case SchemaCTe.ConsReciCTe:
                    schemaPath = Path.Combine(PathSchemas, $"consReciCTe_v{versao.GetDescription()}.xsd");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
            }

            SchemasCache.Add(schema, schemaPath);

            return SchemasCache[schema];
        }

        #endregion Methods
    }
}