// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-14-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-14-2016
// ***********************************************************************
// <copyright file="CTeIde.cs" company="ACBr.Net">
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

using ACBr.Net.Core.Extensions;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;
using System;
using System.ComponentModel;
using ACBr.Net.DFe.Core.Collection;

namespace ACBr.Net.CTe
{
    public sealed class CTeIde : DFeParentItem<CTeIde, InfCte>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeIde()
        {
            IndGlobalizado = CTeIndicador.Nao;
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.Enum, "cUF", Id = "#005", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCodUF CUF { get; set; }

        [DFeElement(TipoCampo.Int, "cCT", Id = "#006", Min = 8, Max = 8, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CCT { get; set; }

        [DFeElement(TipoCampo.Int, "CFOP", Id = "#007", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CFOP { get; set; }

        [DFeElement(TipoCampo.Str, "natOp", Id = "#008", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string NatOp { get; set; }

        [DFeElement(TipoCampo.Enum, "mod", Id = "#010", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public ModeloCTe Mod { get; set; }

        [DFeElement(TipoCampo.Int, "serie", Id = "#011", Min = 1, Max = 3, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int Serie { get; set; }

        [DFeElement(TipoCampo.Int, "nCT", Id = "#012", Min = 1, Max = 9, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int NCT { get; set; }

        [DFeElement(TipoCampo.DatHorTz, "dhEmi", Id = "#013", Min = 25, Max = 25, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTimeOffset DhEmi { get; set; }

        [DFeElement(TipoCampo.Enum, "tpImp", Id = "#014", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoImpressao TpImp { get; set; }

        [DFeElement(TipoCampo.Enum, "tpEmis", Id = "#015", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoEmissao TpEmis { get; set; }

        [DFeElement(TipoCampo.Int, "cDV", Id = "#016", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CDV { get; set; }

        [DFeElement(TipoCampo.Enum, "tpAmb", Id = "#017", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeTipoAmbiente TpAmb { get; set; }

        [DFeElement(TipoCampo.Enum, "tpCTe", Id = "#018", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeTipo TpCTe { get; set; }

        [DFeElement(TipoCampo.Enum, "procEmi", Id = "#019", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeProcessoEmissao ProcEmi { get; set; }

        [DFeElement(TipoCampo.Str, "verProc", Id = "#020", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string VerProc { get; set; }

        [DFeElement(TipoCampo.Enum, "indGlobalizado", Id = "#021", Min = 1, Max = 1, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeIndicador IndGlobalizado { get; set; }

        [DFeElement(TipoCampo.Int, "cMunEnv", Id = "#022", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CMunEnv { get; set; }

        [DFeElement(TipoCampo.Str, "xMunEnv", Id = "#023", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMunEnv { get; set; }

        [DFeElement(TipoCampo.Enum, "UFEnv", Id = "#024", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCodUF UFEnv { get; set; }

        [DFeElement(TipoCampo.Enum, "modal", Id = "#025", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeModal Modal { get; set; }

        [DFeElement(TipoCampo.Enum, "tpServ", Id = "#026", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeTipoServico TpServ { get; set; }

        [DFeElement(TipoCampo.Int, "cMunIni", Id = "#027", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CMunIni { get; set; }

        [DFeElement(TipoCampo.Str, "xMunIni", Id = "#028", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMunIni { get; set; }

        [DFeElement(TipoCampo.Enum, "UFIni", Id = "#029", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeSiglaUF UFIni { get; set; }

        [DFeElement(TipoCampo.Int, "cMunFim", Id = "#030", Min = 7, Max = 7, Ocorrencia = Ocorrencia.Obrigatoria)]
        public int CMunFim { get; set; }

        [DFeElement(TipoCampo.Str, "xMunFim", Id = "#031", Min = 1, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XMunFim { get; set; }

        [DFeElement(TipoCampo.Enum, "UFFim", Id = "#032", Min = 2, Max = 2, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeSiglaUF UFFim { get; set; }

        [DFeElement(TipoCampo.Enum, "retira", Id = "#033", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeRetira Retira { get; set; }

        [DFeElement(TipoCampo.Str, "xDetRetira", Id = "#034", Min = 1, Max = 160, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string XDetRetira { get; set; }

        [DFeElement(TipoCampo.Enum, "indIEToma", Id = "#035", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeIndIEToma IndIEToma { get; set; }

        [DFeItem(typeof(CTeToma3), "toma3")]
        [DFeItem(typeof(CTeToma03), "toma03")]
        [DFeItem(typeof(CTeToma4), "toma4")]
        public ICTeTomador Tomador { get; set; }

        [DFeElement(TipoCampo.Custom, "dhCont", Id = "#057", Min = 1, Max = 21, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DateTime DhCont { get; set; }

        [DFeElement(TipoCampo.Str, "xJust", Id = "#058", Min = 15, Max = 256, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XJust { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeIndGlobalizado()
        {
            return Parent.Versao == CTeVersao.v300 && Mod == ModeloCTe.CTe && IndGlobalizado == CTeIndicador.Sim;
        }

        private bool ShouldSerializeIndIEToma()
        {
            return Parent.Versao == CTeVersao.v300 && Mod == ModeloCTe.CTeOS;
        }

        private bool ShouldSerializeRetira()
        {
            return Mod == ModeloCTe.CTe;
        }

        private bool ShouldSerializeXDetRetira()
        {
            return Mod == ModeloCTe.CTe;
        }

        private bool ShouldSerializeTomador()
        {
            return Mod == ModeloCTe.CTe;
        }

        private bool ShouldSerializeDhCont()
        {
            return TpEmis == DFeTipoEmissao.FSDA;
        }

        private bool ShouldSerializeXJust()
        {
            return TpEmis == DFeTipoEmissao.FSDA;
        }

        private string SerializeDhCont()
        {
            return Parent.Versao == CTeVersao.v300
                ? DhCont.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz")
                : DhCont.ToString("s");
        }

        private object DeserializeDhCont(string value)
        {
            return value.ToData();
        }

        #endregion Methods
    }
}

#pragma warning restore