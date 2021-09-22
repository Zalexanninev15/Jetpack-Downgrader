using DarkUI.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace JetpackGUI
{
    public partial class About : Form
    {
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }
        public About() { InitializeComponent(); }
        void MsgWarning() { DarkMessageBox.ShowWarning(MSG[1], MSG[0]); }
        void darkButton5_Click(object sender, EventArgs e) { panel1.Visible = false; }
        void darkButton1_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/issues"); } catch { MsgWarning(); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/issues"); } }
        void darkButton1_Click_1(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader#authors"); } catch { MsgWarning(); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader#authors"); } }
        void darkButton3_Click(object sender, EventArgs e) { try { Process.Start("https://zalexanninev15.github.io/Jetpack-Downgrader"); } catch { MsgWarning(); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } }
        void darkButton4_Click(object sender, EventArgs e) { try { Process.Start("https://gtaforums.com/topic/969056-jetpack-downgrader"); } catch { MsgWarning(); Clipboard.SetText("https://gtaforums.com/topic/969056-jetpack-downgrader/"); } }

        string[] MSG = new string[2];

        void MyLang_Load(object sender, EventArgs e)
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
    }
}