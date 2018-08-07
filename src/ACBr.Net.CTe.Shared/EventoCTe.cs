// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-22-2017
//
// Last Modified By : RFTD
// Last Modified On : 10-22-2017
// ***********************************************************************
// <copyright file="EventoCTe.cs" company="ACBr.Net">
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

using ACBr.Net.CTe.Eventos;

namespace ACBr.Net.CTe
{
    public static class EventoCTe
    {
        public static CTeEvCancCTe Cancelamento(string nProt, string xJust)
        {
            return new CTeEvCancCTe() { NProt = nProt, XJust = xJust };
        }

        public static CTeEvCCeCTe CCe(CTeInfCorrecao[] correcoes)
        {
            var cceCTe = new CTeEvCCeCTe();
            cceCTe.InfCorrecao.AddRange(correcoes);
            return cceCTe;
        }

        public static CTeEvPrestDesacordo PrestacaoDesacordo(bool indicadorDesacordo, string observacao)
        {
            return new CTeEvPrestDesacordo
            {
                IndDesacordoOper = indicadorDesacordo,
                Obs = observacao
            };
        }

        public static CTeEvRegMultimodal RegMultimodal(string registro, string nDoc)
        {
            return new CTeEvRegMultimodal
            {
                Registro = registro,
                NDoc = nDoc
            };
        }

        public static CTeEvEPEC EPEC()
        {
            return new CTeEvEPEC();
        }
    }
}