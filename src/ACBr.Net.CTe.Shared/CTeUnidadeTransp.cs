// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-15-2016
//
// Last Modified By : RFTD
// Last Modified On : 10-16-2016
// ***********************************************************************
// <copyright file="CTeUnidadeTransp.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Common;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeUnidadeTransp : GenericClone<CTeUnidadeTransp>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        public CTeUnidadeTransp()
        {
            InfUnidCarga = new DFeCollection<CTeTUnidCarga>();
            LacUnidTransp = new DFeCollection<CTeLacre>();
        }

        #endregion Constructors

        #region Propriedades

        [DFeElement(TipoCampo.Enum, "tpUnidTransp", Id = "#280", Min = 1, Max = 1, Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeUnidTransp TpUnidTransp { get; set; }

        [DFeElement(TipoCampo.StrNumber, "idUnidTransp", Id = "#281", Min = 1, Max = 20, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string IdUnidTransp { get; set; }

        [DFeCollection("lacUnidTransp", Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<CTeLacre> LacUnidTransp { get; set; }

        [DFeCollection("infUnidCarga", Ocorrencia = Ocorrencia.Obrigatoria)]
        public DFeCollection<CTeTUnidCarga> InfUnidCarga { get; set; }

        [DFeElement(TipoCampo.De2, "qtdRat", Id = "#280", Min = 1, Max = 5, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public decimal? QtdRat { get; set; }

        #endregion Propriedades
    }
}