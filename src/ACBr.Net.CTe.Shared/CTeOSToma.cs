﻿// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : marcosgerene
// Created          : 09-01-2019
//
// Last Modified By : marcosgerene
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="CTeToma03.cs" company="ACBr.Net">
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

using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeOSToma : DFeParentItem<CTeOSToma, CTeInfCTe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeOSToma()
        {
            EnderToma = new CTeEndereco();
        }

        public CTeOSToma(CTeInfCTe parent) : this()
        {
            Parent = parent;
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Id = "#065", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        [DFeElement(TipoCampo.StrNumberFill, "CPF", Id = "#066", Min = 11, Max = 11, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CPF { get; set; }

        [DFeElement(TipoCampo.Custom, "IE", Id = "#067", Min = 0, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IE { get; set; }

        [DFeElement(TipoCampo.Str, "xNome", Id = "#068", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNome { get; set; }

        [DFeElement(TipoCampo.Str, "xFant", Id = "#069", Min = 2, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XFant { get; set; }

        [DFeElement(TipoCampo.StrNumber, "fone", Id = "#118", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        [DFeElement("enderToma", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeEndereco EnderToma { get; set; }

        [DFeElement(TipoCampo.Str, "email", Id = "#130", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Email { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeCPF()
        {
            return CNPJ.IsEmpty();
        }

        private bool ShouldSerializeCNPJ()
        {
            return CPF.IsEmpty();
        }

        private string SerializeIE()
        {
            return IE.Trim().ToUpper() == CTeConst.CTeIEIsento ? IE.Trim().ToUpper() : IE.OnlyNumbers();
        }

        private object DeserializeIE(string value)
        {
            return value;
        }

        private string SerializeXNome()
        {
            return Parent.Ide.TpAmb == DFeTipoAmbiente.Homologacao ? CTeConst.CTeHomologação : XNome;
        }

        private object DeserializeXNome(string value)
        {
            return value;
        }

        #endregion Methods
    }
}