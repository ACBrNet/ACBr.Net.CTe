using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACBr.Net.CTe.Demo
{
    public partial class FormStatus : Form
    {
        #region Fields

        private static FormStatus instance;
        private static CancellationToken cancelToken;

        #endregion Fields

        #region Constructors

        public FormStatus()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public static async void ShowStatus(StatusACBrCTe status)
        {
            switch (status)
            {
                case StatusACBrCTe.CTeIdle:
                    instance?.Invoke(new Action(() => instance?.Close()));
                    instance = null;
                    return;

                case StatusACBrCTe.CTeStatusServico:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Verificando Status do servico...";
                    break;

                case StatusACBrCTe.CTeRecepcao:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando dados do CTe...";
                    break;

                case StatusACBrCTe.CTeRetRecepcao:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Recebendo dados do CTe...";
                    break;

                case StatusACBrCTe.CTeConsulta:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando CTe...";
                    break;

                case StatusACBrCTe.CTeCancelamento:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando cancelamento de CTe...";
                    break;

                case StatusACBrCTe.CTeInutilizacao:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando pedido de Inutilização...";
                    break;

                case StatusACBrCTe.CTeRecibo:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando Recibo de Lote...";
                    break;

                case StatusACBrCTe.CTeCadastro:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando Cadastro...";
                    break;

                case StatusACBrCTe.CTeEmail:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando Email...";
                    break;

                case StatusACBrCTe.CTeCCe:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando CCe...";
                    break;

                case StatusACBrCTe.CTeEvento:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando Evento...";
                    break;

                case StatusACBrCTe.CTeDistDFeInt:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando Distribuição DFe...";
                    break;

                case StatusACBrCTe.CTeEnvioWebService:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando para o webservice...";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }

            await Task.Run(() => instance.ShowDialog());
        }

        #endregion Methods
    }
}