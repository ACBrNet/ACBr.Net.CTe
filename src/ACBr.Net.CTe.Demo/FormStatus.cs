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

        public static async void ShowStatus(StatusCTe status)
        {
            switch (status)
            {
                case StatusCTe.EmEspera:
                    instance?.Invoke(new Action(() => instance?.Close()));
                    instance = null;
                    return;

                case StatusCTe.StatusServico:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Verificando Status do servico...";
                    break;

                case StatusCTe.Recepcao:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando dados do CTe...";
                    break;

                case StatusCTe.RetRecepcao:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Recebendo dados do CTe...";
                    break;

                case StatusCTe.Consulta:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando CTe...";
                    break;

                case StatusCTe.Cancelamento:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando cancelamento de CTe...";
                    break;

                case StatusCTe.Inutilizacao:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando pedido de Inutilização...";
                    break;

                case StatusCTe.Recibo:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando Recibo de Lote...";
                    break;

                case StatusCTe.Cadastro:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando Cadastro...";
                    break;

                case StatusCTe.Email:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando Email...";
                    break;

                case StatusCTe.CCe:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando CCe...";
                    break;

                case StatusCTe.Evento:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Enviando Evento...";
                    break;

                case StatusCTe.DistDFeInt:
                    instance = new FormStatus();
                    instance.lblMenssagem.Text = @"Consultando Distribuição DFe...";
                    break;

                case StatusCTe.EnvioWebService:
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