// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 06-11-2017
//
// Last Modified By : RFTD
// Last Modified On : 06-11-2017
// ***********************************************************************
// <copyright file="CfgGeral.cs" company="ACBr.Net">
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
using System.Reflection;
using ACBr.Net.Core;
using ACBr.Net.Core.Extensions;
using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core.Common;
using PropertyChanged;

namespace ACBr.Net.CTe.Configuracao
{
    [ImplementPropertyChanged]
    [TypeConverter(typeof(ACBrExpandableObjectConverter))]
    public sealed class CfgGeral : DFeGeralConfigBase
    {
        #region Fields

        private string arquivoServicos;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CfgGeral"/> class.
        /// </summary>
        internal CfgGeral()
        {
            Salvar = false;

            var path = Assembly.GetExecutingAssembly().GetPath();
            if (!path.IsEmpty())
            {
                PathSchemas = Path.Combine(path, "Schemas");
                PathSalvar = Path.Combine(path, "XmlCTe");
            }

            VersaoCTe = CTeVersao.v300;
            ExibirErroSchema = false;
            RetirarAcentos = false;
            FormatoAlerta = "TAG:%TAG% ID:%ID%/%TAG%(%DESCRICAO%) - %MSG%.";
            arquivoServicos = string.Empty;
        }

        #endregion Constructor

        #region Properties

        public CTeVersao VersaoCTe { get; set; }

        public string ArquivoServicos
        {
            get
            {
                return arquivoServicos;
            }
            set
            {
                arquivoServicos = value;
                ServiceManager.Load(arquivoServicos);
            }
        }

        #endregion Properties
    }
}