using DarkUI.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Net;
using CG.Web.MegaApiClient;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace JetpackGUI
{
    public partial class MainForm : Form
    {
        [DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }
        [DllImport("user32.dll")]
        extern static IntPtr SetFocus(IntPtr hWnd);

        Props config = new Props();
        GUI language = new GUI();
        XmlSerializer lzol = new XmlSerializer(typeof(LanguagesStringReader));
        string[] mse = new string[10];
        string[] langs = new string[10];
        bool tabFix = false;
        bool lpFix = false;
        bool IsBak = false;
        bool sp = true;
        bool NotDone = true;
        bool IsDD = false;
        bool db = false;
        string cache = @Application.StartupPath + @"\files\mods_cache";
        string zip_link = "application/zip";
        string[] photos_links = new string[3];
        string site_link = "";
        string[] fl = new string[17];
        bool x64 = Environment.Is64BitOperatingSystem;
        string langcode = "EN";

        void MainForm_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            if (!Directory.Exists(@Application.StartupPath + @"\files\patches")) { darkButton4.Visible = true; button1.Visible = false; }
            if (!Directory.Exists(cache)) { Directory.CreateDirectory(cache); }
            else { Directory.Delete(cache, true); Directory.CreateDirectory(cache); }
            try
            {
                string[] mf = Directory.GetFiles(@Application.StartupPath + @"\files", "*.zip");
                for (int i = 0; i < mf.Length; i++) { File.Delete(mf[i]); }
            }
            catch { }
            fl[0] = @"\gta-sa.exe"; fl[1] = @"\gta_sa.exe"; fl[2] = @"\audio\CONFIG\TrakLkup.dat"; fl[3] = @"\audio\streams\BEATS";
            fl[4] = @"\audio\streams\CH"; fl[5] = @"\audio\streams\CR"; fl[6] = @"\audio\streams\CUTSCENE"; fl[7] = @"\audio\streams\DS";
            fl[8] = @"\audio\streams\MH"; fl[9] = @"\audio\streams\MR"; fl[10] = @"\audio\streams\RE"; fl[11] = @"\audio\streams\RG";
            fl[12] = @"\anim\anim.img"; fl[13] = @"\data\script\main.scm"; fl[14] = @"\data\script\script.img"; fl[15] = @"\models\gta_int.img";
            fl[16] = @"\models\gta3.img";
            Translate();
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("github.com");
                if ((pingReply.Status == IPStatus.HardwareError) || (pingReply.Status == IPStatus.IcmpError))
                {
                    DialogResult result = DarkMessageBox.ShowWarning(lc_text[27] + " " + lc_text[28], lc_text[11], DarkDialogButton.YesNo);
                    if (result == DialogResult.No) { Application.Exit(); }
                }
            }
            catch
            {
                DialogResult result = DarkMessageBox.ShowWarning(lc_text[27] + " " + lc_text[28], lc_text[11], DarkDialogButton.YesNo);
                if (result == DialogResult.No) { Application.Exit(); }
            }
            SettingsLoader();
        }

        void SettingsLoader()
        {
            config.ReadXml();
            checkBox1.Checked = config.Fields.CreateBackups;
            checkBox2.Checked = config.Fields.CreateShortcut;
            checkBox9.Checked = config.Fields.ResetGame;
            checkBox4.Checked = config.Fields.RGL_GarbageCleaning;
            checkBox6.Checked = config.Fields.RegisterGamePath;
            checkBox3.Checked = config.Fields.CopyGameToNewPath;
            checkBox7.Checked = config.Fields.EnableDirectPlay;
            checkBox8.Checked = config.Fields.InstallDirectXComponents;
            checkBox5.Checked = config.Fields.Forced;
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@Application.StartupPath + @"\files\patches"))
            {
                NotDone = false;
                bool all_patches = true;
                for (int i = 2; i < fl.Length; i++) { if (!File.Exists(@Application.StartupPath + @"\files\patches" + fl[i] + ".jpp")) { all_patches = false; } }
                if (!File.Exists(@Application.StartupPath + @"\files\patches\game.jpp")) { all_patches = false; }
                if (all_patches == true)
                {
                    if (Directory.Exists(@GamePath.Text))
                    {
                        DialogResult result = DarkMessageBox.ShowInformation(lc_text[16], lc_text[9], DarkDialogButton.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Indeterminate);
                            button1.Enabled = false;
                            int d = 0;
                            Process.Start(@Application.StartupPath + @"\files\jpd.exe", "\"" + GamePath.Text + "\"").WaitForExit();
                            string str = "jpd";
                            foreach (Process process2 in Process.GetProcesses()) { if (!process2.ProcessName.ToLower().Contains(str.ToLower())) { d = 1; } }
                            if ((d == 1) && Directory.Exists(cache + @"\zips"))
                            {
                                File.WriteAllBytes(cache + @"\zips\ASI_Loader.zip", Properties.Resources.ASILoader);
                                string[] modsZip = Directory.GetFiles(cache + @"\zips", "*.zip");
                                for (int i = 0; i < modsZip.Length; i++)
                                {
                                    string modName = new FileInfo(modsZip[i]).Name.Replace(".zip", "");
                                    if (IsInstaller(modsZip[i]) == false)
                                    {
                                        try
                                        {
                                            TaskBarProgressBar.SetValue(this.Handle, i, modsZip.Length);
                                            if (checkBox3.Checked == false) { Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + modsZip[i] + "\" -o\"" + GamePath.Text + "\" -y").WaitForExit(); }
                                            else { Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + modsZip[i] + "\" -o\"" + GamePath.Text + "_Downgraded\" -y").WaitForExit(); }
                                            if (modName != "ASI_Loader") { MsgInfo(lc_text[6] + " \"" + modName + "\" " + lc_text[17]); }
                                            if ((modName == "ASI_Loader") && (modsZip.Length == 1)) { MsgInfo(lc_text[6] + " \"" + modName + "\" " + lc_text[17]); }
                                        }
                                        catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgError(lc_text[6] + " \"" + modName + "\" " + lc_text[22]); }
                                    }
                                    else
                                    {
                                        if (checkBox3.Checked == true) { MessageBox.Show(lc_text[29] + ": \"" + GamePath.Text + "_Downgraded\"!\n" + lc_text[30], lc_text[11], MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification); }
                                        File.Move(modsZip[i], modsZip[i].Replace(".zip", ".exe"));
                                        Process.Start(modsZip[i].Replace(".zip", ".exe")).WaitForExit();
                                        File.Move(modsZip[i].Replace(".zip", ".exe"), modsZip[i]);
                                        MsgInfo(lc_text[6] + " \"" + modName + "\"" + lc_text[17]);
                                    }
                                }
                            }
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                            MsgInfo(lc_text[18]);
                            button1.Enabled = true;
                            if (checkBox1.Checked == true) { pictureBox10.Visible = true; IsBak = true; }
                            if (checkBox3.Checked == false) { pictureBox10.Visible = true; IsBak = true; }
                            progressPanel.Visible = true;
                            stagesPanel.Visible = false;
                            labelPartProgress.Visible = false;
                            labelAllProgress.Visible = false;
                            AllProgressBar.Visible = false;
                            PartProgressBar.Visible = false;
                            label3.Visible = true;
                            play.Visible = true;
                            darkButton5.Visible = true;
                        }
                    }
                    else { MsgWarning(lc_text[25]); pictureBox10.Visible = false; IsBak = false; NotDone = true; }
                }
                else { try { Directory.Delete(@Application.StartupPath + @"\files\patches", true); } catch { } button1.Visible = false; darkButton4.Visible = true; NotDone = true; }
            }
            else { button1.Visible = false; darkButton4.Visible = true; NotDone = true; }
        }

        //static void Patcher(string argument)
        //{
        //    Process start_info = new Process();
        //    start_info.StartInfo.FileName = @Path.GetDirectoryName(@Application.StartupPath + @"\files\patcher.exe");
        //    start_info.StartInfo.Arguments = @argument;
        //    start_info.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    start_info.StartInfo.CreateNoWindow = true;
        //    start_info.StartInfo.UseShellExecute = false;
        //    start_info.Start();
        //    start_info.WaitForExit();
        //}

        //static void Create(string ShortcutPath, string TargetPath)
        //{
        //    IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
        //    IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutPath);
        //    Shortcut.TargetPath = TargetPath;
        //    Shortcut.WorkingDirectory = TargetPath.Replace(@"\gta_sa.exe", "");
        //    Shortcut.Save();
        //}

        static string GetMD5(string file)
        {
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(@file))
                    {
                        var hashBytes = md5.ComputeHash(stream);
                        var sb = new StringBuilder();
                        foreach (var t in hashBytes) { sb.Append(t.ToString("X2")); }
                        return Convert.ToString(sb);
                    }
                }
            }
            catch { return "0x50 0x45"; }
        }

        void pictureBox1_Click(object sender, EventArgs e) { SelectPathToGame(); }
        void checkBox2_CheckedChanged(object sender, EventArgs e) { config.Fields.CreateShortcut = checkBox2.Checked; config.WriteXml(); }
        void checkBox1_CheckedChanged(object sender, EventArgs e) { config.Fields.CreateBackups = checkBox1.Checked; config.WriteXml(); }
        void checkBox4_CheckedChanged(object sender, EventArgs e) { config.Fields.RGL_GarbageCleaning = checkBox4.Checked; config.WriteXml(); }
        void checkBox6_CheckedChanged(object sender, EventArgs e) { config.Fields.RegisterGamePath = checkBox6.Checked; config.WriteXml(); }
        public MainForm() { InitializeComponent(); this.KeyPreview = true; }
        void checkBox5_CheckedChanged(object sender, EventArgs e) { config.Fields.Forced = checkBox5.Checked; config.WriteXml(); }
        void checkBox3_CheckedChanged(object sender, EventArgs e) { config.Fields.CopyGameToNewPath = checkBox3.Checked; config.WriteXml(); }
        void checkBox9_CheckedChanged(object sender, EventArgs e) { config.Fields.ResetGame = checkBox9.Checked; config.WriteXml(); }
        void darkButton3_Click(object sender, EventArgs e) { try { Process.Start(site_link); } catch { MsgWarning(lc_text[26]); Clipboard.SetText(site_link); } }
        void darkButton5_Click(object sender, EventArgs e) { Application.Exit(); }
        void ScreenShot_Click(object sender, EventArgs e) { try { Process.Start(ScreenShot.ImageLocation); } catch { MsgWarning(lc_text[26]); Clipboard.SetText(ScreenShot.ImageLocation); } }
        void listBox1_MouseDown(object sender, MouseEventArgs e) { SetFocus(IntPtr.Zero); }
        void button3_Click(object sender, EventArgs e) { try { Process.Start(GamePath.Text + @"\gta_sa.exe"); } catch { MsgWarning(lc_text[25]); pictureBox10.Visible = false; IsBak = false; } }
        void MsgInfo(string message) { DarkMessageBox.ShowInformation(message, lc_text[9]); }
        void MsgError(string message) { DarkMessageBox.ShowError(message, lc_text[10]); }
        void MsgWarning(string message) { DarkMessageBox.ShowWarning(message, lc_text[11]); }
        void MainForm_FormClosed(object sender, FormClosedEventArgs e) { if (Directory.Exists(cache)) { Directory.Delete(cache, true); } }
        void button1_VisibleChanged(object sender, EventArgs e) { IsDD = button1.Visible; }
        void stagesPanel_VisibleChanged(object sender, EventArgs e) { sp = stagesPanel.Visible; }
        //void Logger(string type, string ido, string status) { darkListView2.Items.Add(new DarkListItem("[" + type + "]  " + ido + "=" + status)); }
        void pictureBox3_Click(object sender, EventArgs e) { About about = new About(); about.ShowDialog(); }
        void pictureBox4_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); } catch { MsgWarning(lc_text[26]); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); } }
        void checkBox7_CheckedChanged(object sender, EventArgs e) { config.Fields.EnableDirectPlay = checkBox7.Checked; config.WriteXml(); }

        async void MegaDownloader(string url, string file, string label, int code)
        {
            try { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Normal); } catch { }
            if (File.Exists(file)) { File.Delete(file); }
            var client = new MegaApiClient();
            client.LoginAnonymous();
            Uri zip_link_uri = new Uri(url);
            INodeInfo node = client.GetNodeFromLink(zip_link_uri);
            labelPartProgress.Text = label + " (" + Convert.ToDouble(node.Size / 1048576).ToString("#.# " + lc_text[7] + ")");
            progressPanel.Visible = true;
            stagesPanel.Visible = false;
            PartProgressBar.Value = 0;
            IProgress<double> ph = new Progress<double>(x =>
            {
                PartProgressBar.Value = (int)x;
                try { TaskBarProgressBar.SetValue(this.Handle, (int)x, 100); } catch { }
            });
            await client.DownloadFileAsync(zip_link_uri, file, ph);
            client.Logout();
            if (client.IsLoggedIn == false)
            {
                if (code == 0)
                {
                    try { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Indeterminate); } catch { }
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + file + "\" -o\"" + @Application.StartupPath + "\\files\" -y").WaitForExit();
                    File.Delete(file);
                    darkButton4.Visible = false;
                    button1.Visible = true;
                    try { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); } catch { }
                }
                if (code == 1)
                {
                    try { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Indeterminate); } catch { }
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + file + "\" -o\"" + @Application.StartupPath + "\\files\" -y").WaitForExit();
                    File.Delete(file);
                    try { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); } catch { }
                }
                progressPanel.Visible = false;
                stagesPanel.Visible = true;
            }
        }

        void button6_Click(object sender, EventArgs e)
        {
            if (DSPanel.Visible == false)
            {
                SettingsLoader();
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
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("github.com");
                if (ModsPanel.Visible == false)
                {
                    try
                    {
                        TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Indeterminate);
                        using (System.Net.WebClient mods = new System.Net.WebClient())
                        {
                            mods.DownloadFile("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/info/list.txt", cache + @"\list.txt");
                            string[] modsl = File.ReadAllLines(cache + @"\list.txt", Encoding.ASCII);
                            listBox1.Items.Clear();
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Normal);
                            for (int i = 0; i < modsl.Length; i++)
                            {
                                if (modsl[i] != "")
                                {
                                    listBox1.Items.Add(modsl[i]);
                                    mods.DownloadFile("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/info/txts/" + modsl[i] + ".txt", cache + @"\" + modsl[i] + ".txt");
                                    string[] ms = File.ReadAllLines(cache + "\\" + modsl[i] + ".txt", Encoding.ASCII);
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
                                    TaskBarProgressBar.SetValue(this.Handle, i, modsl.Length);
                                }
                            }
                        }
                        tabFix = true;
                        DSPanel.Visible = true;
                        ModsPanel.Visible = true;
                    }
                    catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgWarning(lc_text[27]); ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
                    TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                }
                else { ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
            }
            catch { MsgWarning(lc_text[27]); ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
        }

        bool IsInstaller(string file)
        {
            var twoBytes = new byte[2];
            using (var fileStream = File.Open(file, FileMode.Open)) { fileStream.Read(twoBytes, 0, 2); }
            return Encoding.UTF8.GetString(twoBytes) == "MZ";
        }

        void pictureBox2_Click(object sender, EventArgs e)
        {
            darkComboBox2.Items.Clear();
            if (lpFix == false)
            {
                LangsPanel.Visible = true;
                lpFix = true;
                langs = Directory.GetFiles(@Application.StartupPath + @"\files\languages", "*.xml");
                for (int i = 0; i < langs.Length; i++)
                {
                    if (langs[i] != "")
                    {
                        TempValues.SelectedLanguage = langs[i];
                        string lg = "English";
                        using (StringReader reader = new StringReader(File.ReadAllText(@Application.StartupPath + @"\files\languages\" + TempValues.SelectedLanguage + ".xml")))
                        {
                            var LOCAL = (LanguagesStringReader)lzol.Deserialize(reader);
                            lg = LOCAL.Language;
                        }
                        darkComboBox2.Items.Add(lg);
                        if (langcode == new FileInfo(langs[i]).Name.Replace(".xml", "")) { darkComboBox2.SelectedItem = lg; }
                    }
                }
            }
            else { LangsPanel.Visible = false; lpFix = false; }
        }

        void SelectPathToGame()
        {
            var dialog = new FolderSelectDialog { InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Title = lc_text[12] };
            if (dialog.Show()) { GamePath.Text = dialog.FileName; } else { GamePath.Clear(); }
        }

        string[] lc_text = new string[40];
        void Translate()
        {
            language.ReadXml();
            langcode = language.Fields.LanguageCode;
            try
            {
                TempValues.SelectedLanguage = langcode;
                using (StringReader reader = new StringReader(File.ReadAllText(@Application.StartupPath + @"\files\languages\" + TempValues.SelectedLanguage + ".xml")))
                {
                    var LOCAL = (LanguagesStringReader)lzol.Deserialize(reader);
                    // Text loading
                    darkLabel1.Text = LOCAL.Languages;
                    HelloUser.Text = LOCAL.Stage;
                    label2.Text = LOCAL.OtherActions;
                    DSPanel.SectionHeader = LOCAL.Tab1;
                    label1.Text = LOCAL.PathLabel + ":";
                    button6.Text = "1. " + DSPanel.SectionHeader;
                    darkTitle1.Text = LOCAL.CBG1;
                    darkTitle2.Text = LOCAL.CBG2;
                    ModsPanel.SectionHeader = LOCAL.Tab2;
                    button2.Text = "2. " + ModsPanel.SectionHeader;
                    darkLabel9.Text = LOCAL.List;
                    darkLabel2.Text = LOCAL.ASILoader;
                    darkGroupBox1.Text = LOCAL.AboutMod;
                    lc_text[0] = LOCAL.ModName;
                    lc_text[1] = LOCAL.ModVersion;
                    lc_text[2] = LOCAL.ModAuthor;
                    darkGroupBox2.Text = LOCAL.DescriptionMod;
                    darkButton3.Text = LOCAL.TopicOfMod;
                    lc_text[3] = LOCAL.DownloadingModCache;
                    button1.Text = "3. " + LOCAL.Downgrade;
                    darkButton4.Text = "3. " + LOCAL.DownloadPatches;
                    lc_text[4] = LOCAL.DownloadingPatches;
                    lc_text[5] = LOCAL.DownloadingDirectXFiles;
                    lc_text[6] = LOCAL.ModWord;
                    lc_text[7] = LOCAL.Mbyte;
                    label3.Text = LOCAL.WishPlay;
                    play.Text = LOCAL.Play;
                    darkButton5.Text = LOCAL.CloseApp;
                    // CheckBoxes loading
                    checkBox1.Text = LOCAL.CreateBackups;
                    checkBox2.Text = LOCAL.CreateShortcut;
                    checkBox9.Text = LOCAL.ResetGame;
                    checkBox4.Text = LOCAL.RGL_GarbageCleaning;
                    checkBox6.Text = LOCAL.RegisterGamePath;
                    checkBox3.Text = LOCAL.CopyGameToNewPath;
                    checkBox7.Text = LOCAL.EnableDirectPlay;
                    checkBox8.Text = LOCAL.InstallDirectXComponents;
                    checkBox5.Text = LOCAL.Forced;
                    YesInstallMe.Text = LOCAL.InstallMod;
                    // Titles loading
                    lc_text[8] = LOCAL.Request;
                    lc_text[9] = LOCAL.Information;
                    lc_text[10] = LOCAL.Error;
                    lc_text[11] = LOCAL.Warning;
                    lc_text[12] = LOCAL.FolderSelectDialog;
                    // Information messages loading
                    lc_text[13] = LOCAL.WishDownloadPatches;
                    lc_text[14] = LOCAL.WishDownloadDirectXFiles;
                    lc_text[15] = LOCAL.InstallModQuestion;
                    lc_text[16] = LOCAL.WishDowngrader;
                    lc_text[17] = LOCAL.ModSucces;
                    lc_text[18] = LOCAL.Succes;
                    lc_text[19] = LOCAL.BindingOK;
                    lc_text[20] = LOCAL.ReturnUsingBackups;
                    // Error messages loading
                    lc_text[21] = LOCAL.AboutModDamaged;
                    lc_text[22] = LOCAL.ModFailure;
                    // Warning messages loading
                    lc_text[23] = LOCAL.WishReturnUsingBackups;
                    lc_text[24] = LOCAL.WishRegGame;
                    lc_text[25] = LOCAL.PathNotFound;
                    lc_text[26] = LOCAL.BrowserNotFound;
                    lc_text[27] = LOCAL.NetworkNotFound;
                    lc_text[28] = LOCAL.OfflineMode;
                    lc_text[29] = LOCAL.NewPath;
                    lc_text[30] = LOCAL.YouCanDelete;
                    // Debug mode loading
                    lc_text[31] = LOCAL.Activation;
                    lc_text[32] = LOCAL.Deactivation;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
        }

        void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("github.com");
                if (YesInstallMe.Checked == true)
                {
                    if (!File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip"))
                    {
                        if (!Directory.Exists(cache + @"\zips")) { Directory.CreateDirectory(cache + @"\zips"); }
                        try
                        {
                            DialogResult result = DarkMessageBox.ShowInformation(lc_text[15], lc_text[8], DarkDialogButton.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (zip_link.Contains("mega.nz")) { MegaDownloader(zip_link, cache + @"\zips\" + @nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip", lc_text[3] + " \"" + @nameLabel.Text.Replace(lc_text[0] + ": ", "") + "\"...", 2); }
                                else
                                {
                                    TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Normal);
                                    progressPanel.Visible = true;
                                    stagesPanel.Visible = false;
                                    labelPartProgress.Text = lc_text[3] + " \"" + @nameLabel.Text.Replace(lc_text[0] + ": ", "") + "\"...";
                                    PartProgressBar.Value = 0;
                                    using (System.Net.WebClient wc = new System.Net.WebClient())
                                    {
                                        var r = wc.OpenRead(zip_link);
                                        labelPartProgress.Text += " (" + (Convert.ToDouble(wc.ResponseHeaders["Content-Length"]) / 1048576).ToString("#.# " + lc_text[7] + ")");
                                        r.Close();
                                        wc.DownloadProgressChanged += (s, a) => { TaskBarProgressBar.SetValue(this.Handle, a.ProgressPercentage, 100); PartProgressBar.Value = a.ProgressPercentage; };
                                        wc.DownloadFileCompleted += (s, a) =>
                                        {
                                            PartProgressBar.Value = 0;
                                            progressPanel.Visible = false;
                                            stagesPanel.Visible = true;
                                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                                        };
                                        wc.DownloadFileAsync(new Uri(zip_link), @cache + @"\zips\" + @nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip");
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgError(lc_text[21]); MsgError(ex.ToString() + "\nFile: " + cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip"); TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); }
                    }
                    else { YesInstallMe.Checked = true; }
                }
                if (YesInstallMe.Checked == false)
                {
                    progressPanel.Visible = false;
                    stagesPanel.Visible = true;
                    if (File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip")) { File.Delete(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip"); }
                }
            }
            catch { MsgWarning(lc_text[27]); YesInstallMe.Checked = false; }
        }

        void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                if (!Directory.Exists(@Application.StartupPath + @"\files\DirectX"))
                {
                    try
                    {
                        Ping ping = new Ping();
                        PingReply pingReply = null;
                        pingReply = ping.Send("github.com");
                        DialogResult result = DarkMessageBox.ShowInformation(lc_text[14], lc_text[8], DarkDialogButton.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            try { MegaDownloader("https://mega.nz/file/0pFRwAqa#Arguk9cQLpXYeQgXnFfAp6cw6F5OIZFKP2tRTwNCArI", @Application.StartupPath + @"\files\ddirectx.7z", lc_text[5], 1); }
                            catch { checkBox8.Checked = false; }
                        }
                        else { checkBox8.Checked = false; }
                    }
                    catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgWarning(lc_text[27]); checkBox8.Checked = false; TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); }
                }
            }
            config.Fields.InstallDirectXComponents = checkBox8.Checked;
            config.WriteXml();
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); } catch { MsgWarning(lc_text[26]); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); } }
            if (e.KeyData == Keys.F4) { Process.Start("notepad.exe", @Application.StartupPath + @"\files\downgrader.xml"); }
            if (e.KeyData == Keys.F12)
            {
                if (db == false) { MsgWarning(lc_text[31]); config.Fields.UserMode = false; config.WriteXml(); db = true; }
                else { MsgWarning(lc_text[32]); config.Fields.UserMode = true; config.WriteXml(); db = false; }
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.O))
            {
                if (NotDone == true)
                {
                    tabFix = false;
                    ModsPanel.Visible = false;
                    DSPanel.Visible = true;
                    SelectPathToGame();
                }
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y)) { if (GamePath.Text != "") { pictureBox11_Click(sender, e); } }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Z)) { if (IsBak == true) { pictureBox10_Click(sender, e); } }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S)) { if ((IsDD == false) && (sp == true)) { button1_Click(sender, e); } }
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
                    TempValues.SelectedLanguage = langs[i];
                    using (StringReader reader = new StringReader(File.ReadAllText(@Application.StartupPath + @"\files\languages\" + TempValues.SelectedLanguage + ".xml")))
                    {
                        var LOCAL = (LanguagesStringReader)lzol.Deserialize(reader);
                        if (darkComboBox2.Text == LOCAL.Language) 
                        { 
                            language.Fields.LanguageCode = new FileInfo(langs[i]).Name.Replace(".xml", ""); 
                            language.WriteXml(); 
                            Translate(); 
                        }
                    }
                }
            }
        }

        void darkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("github.com");
                DialogResult result = DarkMessageBox.ShowInformation(lc_text[13], lc_text[8], DarkDialogButton.YesNo);
                if (result == DialogResult.Yes)
                {
                    try { MegaDownloader("https://mega.nz/file/4tcXiSqA#8JzulAC0oABzinb7914sq2xkyxE7c6atSeMval-YWms", @Application.StartupPath + @"\files\dpatches.rar", lc_text[4], 0); }
                    catch { progressPanel.Visible = false; stagesPanel.Visible = true; button1.Visible = false; }
                }
                else { progressPanel.Visible = false; stagesPanel.Visible = true; button1.Visible = false; }
            }
            catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgWarning(lc_text[27]); TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); }
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Text != "")
                {
                    ScreenShot.Enabled = true;
                    darkGroupBox1.Visible = true;
                    string[] tInfo = mse[listBox1.SelectedIndex].Split('|');
                    nameLabel.Text = lc_text[0] + ": " + tInfo[0];
                    darkLabel5.Text = lc_text[1] + ": " + tInfo[1];
                    darkLabel6.Text = lc_text[2] + ": " + tInfo[2];
                    darkLabel4.Text = tInfo[3];
                    site_link = tInfo[4];
                    ScreenShot.ImageLocation = tInfo[5];
                    photos_links[0] = ScreenShot.ImageLocation;
                    photos_links[1] = tInfo[6];
                    photos_links[2] = tInfo[7];
                    zip_link = tInfo[8];
                    YesInstallMe.Checked = File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip");
                }
            }
            catch (Exception ex) { MsgError(lc_text[21]); MsgError(ex.ToString()); ScreenShot.Enabled = false; zip_link = "application/zip"; }
        }

        void GamePath_TextChanged(object sender, EventArgs e)
        {
            if (GamePath.Text != "")
            {
                pictureBox11.Visible = true;
                for (int i = 0; i < fl.Length; i++) { if (File.Exists(GamePath.Text + fl[i] + ".jpb")) { pictureBox10.Visible = true; IsBak = true; } }
                if (((GetMD5(@GamePath.Text + @"\gta_sa.exe") == "6687A315558935B3FC80CDBFF04437A4") || (GetMD5(@GamePath.Text + @"\gta-sa.exe") == "6687A315558935B3FC80CDBFF04437A4")) && ((!File.Exists(@GamePath.Text + @"\MTLX.dll")) || (!File.Exists(@GamePath.Text + @"\index.bin")))) { pictureBox10.Visible = true; IsBak = true; }
            }
            else { pictureBox11.Visible = false; pictureBox10.Visible = false; IsBak = false; }
        }

        void pictureBox10_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.ShowWarning(lc_text[23], lc_text[11], DarkDialogButton.YesNo);
            if (result == DialogResult.Yes)
            {
                checkBox1.Checked = false;
                originalGameRestoreProgressBar.Visible = true;
                string exe1 = GetMD5(@GamePath.Text + @"\gta_sa.exe.jpb");
                string exe2 = GetMD5(@GamePath.Text + @"\gta_sa.exe");
                string exe3 = GetMD5(@GamePath.Text + @"\gta-sa.exe");
                string exe4 = GetMD5(@GamePath.Text + @"\gta-sa.exe.jpb");
                if ((exe1 != "0x50 0x45" && exe2 != "0x50 0x45") || (exe3 != "0x50 0x45" && exe4 != "0x50 0x45"))
                {
                    if ((exe1 != exe2) || (exe3 != exe4))
                    {
                        System.Threading.Tasks.Task.Run(() =>
                        {
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Normal);
                            originalGameRestoreProgressBar.Maximum = fl.Length;
                            for (int i = 0; i < fl.Length; i++)
                            {
                                if (File.Exists(@GamePath.Text + fl[i] + ".jpb"))
                                {
                                    try { File.SetAttributes(@GamePath.Text + fl[i], FileAttributes.Normal); File.Delete(@GamePath.Text + fl[i]); }
                                    catch { }
                                    try
                                    {
                                        File.SetAttributes(@GamePath.Text + fl[i] + ".jpb", FileAttributes.Normal);
                                        File.Copy(@GamePath.Text + fl[i] + ".jpb", @GamePath.Text + fl[i]);
                                        File.Delete(@GamePath.Text + fl[i] + ".jpb");
                                    }
                                    catch { }
                                }
                                if (fl[i] == @"\gta_sa.exe")
                                {
                                    if (File.Exists(@GamePath.Text + fl[0]))
                                    {
                                        try { File.SetAttributes(@GamePath.Text + fl[i], FileAttributes.Normal); File.Delete(@GamePath.Text + fl[i]); } catch { }
                                        try { File.Copy(@GamePath.Text + fl[0], @GamePath.Text + fl[1]); } catch { }
                                    }
                                }
                                originalGameRestoreProgressBar.Value = i + 1;
                                TaskBarProgressBar.SetValue(this.Handle, i + 1, fl.Length);
                            }
                            if (((GetMD5(@GamePath.Text + @"\gta_sa.exe") == "6687A315558935B3FC80CDBFF04437A4") || (GetMD5(@GamePath.Text + @"\gta-sa.exe") == "6687A315558935B3FC80CDBFF04437A4")) && ((!File.Exists(@GamePath.Text + @"\MTLX.dll")) || (!File.Exists(@GamePath.Text + @"\index.bin"))))
                            {
                                TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Indeterminate);
                                var restoreRGLfiles = new ProcessStartInfo
                                {
                                    FileName = @Application.StartupPath + @"\files\7z.exe",
                                    Arguments = "x \"" + @Application.StartupPath + "\\files\\rgl.jpbc\" -o\"" + @GamePath.Text + "\" -y",
                                    UseShellExecute = false,
                                    CreateNoWindow = true,
                                };
                                restoreRGLfiles.WindowStyle = ProcessWindowStyle.Hidden;
                                Process.Start(restoreRGLfiles).WaitForExit();
                            }
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                            MsgInfo(lc_text[20]);
                            pictureBox10.Visible = false;
                            IsBak = false;
                            originalGameRestoreProgressBar.Visible = false;
                        });
                    }
                    else
                    {
                        try { File.SetAttributes(@GamePath.Text + @"\gta_sa.exe.jpb", FileAttributes.Normal); File.Delete(@GamePath.Text + @"\gta_sa.exe.jpb"); } catch { }
                        try { File.SetAttributes(@GamePath.Text + @"\gta-sa.exe.jpb", FileAttributes.Normal); File.Delete(@GamePath.Text + @"\gta-sa.exe.jpb"); } catch { }
                        MsgInfo(lc_text[20]);
                        pictureBox10.Visible = false;
                        IsBak = false;
                        originalGameRestoreProgressBar.Visible = false;
                    }
                }
                else
                {
                    pictureBox10.Visible = false;
                    IsBak = false;
                    originalGameRestoreProgressBar.Visible = false;
                }
            }
        }

        void pictureBox11_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.ShowWarning(lc_text[24], lc_text[11], DarkDialogButton.YesNo);
            if (result == DialogResult.Yes)
            {
                if (x64 == true)
                {
                    Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\GTA San Andreas\Installation");
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Rockstar Games\GTA San Andreas\Installation", "ExePath", "\"" + GamePath.Text + "\"");
                }
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rockstar Games\GTA San Andreas\Installation");
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\GTA San Andreas\Installation", "ExePath", "\"" + GamePath.Text + "\"");
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\GTA San Andreas\Installation", "Installed", "1");
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rockstar Games\Launcher");
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\Launcher", "Language", "en-US");
                MsgInfo(lc_text[19]);
            }
        }

        void progressPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (progressPanel.Visible == true) { LangsPanel.Visible = false; HelloUser.Visible = false; pictureBox5.Visible = false; pictureBox6.Visible = false; pictureBox7.Visible = false; }
            else { HelloUser.Visible = true; pictureBox5.Visible = true; pictureBox6.Visible = true; pictureBox7.Visible = true; }
        }
    }
}
