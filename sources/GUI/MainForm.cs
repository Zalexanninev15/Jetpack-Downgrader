using System;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace JetpackDowngraderGUI
{
    public partial class MainForm : Form
    {
        string[] lc = new string[100];
        bool[] appset = new bool[8];
        public MainForm() { InitializeComponent(); }
        IniEditor cfg = new IniEditor(@Application.StartupPath + @"\app\jpd.ini");
        IniEditor lang = new IniEditor(@Application.StartupPath + @"\languages\" + Properties.Settings.Default.LanguageCode + ".txt");
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Loading the localization
                // Text (GUI) loading
                label1.Text = Convert.ToString(lang.GetValue("Interface", "PathLabel"));
                DSPanel.SectionHeader = Convert.ToString(lang.GetValue("Interface", "Tab1"));
                button6.Text = "1. " + DSPanel.SectionHeader;
                button2.Text = "2. " + Convert.ToString(lang.GetValue("Interface", "Tab2"));
                button1.Text = "3. " + Convert.ToString(lang.GetValue("Interface", "Downgrade"));
                button3.Text = "4. " + Convert.ToString(lang.GetValue("Interface", "Play"));
                // CheckBox loading
                checkBox1.Text = Convert.ToString(lang.GetValue("CheckBox", "Backup"));
                checkBox2.Text = Convert.ToString(lang.GetValue("CheckBox", "Shortcut"));
                checkBox9.Text = Convert.ToString(lang.GetValue("CheckBox", "Reset"));
                checkBox4.Text = Convert.ToString(lang.GetValue("CheckBox", "GarbageCleaning"));
                checkBox6.Text = Convert.ToString(lang.GetValue("CheckBox", "GameReg"));
                checkBox3.Text = Convert.ToString(lang.GetValue("CheckBox", "NoUpdates"));
                checkBox5.Text = Convert.ToString(lang.GetValue("CheckBox", "Forced"));
                checkBox7.Text = Convert.ToString(lang.GetValue("CheckBox", "DirectPlay"));
                checkBox8.Text = Convert.ToString(lang.GetValue("CheckBox", "InstallDirectX"));
                // Title loading
                lc[0] = Convert.ToString(lang.GetValue("Title", "Info"));
                lc[1] = Convert.ToString(lang.GetValue("Title", "Error"));
                lc[8] = Convert.ToString(lang.GetValue("Title", "Warning"));
                lc[6] = Convert.ToString(lang.GetValue("Title", "FolderSelectDialog"));
                // InfoMsg loading
                lc[4] = Convert.ToString(lang.GetValue("InfoMsg", "Succes"));
                lc[9] = Convert.ToString(lang.GetValue("InfoMsg", "Version"));
                lc[10] = Convert.ToString(lang.GetValue("InfoMsg", "Author"));
                // ErrorMsg loading
                lc[2] = Convert.ToString(lang.GetValue("ErrorMsg", "ReadINI"));
                lc[3] = Convert.ToString(lang.GetValue("ErrorMsg", "WriteINI"));
                // WarningMsg loading
                lc[7] = Convert.ToString(lang.GetValue("WarningMsg", "PathNotFound"));
                lc[5] = Convert.ToString(lang.GetValue("WarningMsg", "BrowserNotFound"));
            }
            catch { MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            // Loading settings
            try
            {
                checkBox1.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackups"));
                checkBox2.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateShortcut"));
                checkBox9.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "ResetGame"));
                checkBox4.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "GarbageCleaning"));
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

        async void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@GamePath.Text))
            {
                this.Enabled = false;
                button3.Enabled = false;
                int d = 0;
                cfg.SetValue("JPD", "SelectFolder", "false");
                cfg.SetValue("JPD", "UseMsg", "false");
                cfg.SetValue("JPD", "Component", "true");
                Process.Start(@Application.StartupPath + @"\app\jpd.exe", "\"" + GamePath.Text + "\"").WaitForExit();
                string str = "jpd";
                foreach (Process process2 in Process.GetProcesses())
                {
                    if (!process2.ProcessName.ToLower().Contains(str.ToLower())) { d = 1; }
                }
                if (d == 1)
                {
                    // Install mods

                    //
                    MsgInfo(lc[4], lc[0]);
                    this.Enabled = true;
                    button3.Enabled = true;
                }
            }
            else { MsgWarning(lc[7], lc[8]); }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderSelectDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Title = lc[6]
            };
            if (dialog.Show()) { GamePath.Text = dialog.FileName; } else { GamePath.Clear(); }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateShortcut", Convert.ToString(checkBox2.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateBackups", Convert.ToString(checkBox1.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox4_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "GarbageCleaning", Convert.ToString(checkBox4.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox6_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "RegisterGamePath", Convert.ToString(checkBox6.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox5_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "Forced", Convert.ToString(checkBox5.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox8_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "InstallDirectX", Convert.ToString(checkBox8.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox7_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "EnableDirectPlay", Convert.ToString(checkBox7.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox3_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateNewGamePath", Convert.ToString(checkBox3.Checked).Replace("T", "t").Replace("F", "f")); }
        private void checkBox9_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "ResetGame", Convert.ToString(checkBox9.Checked).Replace("T", "t").Replace("F", "f")); }
        private void pictureBox4_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } catch { MsgError(lc[5], lc[1]); } }
        private void MsgInfo(string message, string title) { MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information); }
        private void MsgError(string message, string title) { MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private void MsgWarning(string message, string title) { MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        private void button7_Click(object sender, EventArgs e) { Process.Start("notepad.exe", @Application.StartupPath + @"\app\jpd.ini"); }
        private void pictureBox3_Click(object sender, EventArgs e) { MsgInfo("Jetpack Downgrader GUI\n" + lc[9] + ": " + Application.ProductVersion + "\n" + lc[10] + " Zalexanninev15", lc[0]); }
    }
}