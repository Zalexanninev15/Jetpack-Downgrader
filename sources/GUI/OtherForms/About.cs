using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

using VitNX.Functions.Windows.Win32;
using VitNX.UI.ControlsV1.BasedOnDarkUI.Forms;
using VitNX.Functions.Windows.Apps;

namespace JetpackGUI
{
    public partial class About : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            if (Import.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                Import.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public About()
        {
            InitializeComponent();
        }

        private void MsgWarning()
        {
            VitNX_MessageBox.ShowWarning(MSG[1], MSG[0]);
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            if (!Processes.OpenLink(Urls.GitHubAppIssues))
            {
                MsgWarning();
                Clipboard.SetText(Urls.GitHubAppIssues);
            }
        }

        private void darkButton1_Click_1(object sender, EventArgs e)
        {
            if (!Processes.OpenLink(Urls.GitHubAppAuthors))
            {
                MsgWarning();
                Clipboard.SetText(Urls.GitHubAppAuthors);
            }
        }

        private void darkButton3_Click(object sender, EventArgs e)
        {
            if (!Processes.OpenLink(Urls.GitHub))
            {
                MsgWarning();
                Clipboard.SetText(Urls.GitHub);
            }
        }

        private void darkButton4_Click(object sender, EventArgs e)
        {
            if (!Processes.OpenLink(Urls.GTAForums))
            {
                MsgWarning();
                Clipboard.SetText(Urls.GTAForums);
            }
        }

        private string[] MSG = new string[2];

        private void MyLang_Load(object sender, EventArgs e)
        {
            MSG[0] = "Warning";
            this.Size = new System.Drawing.Size(485, 429);
            GUI language = new GUI();
            language.ReadXml();
            string langcode = language.Fields.LanguageCode;
            XmlSerializer lzol = new XmlSerializer(typeof(LanguagesString));
            using (StringReader reader = new StringReader(File.ReadAllText($@"{Application.StartupPath}\files\languages\{langcode}.xml")))
            {
                var LOCAL = (LanguagesString)lzol.Deserialize(reader);
                Text = LOCAL.AboutTitle;
                darkTextBox1.Text = "- " + LOCAL.Version + ": " + Convert.ToString(Application.ProductVersion);
                darkTextBox1.Text += "\r\n- " + LOCAL.Authors + ":\r\n~ Zalexanninev15 - " + LOCAL.Zalexanninev15 + "\r\n~ Vadim M. - " + LOCAL.VadimM;
                darkTextBox1.Text += "\r\n- " + LOCAL.License + ": MIT";
                darkTextBox1.Text += "\r\n- " + LOCAL.Localization + ": " + LOCAL.LocalizationBy;
                darkButton1.Text = LOCAL.AboutDonate;
                darkButton2.Text = LOCAL.AboutIssues;
                darkButton3.Text = LOCAL.AboutSite;
                darkButton4.Text = LOCAL.AboutTopic;
                MSG[0] = LOCAL.Warning;
                MSG[1] = LOCAL.BrowserNotFound;
            }
        }
    }
}