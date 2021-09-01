using DarkUI.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JetpackGUI
{
    public partial class About : Form
    {
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }
        public About() { InitializeComponent(); }
        void MsgWarning() { DarkMessageBox.ShowWarning(MSG[1], MSG[0]); }
        void darkButton1_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/issues"); } catch { MsgWarning(); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/issues"); } }
        void darkButton1_Click_1(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader#authors"); } catch { MsgWarning(); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader#authors"); } }
        void darkButton3_Click(object sender, EventArgs e) { try { Process.Start("https://zalexanninev15.github.io/Jetpack-Downgrader"); } catch { MsgWarning(); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } }
        void darkButton4_Click(object sender, EventArgs e) { try { Process.Start("https://gtaforums.com/topic/969056-jetpack-downgrader"); } catch { MsgWarning(); Clipboard.SetText("https://gtaforums.com/topic/969056-jetpack-downgrader/"); } }

        string[] MSG = new string[2];

        void MyLang_Load(object sender, EventArgs e)
        {
            MSG[0] = "Warning";
            this.Size = new System.Drawing.Size(485, 424);
            GUI language = new GUI();
            language.ReadXml();
            string langcode = language.Fields.LanguageCode;
            Editor lang = new Editor(@Application.StartupPath + @"\files\languages\" + langcode + ".zcf");
            Text = lang.GetValue("AboutTitle");
            darkTextBox1.Text = "- " + lang.GetValue("Version") + ": " + Convert.ToString(Application.ProductVersion);
            darkTextBox1.Text += "\r\n- " + lang.GetValue("Authors") + ":\r\n~ Zalexanninev15 - " + lang.GetValue("Zalexanninev15") + "\r\n~ Vadim M. - " + lang.GetValue("Vadim M.");
            darkTextBox1.Text += "\r\n- " + lang.GetValue("License") + ": MIT";
            darkTextBox1.Text += "\r\n- " + lang.GetValue("Localization") + ": " + lang.GetValue("LocalizationBy");
            darkButton1.Text = lang.GetValue("AboutDonate");
            darkButton2.Text = lang.GetValue("AboutIssues");
            darkButton3.Text = lang.GetValue("AboutSite");
            darkButton4.Text = lang.GetValue("AboutTopic");
            MSG[0] = lang.GetValue("Warning");
            MSG[1] = lang.GetValue("BrowserNotFound");
        }
    }
}
