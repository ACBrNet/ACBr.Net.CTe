// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : Marcos Gerene
// Created          : 09-10-2018
//
// Last Modified By : RFTD
// Last Modified On : 10-10-2018
// ***********************************************************************
// <copyright file="CTeInfRespTec.cs" company="ACBr.Net">
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
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    /// <summary>
    ///   TRespTec - Informações do Responsável Técnico pela emissão do DF-e
    /// </summary>
    public sealed class CTeInfRespTec : GenericClone<CTeInfRespTec>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Propriedades

        [DFeElement(TipoCampo.StrNumberFill, "CNPJ", Min = 14, Max = 14, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string CNPJ { get; set; }

        [DFeElement(TipoCampo.Str, "xContato", Min = 2, Max = 60, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string XNome { get; set; }

        [DFeElement(TipoCampo.Str, "email", Min = 1, Max = 60, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Email { get; set; }

        [DFeElement(TipoCampo.Str, "fone", Min = 6, Max = 14, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string Fone { get; set; }

        [DFeElement(TipoCampo.Str, "idCSRT", Min = 3, Max = 3, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string IdCSRT { get; set; }

        [DFeElement(TipoCampo.Str, "hashCSRT", Min = 28, Max = 28, Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public string HashCSRT { get; set; }

        #endregion Propriedades
    }
}