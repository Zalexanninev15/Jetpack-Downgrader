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
        string[] lc = new string[30];
        string[] mse = new string[10];
        string[] langs = new string[10];
        bool tabFix = false;
        bool lpFix = false;
        bool db = false;
        string cache = @Application.StartupPath + @"\files\mods_cache";
        string zip_link = "application/zip";
        string[] photos_links = new string[3];
        string site_link = "";

        public MainForm() { InitializeComponent(); this.KeyPreview = true; }

        void MainForm_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!Directory.Exists(@Application.StartupPath + @"\files\patches")) { darkButton4.Visible = true; button1.Visible = false; }
            if (!Directory.Exists(cache)) { Directory.CreateDirectory(cache); }
            else { Directory.Delete(cache, true); Directory.CreateDirectory(cache); }
            try
            {
                string[] zf = Directory.GetFiles(@Application.StartupPath + @"\files", "*.zip");
                for (int i = 0; i < zf.Length; i++) { File.Delete(zf[i]); }
            }
            catch { }
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
            catch { MsgError(lc[2]); }
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
                    string[] modsZip = Directory.GetFiles(cache + @"\zips", "*.zip");
                    for (int i = 0; i < modsZip.Length; i++)
                    {
                        string modName = new FileInfo(modsZip[i]).Name.Replace(".zip", "");
                        try
                        {
                            Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + modsZip[i] + "\" -o\"" + GamePath.Text + "\" -y").WaitForExit();
                            MsgInfo(lc[23] + " \"" + modName + "\" " + lc[24]);
                        }
                        catch { MsgError(lc[23] + " \"" + modName + "\" " + lc[25]); }
                    }
                    MsgInfo(lc[4]);
                    this.Enabled = true;
                }
            }
            else { MsgWarning(lc[7]); }
        }

        void pictureBox1_Click(object sender, EventArgs e) { SelectPathToGame(); }
        void checkBox2_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateShortcut", Convert.ToString(checkBox2.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox1_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateBackups", Convert.ToString(checkBox1.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox4_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "GarbageCleaning", Convert.ToString(checkBox4.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox6_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "RegisterGamePath", Convert.ToString(checkBox6.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox5_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "Forced", Convert.ToString(checkBox5.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox3_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateNewGamePath", Convert.ToString(checkBox3.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox9_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "ResetGame", Convert.ToString(checkBox9.Checked).Replace("T", "t").Replace("F", "f")); }
        void darkButton3_Click(object sender, EventArgs e) { try { Process.Start(site_link); } catch { MsgError(lc[5]); } }
        void ScreenShot_Click(object sender, EventArgs e) { try { Process.Start(ScreenShot.ImageLocation); } catch { MsgError(lc[5]); } }
        void MsgInfo(string message) { DarkMessageBox.ShowInformation(message, lc[0]); }
        void MainForm_FormClosed(object sender, FormClosedEventArgs e) { if (Directory.Exists(cache)) { Directory.Delete(cache, true); } }
        void MsgError(string message) { DarkMessageBox.ShowError(message, lc[1]); }
        void MsgWarning(string message) { DarkMessageBox.ShowWarning(message, lc[8]); }
        void button7_Click(object sender, EventArgs e) { Process.Start("notepad.exe", @Application.StartupPath + @"\files\jpd.ini"); }
        void pictureBox3_Click(object sender, EventArgs e) { MsgInfo("Jetpack GUI\n" + lc[9] + ": " + Convert.ToString(Application.ProductVersion).Replace(".0", "") + "\n" + lc[10] + ": Zalexanninev15 (programmer and creator) && Vadim M. (consultant)\n" + lc[14]); }
        void pictureBox4_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } catch { MsgError(lc[5]); } }
        void checkBox7_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "EnableDirectPlay", Convert.ToString(checkBox7.Checked).Replace("T", "t").Replace("F", "f")); }

        async void MegaDownloader(string url, string file, string label, int code)
        {
            getNewFile.Visible = true;
            panel1.Visible = false;
            AllProgressBar.Value = 0;
            if (File.Exists(file)) { File.Delete(file); }
            var client = new MegaApiClient();
            client.LoginAnonymous();
            Uri zip_link_uri = new Uri(url);
            INodeInfo node = client.GetNodeFromLink(zip_link_uri);
            IProgress<double> ph = new Progress<double>(x => AllProgressBar.Value = (int)x);
            labelFile.Text = label;
            await client.DownloadFileAsync(zip_link_uri, file, ph);
            client.Logout();
            if (client.IsLoggedIn == false)
            {
                if (code == 0)
                {
                    Directory.CreateDirectory(@Application.StartupPath + @"\files\patches");
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + file + "\" -o\"" + Application.StartupPath + "\\files\" -y").WaitForExit();
                    File.Delete(file);
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + Application.StartupPath + "\\files\\patches\\game.jppe\" -o\"" + Application.StartupPath + "\\files\\patches\" -y").WaitForExit();
                    File.Delete(@Application.StartupPath + @"\files\patches\game.jppe");
                    getNewFile.Visible = false;
                    panel1.Visible = true;
                    darkButton4.Visible = false;
                    button1.Visible = true;
                }
                if (code == 1)
                {
                    Directory.CreateDirectory(@Application.StartupPath + @"\files\DirectX");
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + file + "\" -o\"" + Application.StartupPath + "\\files\" -y").WaitForExit();
                    File.Delete(file);
                    getNewFile.Visible = false;
                    panel1.Visible = true;
                }
                if (code == 2)
                {
                    getNewFile.Visible = false;
                    panel1.Visible = true;
                }
            }
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
                if (tabFix == false) { DSPanel.Visible = false; ModsPanel.Visible = false; }
                else { tabFix = false; ModsPanel.Visible = false; }
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
            else { ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
        }

        void pictureBox2_Click(object sender, EventArgs e)
        {
            darkComboBox2.Items.Clear();
            if (lpFix == false)
            {
                LangsPanel.Visible = true;
                lpFix = true;
                langs = Directory.GetFiles(@Application.StartupPath + @"\files\languages", "*.ini");
                for (int i = 0; i < langs.Length; i++)
                {
                    if (langs[i] != "")
                    {
                        IniEditor lang = new IniEditor(langs[i]);
                        string lg = Convert.ToString(lang.GetValue("Interface", "Language"));
                        darkComboBox2.Items.Add(lg);
                        if (Properties.Settings.Default.LanguageCode == new FileInfo(langs[i]).Name.Replace(".ini", "")) { darkComboBox2.SelectedItem = lg; }
                    }
                }
            }
            else { LangsPanel.Visible = false; lpFix = false; }
        }

        void SelectPathToGame()
        {
            var dialog = new FolderSelectDialog { InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Title = lc[6] };
            if (dialog.Show()) { GamePath.Text = dialog.FileName; } else { GamePath.Clear(); }
        }

        void Translate()
        {
            try
            {
                IniEditor lang = new IniEditor(@Application.StartupPath + @"\files\languages\" + Properties.Settings.Default.LanguageCode + ".ini");
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
                lc[16] = Convert.ToString(lang.GetValue("Interface", "ModName"));
                lc[18] = Convert.ToString(lang.GetValue("Interface", "ModVersion"));
                lc[19] = Convert.ToString(lang.GetValue("Interface", "ModAuthor"));
                darkGroupBox2.Text = Convert.ToString(lang.GetValue("Interface", "DescriptionMod"));
                darkButton3.Text = Convert.ToString(lang.GetValue("Interface", "WebSiteOfMod"));
                lc[17] = Convert.ToString(lang.GetValue("Interface", "DownloadingModCache"));
                button1.Text = "3. " + Convert.ToString(lang.GetValue("Interface", "Downgrade"));
                darkButton4.Text = "3. " + Convert.ToString(lang.GetValue("Interface", "DownloadPatches"));
                lc[21] = Convert.ToString(lang.GetValue("Interface", "DownloadingPatches"));
                lc[22] = Convert.ToString(lang.GetValue("Interface", "DownloadingDirectXFiles"));
                lc[23] = Convert.ToString(lang.GetValue("Interface", "ModWord"));
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
                YesInstallMe.Text = Convert.ToString(lang.GetValue("CheckBox", "InstallMod"));
                // Title loading
                lc[0] = Convert.ToString(lang.GetValue("Title", "Info"));
                lc[1] = Convert.ToString(lang.GetValue("Title", "Error"));
                lc[8] = Convert.ToString(lang.GetValue("Title", "Warning"));
                lc[6] = Convert.ToString(lang.GetValue("Title", "FolderSelectDialog"));
                // InfoMsg loading
                lc[20] = Convert.ToString(lang.GetValue("InfoMsg", "WishDownloadPatches"));
                lc[22] = Convert.ToString(lang.GetValue("InfoMsg", "WishDownloadDirectXFiles"));
                lc[24] = Convert.ToString(lang.GetValue("InfoMsg", "ModSucces"));
                lc[4] = Convert.ToString(lang.GetValue("InfoMsg", "Succes"));
                lc[9] = Convert.ToString(lang.GetValue("InfoMsg", "Version"));
                lc[10] = Convert.ToString(lang.GetValue("InfoMsg", "Author"));
                lc[14] = Convert.ToString(lang.GetValue("InfoMsg", "LocalizationBy"));
                // ErrorMsg loading
                lc[2] = Convert.ToString(lang.GetValue("ErrorMsg", "ReadINI"));
                lc[3] = Convert.ToString(lang.GetValue("ErrorMsg", "WriteINI"));
                lc[11] = Convert.ToString(lang.GetValue("ErrorMsg", "NoNetwork"));
                lc[15] = Convert.ToString(lang.GetValue("ErrorMsg", "AboutModDamaged"));
                lc[25] = Convert.ToString(lang.GetValue("ErrorMsg", "ModFailure"));
                // WarningMsg loading
                lc[7] = Convert.ToString(lang.GetValue("WarningMsg", "PathNotFound"));
                lc[5] = Convert.ToString(lang.GetValue("WarningMsg", "BrowserNotFound"));
                // DebugF12 loading
                lc[12] = Convert.ToString(lang.GetValue("DebugF12", "Activation"));
                lc[13] = Convert.ToString(lang.GetValue("DebugF12", "Deactivation"));
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
        }

        void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (YesInstallMe.Checked == true)
            {
                if (!File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip"))
                {
                    if (!Directory.Exists(cache + @"\zips")) { Directory.CreateDirectory(cache + @"\zips"); }
                    try
                    {
                        if (zip_link.Contains("mega.nz")) { MegaDownloader(zip_link, cache + @"\zips\" + @nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip", lc[17] + " \"" + @nameLabel.Text.Replace(lc[16] + ": ", "") + "\"...", 2); }
                        else
                        {
                            getNewFile.Visible = true;
                            panel1.Visible = false;
                            labelFile.Text = lc[17] + " \"" + @nameLabel.Text.Replace(lc[16] + ": ", "") + "\"...";
                            AllProgressBar.Value = 0;
                            using (System.Net.WebClient wc = new System.Net.WebClient())
                            {
                                wc.DownloadProgressChanged += (s, a) => { AllProgressBar.Value = a.ProgressPercentage; };
                                wc.DownloadFileCompleted += (s, a) =>
                                {
                                    AllProgressBar.Value = 0;
                                    getNewFile.Visible = false;
                                    panel1.Visible = true;
                                };
                                wc.DownloadFileAsync(new Uri(zip_link), @cache + @"\zips\" + @nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip");
                            }
                        }
                    }
                    catch (Exception ex) { MsgError(lc[15]); MessageBox.Show(ex.ToString() + "\n" + cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip"); }
                }
            }
            else
            {
                getNewFile.Visible = false;
                panel1.Visible = true;
                if (File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip")) { File.Delete(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip"); }
            }
        }

        void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                if (!Directory.Exists(@Application.StartupPath + @"\files\DirectX"))
                {
                    MsgInfo(lc[22]);
                    try { MegaDownloader("https://mega.nz/file/hklF0S4I#XCpKtk192-Y6wAE7Gd6EKkIdawEPxHptUVrseNYp0zA", @Application.StartupPath + @"\files\DirectX_Installer.zip", lc[22], 1); }
                    catch { checkBox8.Checked = false; }
                }
            }
            cfg.SetValue("Downgrader", "InstallDirectXComponents", Convert.ToString(checkBox8.Checked).Replace("T", "t").Replace("F", "f"));
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F12)
            {
                if (db == false)
                {
                    MsgWarning(lc[12]);
                    cfg.SetValue("JPD", "UseProgressBar", "false");
                    db = true;
                }
                else
                {
                    MsgWarning(lc[13]);
                    cfg.SetValue("JPD", "UseProgressBar", "true");
                    db = false;
                }
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.O))
            {
                tabFix = false;
                ModsPanel.Visible = false;
                DSPanel.Visible = true;
                SelectPathToGame();
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
                    nameLabel.Text = lc[16] + ": " + tInfo[0];
                    darkLabel5.Text = lc[18] + ": " + tInfo[1];
                    darkLabel6.Text = lc[19] + ": " + tInfo[2];
                    darkLabel4.Text = tInfo[3];
                    site_link = tInfo[4];
                    ScreenShot.ImageLocation = tInfo[5];
                    photos_links[0] = ScreenShot.ImageLocation;
                    photos_links[1] = tInfo[6];
                    photos_links[2] = tInfo[7];
                    zip_link = tInfo[8];
                    YesInstallMe.Checked = File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip");
                }
            }
            catch (Exception ex) { MsgError(lc[15]); MessageBox.Show(ex.ToString()); ScreenShot.Enabled = false; zip_link = "application/zip"; }
        }

        void darkButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < photos_links.Length; i++)
            {
                try { if (ScreenShot.ImageLocation == photos_links[i]) { ScreenShot.ImageLocation = photos_links[i + 1]; i = photos_links.Length + 1; } }
                catch { ScreenShot.ImageLocation = photos_links[i]; i = photos_links.Length + 1; }
            }
        }

        void darkButton2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < photos_links.Length; i++)
            {
                try { if (ScreenShot.ImageLocation == photos_links[i]) { ScreenShot.ImageLocation = photos_links[i - 1]; i = photos_links.Length + 1; } }
                catch { ScreenShot.ImageLocation = photos_links[i]; i = photos_links.Length + 1; }
            }
        }

        void darkComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    IniEditor lang = new IniEditor(langs[i]);
                    if (darkComboBox2.Text == Convert.ToString(lang.GetValue("Interface", "Language")))
                    {
                        Properties.Settings.Default.LanguageCode = new FileInfo(langs[i]).Name.Replace(".ini", ""); ;
                        Properties.Settings.Default.Save();
                        Translate();
                    }
                }
            }
        }

        void darkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                MsgInfo(lc[20]);
                MegaDownloader("https://mega.nz/file/880jHaCB#0775P1K90tfH-s2S6vJNfkR2f0sBpVLGgivjyIhWhPQ", @Application.StartupPath + @"\files\dpatches.zip", lc[21], 0);
            }
            catch { getNewFile.Visible = false; panel1.Visible = true; button1.Visible = false; }
        }
    }
}