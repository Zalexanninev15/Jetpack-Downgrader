using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using VitNX.Forms;
using VitNX.Win32;

namespace JetpackGUI
{
    public partial class About : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            if (NativeFunctions.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                NativeFunctions.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
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
            try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/issues"); }
            catch
            {
                MsgWarning();
                Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/issues");
            }
        }

        private void darkButton1_Click_1(object sender, EventArgs e)
        {
            try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader#authors"); }
            catch
            {
                MsgWarning();
                Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader#authors");
            }
        }

        private void darkButton3_Click(object sender, EventArgs e)
        {
            try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader"); }
            catch
            {
                MsgWarning();
                Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader");
            }
        }

        private void darkButton4_Click(object sender, EventArgs e)
        {
            try { Process.Start("https://gtaforums.com/topic/969056-jetpack-downgrader"); }
            catch
            {
                MsgWarning();
                Clipboard.SetText("https://gtaforums.com/topic/969056-jetpack-downgrader/");
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
            using (StringReader reader = new StringReader(File.ReadAllText(@Application.StartupPath + @"\files\languages\" + langcode + ".xml")))
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

        private int db = 0;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //if (Data.DebugMode)
            //{
            //    try
            //    {
            //        db += 1;
            //        if (db == 10) { WDebug wdb = new WDebug(); wdb.ShowDialog(); }
            //    }
            //    catch { }
            //}
        }
    }
}