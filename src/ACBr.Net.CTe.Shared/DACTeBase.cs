// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 09-30-2018
//
// Last Modified By : RFTD
// Last Modified On : 10-16-2018
// ***********************************************************************
// <copyright file="DACTeBase.cs" company="ACBr.Net">
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

using ACBr.Net.CTe.Services;
using ACBr.Net.DFe.Core.Common;

namespace ACBr.Net.CTe
{
    /// <summary>
    /// Classe base para os componentes de impressão do ACBrCTe.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public abstract class DACTeBase : DFeReportClass<ACBrCTe>
    {
        #region Properties

        public bool Cancelado { get; set; }

        public bool QuebrarLinhasObservacao { get; set; }

        public DACTeLayout LayoutImpressao { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Imprime o DACTe do CTe.
        /// </summary>
        /// <param name="conhecimentos"></param>
        public abstract void Imprimir(CTeProc[] conhecimentos);

        /// <summary>
        /// Imprime o DACTe do CTe em PDF.
        /// </summary>
        /// <param name="conhecimentos"></param>
        public abstract void ImprimirPDF(CTeProc[] conhecimentos);

        /// <summary>
        /// Imprime um evento do CTe.
        /// </summary>
        /// <param name="eventos"></param>
        public abstract void ImprimirEvento(CTeProcEvento[] eventos);

        /// <summary>
        /// Imprime um evento do CTe em PDF.
        /// </summary>
        /// <param name="eventos"></param>
        public abstract void ImprimirEventoPDF(CTeProcEvento[] eventos);

        /// <summary>
        /// Imprime um evento de inutilizacao do CTe.
        /// </summary>
        /// <param name="inutilizao"></param>
        public abstract void ImprimirInutilizacao(InutilizaoResposta inutilizao);

        /// <summary>
        /// Imprime um evento de inutilizacao do CTe em PDF.
        /// </summary>
        /// <param name="inutilizao"></param>
        public abstract void ImprimirInutilizacaoPDF(InutilizaoResposta inutilizao);

        protected override void OnInitialize()
        {
            Cancelado = false;
            QuebrarLinhasObservacao = true;
            LayoutImpressao = DACTeLayout.Retrato;
        }

        protected override void OnDisposing()
        {
            //
        }

        /// <inheritdoc />
        protected override void ParentChanged(ACBrCTe oldParent, ACBrCTe newParent)
        {
            if (oldParent != null && oldParent.DACTe == this) oldParent.DACTe = null;
            if (newParent != null && newParent.DACTe != this) newParent.DACTe = this;
        }

        #endregion Methods
    }
}