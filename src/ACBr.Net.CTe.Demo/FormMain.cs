using ACBr.Net.DFe.Core.Common;
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
using System.Diagnostics;

namespace ACBr.Net.CTe.Demo
{
    public partial class FormMain : Form, IACBrLog
    {
        #region Fields

        private bool loaded;
        private ACBrConfig config;

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
            cbbUfWebservice.EnumDataSource(DFeCodUF.MS, DFeCodUF.EX, DFeCodUF.AN);
            loaded = true;

            LoadConfig();
            LoadServices();
            InitializeLog();
        }

        #endregion Constructors

        #region Methods

        #region EventHandlers

        private void acbrCTe_StatusChanged(object sender, EventArgs e)
        {
            FormStatus.ShowStatus(acbrCTe.Status);
        }

        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadServices();
        }

        private void btnSalvarConfig_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show(@"Configuração salva com sucesso.", @"ACBrCTe");
        }

        private void btnSelecionarSchema_Click(object sender, EventArgs e)
        {
            txtSchemas.Text = Helpers.SelectFolder();
        }

        private void btnSelecionarArquivo_Click(object sender, EventArgs e)
        {
            txtArquivoServicos.Text = Helpers.OpenFile(@"Arquivo de endereços (*.cte)|*.cte|Todos os Arquivos (*.*)|*.*");
        }

        private void btnPathCTe_Click(object sender, EventArgs e)
        {
            txtPathCTe.Text = Helpers.SelectFolder();
        }

        private void btnPathSalvar_Click(object sender, EventArgs e)
        {
            txtArquivosSoap.Text = Helpers.SelectFolder();
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

        private void btnConsultarSituacao_Click(object sender, EventArgs e)
        {
            var ret = acbrCTe.ConsultarSituacaoServico();

            wbbResposta.LoadXml(ret.XmlRetorno);
            wbbRetorno.LoadXml(ret.RetornoWS);
            wbbDados.LoadXml(ret.XmlEnvio);

            rtLogResposta.AppendLine("");
            rtLogResposta.AppendLine("Status Serviço");
            rtLogResposta.AppendLine($"tpAmb:     {ret.Resultado.TipoAmbiente.GetDescription()}");
            rtLogResposta.AppendLine($"verAplic:  {ret.Resultado.VersaoAplicacao}");
            rtLogResposta.AppendLine($"cStat:     {ret.Resultado.CStat}");
            rtLogResposta.AppendLine($"xMotivo:   {ret.Resultado.Motivo}");
            rtLogResposta.AppendLine($"cUF:       {ret.Resultado.UF}");
            rtLogResposta.AppendLine($"dhRecbto:  {ret.Resultado.DhRecbto:G}");
            rtLogResposta.AppendLine($"tMed:      {ret.Resultado.TMed}");
            rtLogResposta.AppendLine($"dhRetorno: {ret.Resultado.DhRetorno:G}");
            rtLogResposta.AppendLine($"xObs:      {ret.Resultado.Obs}");

            tbcRespostas.SelectedTab = tabPageRespostas;
        }

        private void btnInutilizar_Click(object sender, EventArgs e)
        {
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
        }

        private void btnConsultarCadastro_Click(object sender, EventArgs e)
        {
        }

        private void btnCriarEnviar_Click(object sender, EventArgs e)
        {
        }

        private void btnConsultarRecibo_Click(object sender, EventArgs e)
        {
            var recibo = "";
            InputBox.Show("Consultar recibo.", "Digite o número do recibo", ref recibo);

            if (recibo.IsEmpty()) return;

            var ret = acbrCTe.ConsultaRecibo(recibo);
            wbbResposta.LoadXml(ret.XmlRetorno);
            wbbRetorno.LoadXml(ret.RetornoWS);
            wbbDados.LoadXml(ret.XmlEnvio);

            rtLogResposta.AppendLine("Consultar Recibo");

            tbcRespostas.SelectedTab = tabPageRespostas;
        }

        private void btnConsultarXml_Click(object sender, EventArgs e)
        {
            var file = Helpers.OpenFile(@"CTe Xml (CTe*.xml)|CTe*.xml|CTe Xml (*.xml)|*.xml|Todos os Arquivos (*.*)|*.*");
            if (file.IsEmpty()) return;

            acbrCTe.Conhecimentos.Load(file);
            var ret = acbrCTe.Consultar();
            wbbResposta.LoadXml(ret.XmlRetorno);
            wbbRetorno.LoadXml(ret.RetornoWS);
            wbbDados.LoadXml(ret.XmlEnvio);

            rtLogResposta.AppendLine("Consultar CTe");

            tbcRespostas.SelectedTab = tabPageRespostas;
        }

        private void btnConsultarChave_Click(object sender, EventArgs e)
        {
            var chave = "";
            InputBox.Show("Consultar recibo.", "Digite o número do recibo", ref chave);

            if (chave.IsEmpty()) return;

            var ret = acbrCTe.Consultar(chave);
            wbbResposta.LoadXml(ret.XmlRetorno);
            wbbRetorno.LoadXml(ret.RetornoWS);
            wbbDados.LoadXml(ret.XmlEnvio);

            rtLogResposta.AppendLine("Consultar CTe");

            tbcRespostas.SelectedTab = tabPageRespostas;
        }

        private void btnEnviarEPEC_Click(object sender, EventArgs e)
        {
        }

        private void btnImportarXml_Click(object sender, EventArgs e)
        {
            var xml = Helpers.OpenFile(@"Arquivo de endereços (*.xml)|*.xml|Todos os Arquivos (*.*)|*.*");
            if (xml.IsEmpty()) return;

            acbrCTe.Conhecimentos.Load(xml);
        }

        private void btnValidarXml_Click(object sender, EventArgs e)
        {
        }

        private void btnCancelarXml_Click(object sender, EventArgs e)
        {
        }

        private void btnCancelarChave_Click(object sender, EventArgs e)
        {
            var chave = "";
            InputBox.Show("Cancelar CTe.", "Digite a chave da CTe", ref chave);
            if (chave.IsEmpty()) return;

            var protocolo = "";
            InputBox.Show("Cancelar CTe.", "Digite o protocolo do CTe", ref protocolo);
            if (protocolo.IsEmpty()) return;

            var justificativa = "";
            InputBox.Show("Cancelar CTe.", "Digite a justificativa", ref justificativa);
            if (justificativa.IsEmpty()) return;

            var cnpj = "";
            InputBox.Show("Cancelar CTe.", "Digite o CNPJ.", ref cnpj);
            if (cnpj.IsEmpty()) return;

            var nSeq = "";
            InputBox.Show("Cancelar CTe.", "Digite o número sequencial do evento.", ref nSeq);
            if (nSeq.IsEmpty()) return;

            var cancCTe = EventoCTe.Cancelamento(protocolo, justificativa);

            var ret = acbrCTe.EnviarEvento(nSeq.ToInt32(), chave, cnpj, cancCTe);
            wbbResposta.LoadXml(ret.XmlRetorno);
            wbbRetorno.LoadXml(ret.RetornoWS);
            wbbDados.LoadXml(ret.XmlEnvio);

            rtLogResposta.AppendLine("Cancelar CTe");

            tbcRespostas.SelectedTab = tabPageRespostas;
        }

        private void listViewServicos_DoubleClick(object sender, EventArgs e)
        {
            if (listViewServicos.SelectedItems.Count < 1) return;

            var versao = cbbVersao.GetSelectedValue<CTeVersao>();
            var uf = cbbUF.GetSelectedValue<DFeCodUF>();
            var ambiente = cbbAmbiente.GetSelectedValue<DFeTipoAmbiente>();
            var serviceInfo = CTeServiceManager.Servicos[versao][uf][ambiente];

            var tipo = (ServicoCTe)listViewServicos.SelectedItems[0].Tag;
            var url = serviceInfo[tipo];

            InputBox.Show($"Endereço {tipo.GetDescription()}", "Digite a url.", ref url);

            if (url != serviceInfo[tipo]) serviceInfo[tipo] = url;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            var file = Helpers.OpenFile(@"Ini ACBr (*.ini)|*.ini|Todos os Arquivos (*.*)|*.*");
            if (file.IsEmpty()) return;

            CTeServiceManager.ImportIniACBr(file);
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
                item.Tag = tipo;
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

            acbrCTe.Configuracoes.Arquivos.PathSalvar = txtArquivosSoap.Text;
            acbrCTe.Configuracoes.WebServices.Salvar = chkSalvarSoap.Checked;
            acbrCTe.Configuracoes.WebServices.UF = cbbUfWebservice.GetSelectedValue<DFeCodUF>();
            acbrCTe.Configuracoes.WebServices.Ambiente = rdbProducao.Checked ? DFeTipoAmbiente.Producao : DFeTipoAmbiente.Homologacao;
            acbrCTe.Configuracoes.WebServices.AguardarConsultaRet = (uint)(nudTimeOut.Value / 100);

            acbrCTe.Configuracoes.Arquivos.PathSchemas = txtSchemas.Text;
            acbrCTe.Configuracoes.Arquivos.PathCTe = txtPathCTe.Text;
            acbrCTe.Configuracoes.Arquivos.ArquivoServicos = txtArquivoServicos.Text;
        }

        private void LoadConfig()
        {
            config = ACBrConfig.CreateOrLoad("cte.config");

            txtCertificado.Text = config.Get("Certificado", string.Empty);
            txtSenha.Text = config.GetCrypt("Senha", string.Empty);
            txtNumeroSerie.Text = config.Get("NumeroSerie", string.Empty);

            chkSalvarSoap.Checked = config.Get("SalvarSoap", acbrCTe.Configuracoes.WebServices.Salvar);
            txtArquivosSoap.Text = config.Get("PathSoap", acbrCTe.Configuracoes.Arquivos.PathSalvar);
            cbbUfWebservice.SetSelectedValue(config.Get("UF", acbrCTe.Configuracoes.WebServices.UF));
            rdbProducao.Checked = config.Get("Ambiente", acbrCTe.Configuracoes.WebServices.Ambiente) == DFeTipoAmbiente.Producao;
            rdbHomologacao.Checked = config.Get("Ambiente", acbrCTe.Configuracoes.WebServices.Ambiente) == DFeTipoAmbiente.Homologacao;
            nudTimeOut.Value = config.Get("TimeOut", (int)acbrCTe.Configuracoes.WebServices.AguardarConsultaRet * 100);

            txtSchemas.Text = config.Get("PathSchemas", acbrCTe.Configuracoes.Arquivos.PathSchemas);
            txtPathCTe.Text = config.Get("PathCTe", acbrCTe.Configuracoes.Arquivos.PathCTe);
            txtArquivoServicos.Text = config.Get("PathServicos", acbrCTe.Configuracoes.Arquivos.ArquivoServicos);

            AplicarConfig();
        }

        private void SaveConfig()
        {
            config.Set("Certificado", txtCertificado.Text);
            config.SetCrypt("Senha", txtSenha.Text);
            config.Set("NumeroSerie", txtNumeroSerie.Text);

            config.Set("SalvarSoap", chkSalvarSoap.Checked);
            config.Set("PathSoap", txtArquivosSoap.Text);
            config.Set("UF", (int)cbbUfWebservice.GetSelectedValue<DFeCodUF>());
            config.Set("Ambiente", rdbProducao.Checked ? "1" : "2");
            config.Set("TimeOut", (int)nudTimeOut.Value);
            config.Set("ProxyHost", txtProxyHost.Text);
            config.Set("ProxyPort", txtProxyPort.Text);
            config.Set("ProxyUser", txtProxyUser.Text);
            config.SetCrypt("ProxyPass", txtProxyPass.Text);

            config.Set("PathSchemas", txtSchemas.Text);
            config.Set("PathCTe", txtPathCTe.Text);
            config.Set("PathServicos", txtArquivoServicos.Text);

            config.Save();
            AplicarConfig();
        }

        #endregion Methods
    }
}