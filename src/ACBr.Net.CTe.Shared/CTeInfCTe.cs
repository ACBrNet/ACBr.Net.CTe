// ***********************************************************************
// Assembly         : ACBr.Net.CTe
// Author           : RFTD
// Created          : 10-12-2016
//
// Last Modified By : RFTD
// Last Modified On : 06-22-2018
// ***********************************************************************
// <copyright file="CTeInfCTe.cs" company="ACBr.Net">
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
using System.Linq;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Generics;
using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Serializer;

namespace ACBr.Net.CTe
{
    public sealed class CTeInfCTe : GenericClone<CTeInfCTe>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Fields

        private CTeIde ide;
        private CTeOSToma toma;
        private CTeExped exped;
        private CTeRem rem;
        private CTeReceb receb;
        private CTeDest dest;

        #endregion Fields

        #region Contructors

        public CTeInfCTe()
        {
            InfRespTec = new CTeInfRespTec();
            AutXml = new DFeCollection<CTeAutXML>();
            Imp = new CTeImp();
            VPrest = new CTeVPrest();
            Dest = new CTeDest(this);
            Receb = new CTeReceb(this);
            exped = new CTeExped(this);
            rem = new CTeRem(this);
            Emit = new CTeEmit();
            Compl = new CTeCompl();
            Ide = new CTeIde();
            Toma = new CTeOSToma();
            Versao = CTeVersao.v300;
        }

        #endregion Contructors

        #region Propriedades

        [DFeAttribute(TipoCampo.Enum, "versao", Id = "#002", Min = 4, Max = 4, Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeVersao Versao { get; set; }

        [DFeAttribute(TipoCampo.Str, "Id", Id = "#003", Min = 44, Max = 44, Ocorrencia = Ocorrencia.Obrigatoria)]
        public string Id { get; set; }

        [DFeElement("ide", Id = "#004", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeIde Ide
        {
            get => ide;
            set
            {
                if (value.Parent != this)
                {
                    value.Parent = this;
                }

                ide = value;
            }
        }

        [DFeElement("compl", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeCompl Compl { get; set; }

        [DFeElement("emit", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeEmit Emit { get; set; }

        [DFeElement("toma", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeOSToma Toma
        {
            get => toma;
            set
            {
                if (value.Parent != this)
                {
                    value.Parent = this;
                }

                toma = value;
            }
        }
        [DFeElement("rem", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeRem Rem
        {
            get => rem;
            set
            {
                if (value.Parent != this)
                {
                    value.Parent = this;
                }

                rem = value;
            }
        }

        [DFeElement("exped", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeExped Exped
        {
            get => exped;
            set
            {
                if (value.Parent != this)
                {
                    value.Parent = this;
                }

                exped = value;
            }
        }

        [DFeElement("receb", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeReceb Receb
        {
            get => receb;
            set
            {
                if (value.Parent != this)
                {
                    value.Parent = this;
                }

                receb = value;
            }
        }

        [DFeElement("dest", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeDest Dest
        {
            get => dest;
            set
            {
                if (value.Parent != this)
                {
                    value.Parent = this;
                }

                dest = value;
            }
        }

        [DFeElement("vPrest", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeVPrest VPrest { get; set; }

        [DFeElement("imp", Ocorrencia = Ocorrencia.Obrigatoria)]
        public CTeImp Imp { get; set; }

        [DFeItem(typeof(CTeOSNormal), "infCTeNorm")]
        [DFeItem(typeof(CTeNormal), "infCTeNorm")]
        [DFeItem(typeof(CTeAnulacao), "infCteAnu")]
        [DFeItem(typeof(CTeComplemento), "infCteComp")]
        public IInfoCTe InfoCTe { get; set; }

        [DFeCollection("autXML", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public DFeCollection<CTeAutXML> AutXml { get; set; }

        [DFeElement("infRespTec", Ocorrencia = Ocorrencia.NaoObrigatoria)]
        public CTeInfRespTec InfRespTec { get; set; }

        #endregion Propriedades

        #region Methods

        private bool ShouldSerializeCompl()
        {
            return !Compl.XCaracAd.IsEmpty() ||
                   !Compl.XCaracSer.IsEmpty() ||
                   !Compl.XEmi.IsEmpty() ||
                   !Compl.OrigCalc.IsEmpty() ||
                   !Compl.DestCalc.IsEmpty() ||
                   !Compl.XObs.IsEmpty() ||
                   Compl.ShouldSerializeFluxo() ||
                   Compl.ShouldSerializeEntrega() ||
                   Compl.ObsCont.Any() ||
                   Compl.ObsFisco.Any();
        }

        private bool ShouldSerializeToma()
        {
            return Ide.Mod == ModeloCTe.CTeOS && (!Toma.CNPJ.IsEmpty() || !Toma.CPF.IsEmpty() || !Toma.XNome.IsEmpty());
        }

        private bool ShouldSerializeRem()
        {
            return Ide.Mod == ModeloCTe.CTe && (!Rem.CNPJ.IsEmpty() || !Rem.CPF.IsEmpty() || !Rem.XNome.IsEmpty());
        }

        private bool ShouldSerializeExped()
        {
            return Ide.Mod == ModeloCTe.CTe && (!Exped.CNPJ.IsEmpty() || !Exped.CPF.IsEmpty() || !Exped.XNome.IsEmpty());
        }

        private bool ShouldSerializeReceb()
        {
            return Ide.Mod == ModeloCTe.CTe && (!Receb.CNPJ.IsEmpty() || !Receb.CPF.IsEmpty() || !Receb.XNome.IsEmpty());
        }

        private bool ShouldSerializeDest()
        {
            return Ide.Mod == ModeloCTe.CTe && (!Dest.CNPJ.IsEmpty() || !Dest.CPF.IsEmpty() || !Dest.XNome.IsEmpty());
        }

        private bool ShouldSerializeInfRespTec() => !string.IsNullOrEmpty(InfRespTec.CNPJ) || !string.IsNullOrEmpty(InfRespTec.XNome);

        #endregion Methods
    }
}

#pragma warning restore