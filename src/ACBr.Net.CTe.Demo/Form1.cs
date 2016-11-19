using ACBr.Net.CTe.Services;
using ACBr.Net.CTe.Services.StatusServico;
using ACBr.Net.DFe.Core;
using ACBr.Net.DFe.Core.Common;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Windows.Forms;
using System;
using System.Text;
using System.Windows.Forms;

namespace ACBr.Net.CTe.Demo
{
	public partial class Form1 : Form
	{
		#region Fields

		private ILogger logger;

		#endregion Fields

		#region Constructors

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			InitializeLog();
		}

		#endregion Constructors

		#region Methods

		protected override void OnLoad(EventArgs e)
		{
			comboBoxVersao.EnumDataSource<CTeVersao>(CTeVersao.v200);
			comboBoxAmbiente.EnumDataSource<DFeTipoAmbiente>(DFeTipoAmbiente.Homologacao);

			base.OnLoad(e);
		}

		private void InitializeLog()
		{
			var config = new LoggingConfiguration();
			var target = new RichTextBoxTarget
			{
				UseDefaultRowColoringRules = true,
				Layout = @"${date:format=dd/MM/yyyy HH\:mm\:ss} - ${message}",
				FormName = Name,
				ControlName = rtbLog.Name,
				AutoScroll = true
			};

			config.AddTarget("RichTextBox", target);
			config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));

			var infoTarget = new FileTarget
			{
				FileName = "${basedir:dir=Logs:file=ACBrCTe.log}",
				Layout = "${processid}|${longdate}|${level:uppercase=true}|" +
						 "${event-context:item=Context}|${logger}|${message}",
				CreateDirs = true,
				Encoding = Encoding.UTF8,
				MaxArchiveFiles = 93,
				ArchiveEvery = FileArchivePeriod.Day,
				ArchiveNumbering = ArchiveNumberingMode.Date,
				ArchiveFileName = "${basedir}/Logs/Archive/${date:format=yyyy}/${date:format=MM}/ACBrCTe_{{#}}.log",
				ArchiveDateFormat = "dd.MM.yyyy"
			};

			config.AddTarget("infoFile", infoTarget);
			config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, infoTarget));
			LogManager.Configuration = config;
			logger = LogManager.GetLogger("ACBrCTe");
		}

		private void buttonStatusService_Click(object sender, EventArgs e)
		{
			var info = ServiceManager.GetServiceAndress(CTeVersao.v200, DFeCodUF.RJ, TipoUrlServico.CTeStatusServico, DFeTipoAmbiente.Homologacao);
			var cert = CertificadoDigital.SelecionarCertificado();
			var cliente = new CTeStatusServicoServiceClient(info.Url, null, cert);
			var cabecalho = new CTeWsCabecalho
			{
				CUf = 33,
				VersaoDados = info.Versao
			};

			var mensagem = new ConsStatServCte(DFeTipoAmbiente.Homologacao, info.Versao);
			var ret = cliente.StatusServico(cabecalho, mensagem);
			MessageBox.Show(ret.Motivo);
		}

		#endregion Methods
	}
}