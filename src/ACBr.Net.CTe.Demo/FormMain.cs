﻿using ACBr.Net.DFe.Core.Common;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACBr.Net.Core.Extensions;
using ACBr.Net.Core.Logging;
using ACBr.Net.CTe.Services;

namespace ACBr.Net.CTe.Demo
{
    public partial class FormMain : Form, IACBrLog
    {
        #region Fields

        private bool loaded;
        private ACBrConfig config;
        private const string Key = @"a1!B78s!5(h23y1g12\t\98w";

        #endregion Fields

        #region Constructors

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            cbbVersao.EnumDataSource(CTeVersao.v300, CTeVersao.v200);
            cbbAmbiente.EnumDataSource(DFeTipoAmbiente.Homologacao);
            cbbUF.EnumDataSource(DFeCodUF.MS, DFeCodUF.EX, DFeCodUF.AN);
            loaded = true;

            LoadConfig();
            LoadServices();
            InitializeLog();
        }

        #endregion Constructors

        #region Methods

        #region EventHandlers

        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadServices();
        }

        private void btnSalvarConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show(@"Configuração salva com sucesso.", @"ACBrCTe");
        }

        private void btnFindCertificate_Click(object sender, EventArgs e)
        {
            var file = Helpers.OpenFile(@"Arquivo de certificado (*.pfx)|*.pfx|Todos os Arquivos (*.*)|*.*");
            if (file.IsEmpty()) return;

            txtCertificado.Text = file;
            txtNumeroSerie.Text = string.Empty;
        }

        private void btnGetCertificate_Click(object sender, EventArgs e)
        {
            txtNumeroSerie.Text = acbrCTe.Configuracoes.Certificados.SelecionarCertificado();
            if (!txtNumeroSerie.Text.IsEmpty()) txtCertificado.Text = string.Empty;
        }

        private void statusServiceButton_Click(object sender, EventArgs e)
        {
            var ret = acbrCTe.ConsultarSituacaoServico();
            wbbResposta.LoadXml(ret.XmlRetorno);
        }

        private void loadCTeButton_Click(object sender, EventArgs e)
        {
            var file = Helpers.OpenFile(@"CTe Xml (CTe*.xml)|CTe*.xml|CTe Xml (*.xml)|*.xml|Todos os Arquivos (*.*)|*.*");
            if (file.IsEmpty()) return;

            acbrCTe.Conhecimentos.Load(file);
            wbbXml.LoadXml(acbrCTe.Conhecimentos.Last().GetXml());
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            var file = Helpers.OpenFile(@"Ini ACBr (*.ini)|*.ini|Todos os Arquivos (*.*)|*.*");
            if (file.IsEmpty()) return;

            CTeServiceManager.ImportIni(file);
            LoadServices();
            MessageBox.Show(@"Importação realizada com sucesso.", @"ACBrCTe");
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            using (var ofd = new FolderBrowserDialog())
            {
                ofd.SelectedPath = Application.StartupPath;

                if (ofd.ShowDialog(this) != DialogResult.OK) return;

                CTeServiceManager.Save(Path.Combine(ofd.SelectedPath, "services.cte"));
                MessageBox.Show(@"Arquivo salvo com sucesso.", @"ACBrCTe");
            }
        }

        #endregion EventHandlers

        private void InitializeLog()
        {
            var logConfig = new LoggingConfiguration();
            var target = new RichTextBoxTarget
            {
                UseDefaultRowColoringRules = true,
                Layout = @"${date:format=dd/MM/yyyy HH\:mm\:ss} - ${message}",
                FormName = Name,
                ControlName = rtbLog.Name,
                AutoScroll = true
            };

            logConfig.AddTarget("RichTextBox", target);
            logConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));

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

            logConfig.AddTarget("infoFile", infoTarget);
            logConfig.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, infoTarget));
            LogManager.Configuration = logConfig;
        }

        private void LoadServices()
        {
            if (!loaded) return;

            var versao = cbbVersao.GetSelectedValue<CTeVersao>();
            var uf = cbbUF.GetSelectedValue<DFeCodUF>();
            var ambiente = cbbAmbiente.GetSelectedValue<DFeTipoAmbiente>();

            var serviceInfo = CTeServiceManager.Servicos[versao][uf][ambiente];
            var servicesType = (from ServicoCTe value in Enum.GetValues(typeof(ServicoCTe)) select value).ToArray();

            var serviceList = new List<ListViewItem>();

            foreach (var tipo in servicesType)
            {
                var item = new ListViewItem(versao.GetDescription());
                item.SubItems.Add(uf.GetDescription());
                item.SubItems.Add(tipo.GetDescription());
                item.SubItems.Add(serviceInfo[tipo]);
                serviceList.Add(item);
            }

            listViewServicos.BeginUpdate();
            listViewServicos.Items.Clear();
            listViewServicos.Items.AddRange(serviceList.ToArray());
            listViewServicos.EndUpdate();
        }

        private void AplicarConfig()
        {
            acbrCTe.Configuracoes.Certificados.Certificado = !txtCertificado.Text.IsEmpty() ? txtCertificado.Text : txtNumeroSerie.Text;
            acbrCTe.Configuracoes.Certificados.Senha = txtSenha.Text;
        }

        private void LoadConfig()
        {
            config = ACBrConfig.CreateOrLoad(Path.Combine(Application.StartupPath, "cte.config"));

            txtCertificado.Text = config.Get("Certificado", string.Empty);
            var senha = config.Get("Senha", string.Empty);
            if (!senha.IsEmpty()) txtSenha.Text = senha.Decrypt(Key);
            txtNumeroSerie.Text = config.Get("NumeroSerie", string.Empty);

            AplicarConfig();
        }

        private void SaveConfig()
        {
            config.Set("Certificado", txtCertificado.Text);
            config.Set("Senha", !txtSenha.Text.IsEmpty() ? txtSenha.Text.Encrypt(Key) : txtSenha.Text);
            config.Set("NumeroSerie", txtNumeroSerie.Text);

            config.Save();
            AplicarConfig();
        }

        #endregion Methods
    }
}