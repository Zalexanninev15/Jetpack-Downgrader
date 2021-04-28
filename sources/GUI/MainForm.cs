using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;

namespace JetpackDowngraderGUI
{
    public partial class MainForm : Form
    {
        string[] lc = new string[100];
        bool[] appset = new bool[8];
        public MainForm() { InitializeComponent(); }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Loading the localization
                IniEditor lang = new IniEditor(@Application.StartupPath + @"\languages\" + Properties.Settings.Default.LanguageCode + ".txt");
                // Text (GUI) loading
                label1.Text = Convert.ToString(lang.GetValue("Interface", "PathLabel"));
                button2.Text = Convert.ToString(lang.GetValue("Interface", "CButton"));
                button4.Text = Convert.ToString(lang.GetValue("Interface", "LButton"));
                // Title loading
                lc[0] = Convert.ToString(lang.GetValue("Title", "Info"));
                lc[1] = Convert.ToString(lang.GetValue("Title", "Error"));
                // InfoMsg loading
                lc[4] = Convert.ToString(lang.GetValue("InfoMsg", "Succes"));
                // ErrorMsg loading
                lc[2] = Convert.ToString(lang.GetValue("ErrorMsg", "ReadINI"));
                lc[3] = Convert.ToString(lang.GetValue("ErrorMsg", "WriteINI"));
                lc[5] = Convert.ToString(lang.GetValue("ErrorMsg", "BrowserNotFound"));
            }
            catch { MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            // Loading settings
            try
            {

                IniEditor cfg = new IniEditor(@Application.StartupPath + @"\app\jpd.ini");
                checkBox1.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackups"));
                checkBox2.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateShortcut"));
                checkBox9.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "ResetGame"));
                checkBox4.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "RGLGarbage"));
                checkBox6.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "RegisterGamePath"));
                checkBox3.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateNewGamePath"));
                checkBox5.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "Forced"));
                checkBox7.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "EnableDirectPlay"));
                checkBox8.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "InstallDirectX"));
                appset[0] = Convert.ToBoolean(cfg.GetValue("JPD", "SelectFolder"));
                appset[1] = Convert.ToBoolean(cfg.GetValue("JPD", "ConsoleTransparency"));
                appset[2] = Convert.ToBoolean(cfg.GetValue("JPD", "UseMsg"));
                appset[3] = Convert.ToBoolean(cfg.GetValue("JPD", "UseProgressBar"));
                appset[4] = Convert.ToBoolean(cfg.GetValue("JPD", "Component"));
                appset[5] = Convert.ToBoolean(cfg.GetValue("Only", "GameVersion"));
                appset[6] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFiles"));
                appset[7] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFilesAndCheckMD5"));
            }
            catch { MsgError(lc[2], lc[1]); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(@Application.StartupPath + @"\app\jpd.exe").WaitForExit();
            // Install mods

            //
            MsgInfo(lc[4], lc[0]);
        }

        private void button5_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } catch { MsgError(lc[5], lc[1]); } }
        private void MsgInfo(string message, string title) { MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void MsgError(string message, string title) { MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void button4_Click(object sender, EventArgs e)
        {
            // Change language
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
