using DarkUI.Forms;
using DarkUI.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Net;
using CG.Web.MegaApiClient;

namespace JetpackGUI
{
    public partial class MainForm : Form
    {
        // Dark title for Windows 10
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }

        IniEditor cfg = new IniEditor(@Application.StartupPath + @"\files\jpd.ini");
        string[] lc = new string[100];
        string[] mse = new string[100];
        bool tabFix = false;
        bool lpFix = false;
        bool db = false;
        string cache = @Application.StartupPath + @"\files\mods_cache";
        string zip_link = "application/zip";
        string[] photos_links = new string[3];
        string site_link = "";

        public MainForm() 
        { 
            InitializeComponent();
            this.KeyPreview = true;
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!Directory.Exists(@Application.StartupPath + @"\files\patches"))
            {
                pictureBox8.Visible = true;
                button1.Enabled = false;
            }
            if (!Directory.Exists(cache))
            {
                Directory.CreateDirectory(cache);
            }
            Translate();
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
                checkBox8.Checked = Convert.ToBoolean(cfg.GetValue("Downgrader", "InstallDirectXComponents"));
            }
            catch { MsgError(lc[2], lc[1]); }
        }

        async void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@GamePath.Text))
            {
                this.Enabled = false;
                int d = 0;
                cfg.SetValue("JPD", "SelectFolder", "false");
                cfg.SetValue("JPD", "UseMsg", "false");
                cfg.SetValue("JPD", "Component", "true");
                Process.Start(@Application.StartupPath + @"\files\jpd.exe", "\"" + GamePath.Text + "\"").WaitForExit();
                string str = "jpd";
                foreach (Process process2 in Process.GetProcesses()) { if (!process2.ProcessName.ToLower().Contains(str.ToLower())) { d = 1; } }
                if (d == 1)
                {
                    // Install mods [TEST]
                    string[] modsZip = Directory.GetFiles(cache + @"\zips", "*.zip");
                    for (int i = 0; i < modsZip.Length; i++)
                    {
                        Process.Start(@Application.StartupPath + @"\files\7z", "x \"" + modsZip[0] + "\" -ao\"" + GamePath.Text + "\"").WaitForExit();
                     }
                    //
                    MsgInfo(lc[4], lc[0]);
                    this.Enabled = true;
                }
            }
            else { MsgWarning(lc[7], lc[8]); }
        }

        void pictureBox1_Click(object sender, EventArgs e)
        {
            var dialog = new FolderSelectDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Title = lc[6]
            };
            if (dialog.Show()) { GamePath.Text = dialog.FileName; } else { GamePath.Clear(); }
        }

        void checkBox2_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateShortcut", Convert.ToString(checkBox2.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox1_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateBackups", Convert.ToString(checkBox1.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox4_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "GarbageCleaning", Convert.ToString(checkBox4.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox6_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "RegisterGamePath", Convert.ToString(checkBox6.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox5_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "Forced", Convert.ToString(checkBox5.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox8_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "InstallDirectXComponents", Convert.ToString(checkBox8.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox7_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "EnableDirectPlay", Convert.ToString(checkBox7.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox3_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateNewGamePath", Convert.ToString(checkBox3.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox9_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "ResetGame", Convert.ToString(checkBox9.Checked).Replace("T", "t").Replace("F", "f")); }
        void MsgInfo(string message, string title) { DarkMessageBox.ShowInformation(message, title); }
        void MsgError(string message, string title) { DarkMessageBox.ShowError(message, title); }
        void MsgWarning(string message, string title) { DarkMessageBox.ShowWarning(message, title); }
        void button7_Click(object sender, EventArgs e) { Process.Start("notepad.exe", @Application.StartupPath + @"\files\jpd.ini"); }
        void pictureBox3_Click(object sender, EventArgs e) { MsgInfo("Jetpack GUI\n" + lc[9] + ": " + Convert.ToString(Application.ProductVersion).Replace(".0", "") + "\n" + lc[10] + ": Zalexanninev15 (programmer and creator) && Vadim M. (consultant)\n" + lc[14], lc[0]); }

        void pictureBox4_Click(object sender, EventArgs e) 
        { 
            try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } 
            catch { MsgError(lc[5], lc[1]); } 
        }

        void button6_Click(object sender, EventArgs e)
        {
            if (DSPanel.Visible == false) 
            { 
                tabFix = false; 
                ModsPanel.Visible = false; 
                DSPanel.Visible = true; 
            }
            else
            {
                if (tabFix == false) 
                { 
                    DSPanel.Visible = false; 
                    ModsPanel.Visible = false; 
                }
                else 
                { 
                    tabFix = false; 
                    ModsPanel.Visible = false; 
                }
            }
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (ModsPanel.Visible == false) 
            { 
                tabFix = true; 
                DSPanel.Visible = true; 
                ModsPanel.Visible = true;
                try
                {
                    using (System.Net.WebClient mods = new System.Net.WebClient())
                    {
                        mods.DownloadFile("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/all/list.txt", cache + @"\list.txt");
                        string[] modsl = File.ReadAllLines(cache + @"\list.txt", System.Text.Encoding.ASCII);
                        darkListView1.Items.Clear();
                        darkComboBox1.Items.Clear();
                        for (int i = 0; i < modsl.Length; i++)
                        {
                            if (modsl[i] != "")
                            {
                                darkListView1.Items.Add(new DarkListItem(modsl[i]));
                                darkComboBox1.Items.Add(modsl[i]);
                                mods.DownloadFile("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/" + modsl[i] + ".txt", cache + @"\" + modsl[i] + ".txt");
                                string[] ms = File.ReadAllLines(cache + "\\" + modsl[i] + ".txt", System.Text.Encoding.ASCII);
                                // Name - 0
                                // Version - 1
                                // Author - 2
                                // Description - 3
                                // Web-site - 4
                                // Link to photo 1 - 5
                                // Link to photo 1 - 6
                                // Link to photo 1 - 7
                                // Link to ZIP with mod - 8
                                mse[i] = ms[0] + "|" + ms[1] + "|" + ms[2] + "|" + ms[3] + "|" + ms[4] + "|" + ms[5] + "|" + ms[6] + "|" + ms[7] + "|" + ms[8];
                            }
                        }
                    }
                }
                catch { }
            }
            else 
            { 
                ModsPanel.Visible = false; 
                DSPanel.Visible = false; 
                tabFix = false; 
            }
        }

        void pictureBox2_Click(object sender, EventArgs e)
        {
            if (lpFix == false)
            {
                LangsPanel.Visible = true;
                lpFix = true;
            }
            else
            {
                LangsPanel.Visible = false;
                lpFix = false;
            }
        }

        void pictureBox9_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LanguageCode = "EN";
            Properties.Settings.Default.Save();
            Translate();
        }

        void Translate()
        {
            try
            {
                IniEditor lang = new IniEditor(@Application.StartupPath + @"\languages\" + Properties.Settings.Default.LanguageCode + ".ini");
                // Text (GUI) loading
                label1.Text = Convert.ToString(lang.GetValue("Interface", "PathLabel"));
                DSPanel.SectionHeader = Convert.ToString(lang.GetValue("Interface", "Tab1"));
                button6.Text = "1. " + DSPanel.SectionHeader;
                darkTitle1.Text = Convert.ToString(lang.GetValue("Interface", "CBG1"));
                darkTitle2.Text = Convert.ToString(lang.GetValue("Interface", "CBG2"));
                button7.Text = Convert.ToString(lang.GetValue("Interface", "ManualEditing"));
                ModsPanel.SectionHeader = Convert.ToString(lang.GetValue("Interface", "Tab2"));
                button2.Text = "2. " + ModsPanel.SectionHeader;
                darkLabel2.Text = Convert.ToString(lang.GetValue("Interface", "List"));
                darkLabel9.Text = Convert.ToString(lang.GetValue("Interface", "FullList"));
                darkGroupBox1.Text = Convert.ToString(lang.GetValue("Interface", "AboutMod"));
                button1.Text = "3. " + Convert.ToString(lang.GetValue("Interface", "Downgrade"));
                HelloUser.Text = Convert.ToString(lang.GetValue("Interface", "Stage"));
                darkLabel1.Text = Convert.ToString(lang.GetValue("Interface", "Languages"));
                // CheckBox loading
                checkBox1.Text = Convert.ToString(lang.GetValue("CheckBox", "Backup"));
                checkBox2.Text = Convert.ToString(lang.GetValue("CheckBox", "Shortcut"));
                checkBox9.Text = Convert.ToString(lang.GetValue("CheckBox", "Reset"));
                checkBox4.Text = Convert.ToString(lang.GetValue("CheckBox", "GarbageCleaning"));
                checkBox6.Text = Convert.ToString(lang.GetValue("CheckBox", "GameReg"));
                checkBox3.Text = Convert.ToString(lang.GetValue("CheckBox", "NoUpdates"));
                checkBox5.Text = Convert.ToString(lang.GetValue("CheckBox", "Forced"));
                checkBox7.Text = Convert.ToString(lang.GetValue("CheckBox", "EnableDirectPlay"));
                checkBox8.Text = Convert.ToString(lang.GetValue("CheckBox", "InstallDirectXComponents"));
                // Title loading
                lc[0] = Convert.ToString(lang.GetValue("Title", "Info"));
                lc[1] = Convert.ToString(lang.GetValue("Title", "Error"));
                lc[8] = Convert.ToString(lang.GetValue("Title", "Warning"));
                lc[6] = Convert.ToString(lang.GetValue("Title", "FolderSelectDialog"));
                // InfoMsg loading
                lc[4] = Convert.ToString(lang.GetValue("InfoMsg", "Succes"));
                lc[9] = Convert.ToString(lang.GetValue("InfoMsg", "Version"));
                lc[10] = Convert.ToString(lang.GetValue("InfoMsg", "Author"));
                lc[14] = Convert.ToString(lang.GetValue("InfoMsg", "LocalizationBy"));
                // ErrorMsg loading
                lc[2] = Convert.ToString(lang.GetValue("ErrorMsg", "ReadINI"));
                lc[3] = Convert.ToString(lang.GetValue("ErrorMsg", "WriteINI"));
                lc[11] = Convert.ToString(lang.GetValue("ErrorMsg", "NoNetwork"));
                // WarningMsg loading
                lc[7] = Convert.ToString(lang.GetValue("WarningMsg", "PathNotFound"));
                lc[5] = Convert.ToString(lang.GetValue("WarningMsg", "BrowserNotFound"));
                lc[15] = Convert.ToString(lang.GetValue("InfoMsg", "AboutModDamaged"));
                // DebugF12 loading
                lc[12] = Convert.ToString(lang.GetValue("DebugF12", "Activation"));
                lc[13] = Convert.ToString(lang.GetValue("DebugF12", "Deactivation"));
            }
            catch { MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
        }

        void pictureBox8_Click(object sender, EventArgs e)
        {
            // Download ZIP with patches
        }

        void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (YesInstallMe.Checked == true)
            {
                getNewFile.Visible = true;
                if (!File.Exists(cache + @"\zips\" + nameLabel.Text.Replace("Name: ", "") + ".zip"))
                {
                    if (!Directory.Exists(cache + @"\zips"))
                    {
                        Directory.CreateDirectory(cache + @"\zips");
                    }
                    try
                    {
                        if (zip_link.Contains("mega.nz"))
                        {
                            var client = new MegaApiClient();
                            client.LoginAnonymous();
                            Uri fileLink = new Uri(zip_link);
                            INodeInfo node = client.GetNodeFromLink(fileLink);
                            labelFile.Text = "Downloading cache file \"" + node.Name + "\"";
                            Downloading.Style = ProgressBarStyle.Marquee;
                            client.DownloadFile(fileLink, cache + "\\" + node.Name);
                            client.Logout();
                        }
                        else
                        {
                            using (System.Net.WebClient wc = new System.Net.WebClient())
                            {
                                Downloading.Style = ProgressBarStyle.Marquee;
                                labelFile.Text = "Downloading cache file for modification \"" + nameLabel.Text.Replace("Name: ", "") + "\"";
                                wc.DownloadFile(zip_link, cache + @"\zips\" + nameLabel.Text.Replace("Name: ", "") + ".zip");
                            }
                        }
                    }
                    catch { MsgError(lc[15], lc[1]); }
                }
                getNewFile.Visible = false;
            }
            else
            {
                if (File.Exists(cache + @"\zips\" + nameLabel.Text.Replace("Name: ", "") + ".zip"))
                {
                    File.Delete(cache + @"\zips\" + nameLabel.Text.Replace("Name: ", "") + ".zip");
                }
            }
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                if (db == false)
                {
                    MsgWarning(lc[12], lc[8]);
                    cfg.SetValue("JPD", "UseProgressBar", "false");
                    db = true;
                }
                else
                {
                    MsgWarning(lc[13], lc[8]);
                    cfg.SetValue("JPD", "UseProgressBar", "true");
                    db = false;
                }
            }
        }

        void darkComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (darkComboBox1.Text != "")
                {
                    ScreenShot.Enabled = true;
                    darkGroupBox1.Visible = true;
                    string[] tInfo = mse[darkComboBox1.SelectedIndex].Split('|');
                    nameLabel.Text = "Name: " + tInfo[0];
                    darkLabel5.Text = "Version: " + tInfo[1];
                    darkLabel6.Text = "Author: " + tInfo[2];
                    darkLabel4.Text = "Description: " + tInfo[3];
                    site_link = tInfo[4];
                    ScreenShot.ImageLocation = tInfo[5];
                    photos_links[0] = ScreenShot.ImageLocation;
                    photos_links[1] = tInfo[6];
                    photos_links[2] = tInfo[7];
                    zip_link = tInfo[8];
                    YesInstallMe.Checked = File.Exists(cache + @"\zips\" + nameLabel.Text.Replace("Name: ", "") + ".zip");
                }
            }
            catch { MsgError(lc[15], lc[1]); ScreenShot.Enabled = false; zip_link = "application/zip"; }
        }

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Directory.Exists(cache))
            {
                Directory.Delete(cache, true);
            }
        }

        void darkButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < photos_links.Length; i++)
            {
                try
                {
                    if (ScreenShot.ImageLocation == photos_links[i])
                    {
                        ScreenShot.ImageLocation = photos_links[i + 1];
                        i = photos_links.Length + 1;
                    }
                }
                catch 
                {
                    ScreenShot.ImageLocation = photos_links[i];
                    i = photos_links.Length + 1;
                }
            }
        }

        void darkButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < photos_links.Length; i++)
            {
                try
                {
                    if (ScreenShot.ImageLocation == photos_links[i])
                    {
                        ScreenShot.ImageLocation = photos_links[i - 1];
                        i = photos_links.Length + 1;
                    }
                }
                catch 
                {
                    ScreenShot.ImageLocation = photos_links[i];
                    i = photos_links.Length + 1;
                }
            }
        }

        void darkButton3_Click(object sender, EventArgs e)
        {
            try { Process.Start(site_link); }
            catch { MsgError(lc[5], lc[1]); }
        }

        void ScreenShot_Click(object sender, EventArgs e)
        {
            try { Process.Start(ScreenShot.ImageLocation); }
            catch { MsgError(lc[5], lc[1]); }
        }
    }
}