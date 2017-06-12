using ACBr.Net.DFe.Core.Common;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACBr.Net.CTe.Demo
{
    public partial class FormMain : Form
    {
        #region Fields

        private ILogger logger;

        #endregion Fields

        #region Constructors

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitializeLog();
        }

        #endregion Constructors

        #region Methods

        #region EventHandlers

        private void statusServiceButton_Click(object sender, EventArgs e)
        {
            acbrCTe.Configuracoes.WebServices.Uf = DFeCodUF.RJ;
            acbrCTe.Configuracoes.Geral.VersaoCTe = CTeVersao.v200;
            acbrCTe.Configuracoes.Certificados.Certificado = acbrCTe.Configuracoes.Certificados.SelecionarCertificado();

            var ret = acbrCTe.ConsultarSituacaoServico();
            wbbResposta.LoadXml(ret.GetXml());
        }

        private void loadCTeButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.Filter = @"CTe Xml (*.xml)|*.xml";

                if (ofd.ShowDialog(this) != DialogResult.OK) return;

                acbrCTe.Conhecimentos.Load(ofd.FileName);
                wbbXml.LoadXml(acbrCTe.Conhecimentos.Last().GetXml());
            }
        }

        #endregion EventHandlers

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

        #endregion Methods
    }
}