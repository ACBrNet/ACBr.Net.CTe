// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-15-2016
// ***********************************************************************
// <copyright file="CTeICMSOutraUF.cs" company="ACBr.Net">
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
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;
using System.Xml.Serialization;

namespace ACBr.Net.CTe
{
    public sealed class CTeICMSOutraUF : GenericClone<CTeICMSOutraUF>, ICTeICMS, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeICMSOutraUF()
        {
            CST = DFeICMSCst.Cst90;
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.Enum, "CST", Id = "#243", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeICMSCst CST { get; set; }

        [XmlElementAttribute(Order = 1, ElementName = "pRedBCOutraUF")]
        [DFeElement(TipoCampo.De2, "pRedBCOutraUF", Id = "#244", Min = 1, Max = 5, Ocorrencia = Ocorrencia.MaiorQueZero)]
        public decimal PRedBCOutraUF { get; set; }

        [XmlElementAttribute(Order = 2, ElementName = "vBCOutraUF")]
        [DFeElement(TipoCampo.De2, "vBCOutraUF", Id = "#245", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VBCOutraUF { get; set; }

        [XmlElementAttribute(Order = 3, ElementName = "pICMSOutraUF")]
        [DFeElement(TipoCampo.De2, "pICMSOutraUF", Id = "#246", Min = 1, Max = 5, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal PICMSOutraUF { get; set; }

        [XmlElementAttribute(Order = 4, ElementName = "vICMSOutraUF")]
        [DFeElement(TipoCampo.De2, "vICMSOutraUF", Id = "#247", Min = 1, Max = 15, Ocorrencia = Ocorrencia.Obrigatoria)]
        public decimal VICMSOutraUF { get; set; }

        #endregion Propriedades
    }
}