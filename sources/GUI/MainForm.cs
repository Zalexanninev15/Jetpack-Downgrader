using CG.Web.MegaApiClient;

using Microsoft.Win32;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

using VitNX.Forms;
using VitNX.Functions;
using VitNX.Win32;

namespace JetpackGUI
{
    public partial class MainForm : Form
    {
        [DllImport("shcore.dll", SetLastError = true)]
        public static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS PROCESS_DPI_UNAWARE);

        public enum PROCESS_DPI_AWARENESS : int
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_Aware = 2
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (NativeFunctions.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                NativeFunctions.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private Props config = new Props();
        private GUI language = new GUI();
        private XmlSerializer lzol = new XmlSerializer(typeof(LanguagesString));
        private string[] langs = new string[10];
        private bool tabFix = false;
        private bool lpFix = false;
        private bool IsBak = false;
        private bool sp = true;
        private bool IsOnePointNull = false;
        private bool NotDone = true;
        private bool IsDD = false;
        private bool db = false;
        private string cache = @Application.StartupPath + @"\files\mods_cache";
        private string zip_link = "application/zip";
        private string[] photos_links = new string[3];
        private string site_link = "";
        private string[] fl = new string[17];
        private bool x64 = Environment.Is64BitOperatingSystem;
        private string langcode = "EN";

        private void MainForm_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 |
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls;
            SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE);
            if (!Directory.Exists(@Application.StartupPath + @"\files\patches"))
            {
                darkButton4.Visible = true;
                button1.Visible = false;
            }
            if (!Directory.Exists(cache))
                Directory.CreateDirectory(cache);
            else
            {
                Directory.Delete(cache, true);
                Directory.CreateDirectory(cache);
            }
            try
            {
                string[] mf = Directory.GetFiles(@Application.StartupPath + @"\files", "*.zip");
                for (int i = 0; i < mf.Length; i++)
                    File.Delete(mf[i]);
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
                    DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[27] + " " + lc_text[28], lc_text[33]);
                    if (result == DialogResult.No)
                        Application.Exit();
                }
            }
            catch
            {
                DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[27] + " " + lc_text[28], lc_text[33]);
                if (result == DialogResult.No)
                    Application.Exit();
            }
            SettingsLoader();
            try
            {
                if (Windows.GetCurrentVersionFromREG() < 6.3)
                {
                    checkBox7.Visible = false;
                    checkBox7.Checked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                checkBox7.Visible = false;
                checkBox7.Checked = false;
            }
        }

        private void SettingsLoader()
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(@Application.StartupPath + @"\files\patches"))
            {
                NotDone = false;
                bool all_patches = true;
                for (int i = 2; i < fl.Length; i++)
                {
                    if (!File.Exists(@Application.StartupPath + @"\files\patches" + fl[i] + ".jpp"))
                        all_patches = false;
                }
                if (!File.Exists(@Application.StartupPath + @"\files\patches\game.jpp"))
                    all_patches = false;
                if (all_patches == true)
                {
                    if (Directory.Exists(@GamePath.Text) && (File.Exists(@GamePath.Text + @"\gta_sa.exe") || File.Exists(@GamePath.Text + @"\gta-sa.exe")))
                    {
                        DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[16], lc_text[33]);
                        if (result == DialogResult.Yes)
                        {
                            TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Indeterminate);
                            button1.Enabled = false;
                            int d = 0;
                            Process.Start(@Application.StartupPath + @"\files\jpd.exe", "\"" + GamePath.Text + "\"").WaitForExit();
                            string str = "jpd";
                            foreach (Process process2 in Process.GetProcesses())
                            {
                                if (!process2.ProcessName.ToLower().Contains(str.ToLower()))
                                    d = 1;
                            }
                            if ((d == 1) && Directory.Exists(cache + @"\zips"))
                            {
                                File.WriteAllBytes(cache + @"\zips\ASI_Loader.zip", Properties.Resources.ASILoader);
                                string[] modsZip = Directory.GetFiles(cache + @"\zips", "*.zip");
                                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Normal);
                                for (int i = 0; i < modsZip.Length; i++)
                                {
                                    string modName = new FileInfo(modsZip[i]).Name.Replace(".zip", "");
                                    if (IsInstaller(modsZip[i]) == false)
                                    {
                                        try
                                        {
                                            TbProgressBar.SetValue(Handle, i, modsZip.Length);
                                            if (checkBox3.Checked == false)
                                                Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + modsZip[i] + "\" -o\"" + GamePath.Text + "\" -y").WaitForExit();
                                            else
                                                Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + modsZip[i] + "\" -o\"" + GamePath.Text + "_Downgraded\" -y").WaitForExit();
                                            if (modName != "ASI_Loader")
                                                MsgInfo(lc_text[6] + " \"" + modName + "\" " + lc_text[17]);
                                            if ((modName == "ASI_Loader") && (modsZip.Length == 1))
                                                MsgInfo(lc_text[6] + " \"" + modName + "\" " + lc_text[17]);
                                        }
                                        catch
                                        {
                                            TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Error);
                                            MsgError(lc_text[6] + " \"" + modName + "\" " + lc_text[22]);
                                        }
                                    }
                                    else
                                    {
                                        if (checkBox3.Checked == true)
                                        {
                                            VitNX_MessageBox.ShowWarning(lc_text[29] + ": \"" + GamePath.Text + "_Downgraded\"!\n" + lc_text[30], lc_text[11]);
                                        }
                                        File.Move(modsZip[i], modsZip[i].Replace(".zip", ".exe"));
                                        Process.Start(modsZip[i].Replace(".zip", ".exe")).WaitForExit();
                                        File.Move(modsZip[i].Replace(".zip", ".exe"), modsZip[i]);
                                        MsgInfo(lc_text[6] + " \"" + modName + "\"" + lc_text[17]);
                                    }
                                }
                            }
                            TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress);
                            button1.Enabled = true;
                            if (checkBox1.Checked == true)
                            {
                                pictureBox10.Visible = true;
                                IsBak = true;
                            }
                            if (checkBox3.Checked == false)
                            {
                                pictureBox10.Visible = true;
                                IsBak = true;
                            }
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
                    else
                    {
                        MsgWarning(lc_text[25]);
                        pictureBox10.Visible = false;
                        IsBak = false; NotDone = true;
                    }
                }
                else
                {
                    try { Directory.Delete(@Application.StartupPath + @"\files\patches", true); } catch { }
                    button1.Visible = false;
                    darkButton4.Visible = true;
                    NotDone = true;
                }
            }
            else
            {
                button1.Visible = false;
                darkButton4.Visible = true;
                NotDone = true;
            }
        }

        //void Logger(string type, string ido, string status) { darkListView2.Items.Add(new VitNXListItem("[" + type + "]  " + ido + "=" + status)); }

        private static string GetMD5(string file)
        {
            try { return FileSystem.GetFileMD5(file); }
            catch { return "0x50 0x45"; }
        }

        private void darkButton6_Click(object sender, EventArgs e)
        {
            ScreenShotViewer.Visible = false;
            darkButton1.Visible = true;
            darkButton2.Visible = true;
        }

        private void darkButton7_Click(object sender, EventArgs e)
        {
            try { Process.Start(ScreenShotInViewer.ImageLocation); }
            catch
            {
                MsgWarning(lc_text[26]);
                Clipboard.SetText(ScreenShotInViewer.ImageLocation);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectPathToGame();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.CreateShortcut = checkBox2.Checked;
            config.WriteXml();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.CreateBackups = checkBox1.Checked;
            config.WriteXml();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.RGL_GarbageCleaning = checkBox4.Checked;
            config.WriteXml();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.RegisterGamePath = checkBox6.Checked;
            config.WriteXml();
        }

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void checkBox7_VisibleChanged(object sender, EventArgs e)
        {
            pictureBox15.Visible = checkBox7.Visible;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.Forced = checkBox5.Checked;
            config.WriteXml();
            button1.Visible = checkBox5.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.CopyGameToNewPath = checkBox3.Checked;
            config.WriteXml();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.ResetGame = checkBox9.Checked;
            config.WriteXml();
        }

        private void darkButton3_Click(object sender, EventArgs e)
        {
            try { Process.Start(site_link); }
            catch
            {
                MsgWarning(lc_text[26]);
                Clipboard.SetText(site_link);
            }
        }

        private void darkButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ScreenShot_Click(object sender, EventArgs e)
        {
            darkButton1.Visible = false;
            darkButton2.Visible = false;
            ScreenShotInViewer.ImageLocation = ScreenShot.ImageLocation;
            ScreenShotViewer.Visible = true;
        }

        private void modsList_MouseDown(object sender, MouseEventArgs e)
        {
            NativeFunctions.SetFocus(IntPtr.Zero);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
            {
                try { Process.Start(GamePath.Text + @"\gta_sa.exe"); }
                catch
                {
                    MsgWarning(lc_text[25]);
                    pictureBox10.Visible = false;
                    IsBak = false;
                }
            }
            else
            {
                try { Process.Start(GamePath.Text + @"_Downgraded\gta_sa.exe"); }
                catch
                {
                    MsgWarning(lc_text[25]);
                    pictureBox10.Visible = false;
                    IsBak = false;
                }
            }
        }

        private void MsgInfo(string message)
        {
            VitNX_MessageBox.ShowInfo(message, lc_text[9]);
        }

        private void MsgWarning(string message)
        {
            VitNX_MessageBox.ShowWarning(message, lc_text[11]);
        }

        private void MsgError(string message)
        {
            VitNX_MessageBox.ShowError(message, lc_text[10]);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (Directory.Exists(cache))
                    Directory.Delete(cache, true);
            }
            catch { }
        }

        private void button1_VisibleChanged(object sender, EventArgs e)
        {
            IsDD = button1.Visible;
        }

        private void stagesPanel_VisibleChanged(object sender, EventArgs e)
        {
            sp = stagesPanel.Visible;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void darkButton6_Click_1(object sender, EventArgs e)
        {
            try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/AddNewData.md#add-new-modifications"); }
            catch
            {
                MsgWarning(lc_text[26]);
                Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/AddNewData.md#add-new-modifications");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); }
            catch
            {
                MsgWarning(lc_text[26]);
                Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage");
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            config.Fields.EnableDirectPlay = checkBox7.Checked;
            config.WriteXml();
        }

        private async void MegaDownloader(string url, string file, string label, int code)
        {
            try { TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Normal); } catch { }
            if (File.Exists(file)) File.Delete(file);
            var client = new MegaApiClient();
            client.LoginAnonymous();
            Uri zip_link_uri = new Uri(url);
            INodeInfo node = client.GetNodeFromLink(zip_link_uri);
            labelPartProgress.Text = label + " (" + Convert.ToDouble(node.Size / 1048576).ToString("#.# " + lc_text[7] + ")");
            progressPanel.Visible = true;
            stagesPanel.Visible = false;
            PartProgressBar.Value = 0;
            PartProgressBar.CustomText = "";
            IProgress<double> ph = new Progress<double>(x =>
            {
                PartProgressBar.Value = (int)x;
                try { TbProgressBar.SetValue(Handle, (int)x, 100); } catch { }
                // ToDo 
                // Add text with Mb of downloaded file 
                // PartProgressBar.CustomText = $"{lc_text[35]} {(int)x} {lc_text[7]}";
            });
            await client.DownloadFileAsync(zip_link_uri, file, ph);
            client.Logout();
            if (client.IsLoggedIn == false)
            {
                if (code == 0)
                {
                    try { TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Indeterminate); } catch { }
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + file + "\" -o\"" + @Application.StartupPath + "\\files\" -y").WaitForExit();
                    File.Delete(file);
                    darkButton4.Visible = false;
                    button1.Visible = true;
                    try { TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress); } catch { }
                }
                if (code == 1)
                {
                    try { TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Indeterminate); } catch { }
                    Process.Start(@Application.StartupPath + @"\files\7z.exe", "x \"" + file + "\" -o\"" + @Application.StartupPath + "\\files\" -y").WaitForExit();
                    File.Delete(file);
                    try { TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress); } catch { }
                }
                progressPanel.Visible = false;
                stagesPanel.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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
                        using (System.Net.WebClient mods = new System.Net.WebClient())
                        {
                            string source = mods.DownloadString("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/info/v2.json");
                            var parsed = JsonConvert.DeserializeObject<Dictionary<string, ModsData>>(source);
                            modsList.Items.Clear();
                            foreach (var data in parsed)
                                modsList.Items.Add(data.Value.Name);
                        }
                        tabFix = true;
                        DSPanel.Visible = true;
                        ModsPanel.Visible = true;
                    }
                    catch
                    {
                        MsgWarning(lc_text[27]);
                        ModsPanel.Visible = false;
                        DSPanel.Visible = false;
                        tabFix = false;
                    }
                }
                else
                {
                    ModsPanel.Visible = false;
                    DSPanel.Visible = false;
                    tabFix = false;
                }
            }
            catch
            {
                MsgWarning(lc_text[27]);
                ModsPanel.Visible = false;
                DSPanel.Visible = false;
                tabFix = false;
            }
        }

        private bool IsInstaller(string file)
        {
            var twoBytes = new byte[2];
            using (var fileStream = File.Open(file, FileMode.Open))
                fileStream.Read(twoBytes, 0, 2);
            return Encoding.UTF8.GetString(twoBytes) == "MZ";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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
                        string lg = "English";
                        using (StringReader reader = new StringReader(File.ReadAllText(langs[i])))
                        {
                            var LOCAL = (LanguagesString)lzol.Deserialize(reader);
                            lg = LOCAL.Language;
                        }
                        darkComboBox2.Items.Add(lg);
                        if (langcode == new FileInfo(langs[i]).Name.Replace(".xml", ""))
                            darkComboBox2.SelectedItem = lg;
                    }
                }
            }
            else { LangsPanel.Visible = false; lpFix = false; }
        }

        private void SelectPathToGame()
        {
            var dialog = new FolderDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                Title = lc_text[12]
            };
            if (dialog.Show())
                GamePath.Text = dialog.FileName;
            else
                GamePath.Clear();
        }

        private string[] lc_text = new string[40];

        private void Translate()
        {
            language.ReadXml();
            langcode = language.Fields.LanguageCode;
            try
            {
                using (StringReader reader = new StringReader(File.ReadAllText(@Application.StartupPath + @"\files\languages\" + langcode + ".xml")))
                {
                    var LOCAL = (LanguagesString)lzol.Deserialize(reader);
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
                    lc_text[35] = LOCAL.Downloaded;
                    lc_text[36] = LOCAL.Progress;
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
                    lc_text[11] = LOCAL.Warning;
                    lc_text[10] = LOCAL.Error;
                    lc_text[33] = LOCAL.Question;
                    lc_text[12] = LOCAL.FolderSelectDialog;
                    // Information messages loading
                    lc_text[17] = LOCAL.ModSucces;
                    lc_text[19] = LOCAL.BindingOK;
                    lc_text[20] = LOCAL.ReturnUsingBackups;
                    lc_text[34] = LOCAL.Done;
                    // Warning messages loading
                    lc_text[25] = LOCAL.PathNotFound;
                    lc_text[26] = LOCAL.BrowserNotFound;
                    lc_text[29] = LOCAL.NewPath;
                    lc_text[30] = LOCAL.YouCanDelete;
                    // Error messages loading
                    lc_text[21] = LOCAL.AboutModDamaged;
                    lc_text[22] = LOCAL.ModFailure;
                    // Question messages loading
                    lc_text[18] = LOCAL.QEnableDirectPlay;
                    lc_text[13] = LOCAL.WishDownloadPatches;
                    lc_text[14] = LOCAL.WishDownloadDirectXFiles;
                    lc_text[15] = LOCAL.InstallModQuestion;
                    lc_text[16] = LOCAL.WishDowngrader;
                    lc_text[23] = LOCAL.WishReturnUsingBackups;
                    lc_text[24] = LOCAL.WishRegGame;
                    lc_text[27] = LOCAL.NetworkNotFound;
                    lc_text[28] = LOCAL.OfflineMode;
                    // Debug mode loading
                    lc_text[31] = LOCAL.Activation;
                    lc_text[32] = LOCAL.Deactivation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (YesInstallMe.Checked == false)
            {
                progressPanel.Visible = false;
                stagesPanel.Visible = true;
                if (File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip"))
                    File.Delete(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip");
            }
            else
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
                            if (!Directory.Exists(cache + @"\zips"))
                                Directory.CreateDirectory(cache + @"\zips");
                            try
                            {
                                DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[15], lc_text[33]);
                                if (result == DialogResult.Yes)
                                {
                                    if (zip_link.Contains("mega.nz"))
                                        MegaDownloader(zip_link,
                                            cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip",
                                            lc_text[3] + " \"" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + "\"...", 2);
                                    else
                                    {
                                        TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Normal);
                                        progressPanel.Visible = true;
                                        stagesPanel.Visible = false;
                                        labelPartProgress.Text = lc_text[3] + " \"" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + "\"...";
                                        PartProgressBar.Value = 0;
                                        PartProgressBar.VisualMode = VitNX.Controls.VitNX_ProgressBarDisplayMode.TextAndPercentage;
                                        using (System.Net.WebClient wc = new System.Net.WebClient())
                                        {
                                            var sum = 0L;
                                            long prev = 0;
                                            var r = wc.OpenRead(zip_link);
                                            if ((Convert.ToInt64(wc.ResponseHeaders["Content-Length"]) / 1048576) >= 2)
                                                labelPartProgress.Text += " (" + (Convert.ToDouble(wc.ResponseHeaders["Content-Length"]) / 1048576).ToString("#.# " + lc_text[7] + ")");
                                            r.Close();
                                            wc.DownloadProgressChanged += (s, a) =>
                                            {
                                                var diff = a.BytesReceived - prev;
                                                sum += diff;
                                                prev = a.BytesReceived;
                                                TbProgressBar.SetValue(Handle, a.ProgressPercentage, 100);
                                                PartProgressBar.Value = a.ProgressPercentage;
                                                PartProgressBar.CustomText = $"{lc_text[35]} {sum / 1048576} {lc_text[7]}  {lc_text[36]}";
                                            };
                                            wc.DownloadFileCompleted += (s, a) =>
                                            {
                                                PartProgressBar.Value = 0;
                                                progressPanel.Visible = false;
                                                stagesPanel.Visible = true;
                                                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress);
                                                PartProgressBar.VisualMode = VitNX.Controls.VitNX_ProgressBarDisplayMode.Percentage;
                                            };
                                            wc.DownloadFileTaskAsync(new Uri(zip_link), @cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Error);
                                MsgError(lc_text[21]);
                                MsgError(ex.ToString() + "\nFile: " + cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip");
                                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress);
                            }
                        }
                        else { YesInstallMe.Checked = true; }
                    }
                }
                catch
                {
                    MsgWarning(lc_text[27]);
                    YesInstallMe.Checked = false;
                }
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
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
                        DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[14], lc_text[33]);
                        if (result == DialogResult.Yes)
                        {
                            try { MegaDownloader("https://mega.nz/file/0pFRwAqa#Arguk9cQLpXYeQgXnFfAp6cw6F5OIZFKP2tRTwNCArI", @Application.StartupPath + @"\files\ddirectx.7z", lc_text[5], 1); }
                            catch { checkBox8.Checked = false; }
                        }
                        else
                            checkBox8.Checked = false;
                    }
                    catch
                    {
                        TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Error);
                        MsgWarning(lc_text[27]);
                        checkBox8.Checked = false;
                        TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress);
                    }
                }
            }
            config.Fields.InstallDirectXComponents = checkBox8.Checked;
            config.WriteXml();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1)
            {
                try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); }
                catch
                {
                    MsgWarning(lc_text[26]);
                    Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage");
                }
            }
            if (e.KeyData == Keys.F4)
            {
                try { Process.Start("notepad", @Application.StartupPath + @"\files\downgrader.xml"); }
                catch { Process.Start(@Application.StartupPath + @"\files\downgrader.xml"); }
            }
            if (e.KeyData == Keys.F12)
            {
                if (db == false)
                {
                    MsgWarning(lc_text[31]);
                    config.Fields.UserMode = false;
                    config.WriteXml();
                    db = true;
                }
                else
                {
                    MsgWarning(lc_text[32]);
                    config.Fields.UserMode = true;
                    config.WriteXml();
                    db = false;
                }
                Data.DebugMode = db;
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
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y))
            {
                if (GamePath.Text != "")
                    pictureBox11_Click(sender, e);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Z))
            {
                if (IsBak == true)
                    pictureBox10_Click(sender, e);
            }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.S))
            {
                if ((IsDD == false) && (sp == true))
                    button1_Click(sender, e);
            }
        }

        private void darkButton1_Click(object sender, EventArgs e)
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

        private void darkButton2_Click(object sender, EventArgs e)
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

        private void darkComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    using (StringReader reader = new StringReader(File.ReadAllText(langs[i])))
                    {
                        var LOCAL = (LanguagesString)lzol.Deserialize(reader);
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

        private void darkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("github.com");
                DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[13], lc_text[33]);
                if (result == DialogResult.Yes)
                {
                    try { MegaDownloader("https://mega.nz/file/4tcXiSqA#8JzulAC0oABzinb7914sq2xkyxE7c6atSeMval-YWms", @Application.StartupPath + @"\files\dpatches.rar", lc_text[4], 0); }
                    catch
                    {
                        progressPanel.Visible = false;
                        stagesPanel.Visible = true;
                        button1.Visible = false;
                    }
                }
                else
                {
                    progressPanel.Visible = false;
                    stagesPanel.Visible = true;
                    button1.Visible = false;
                }
            }
            catch
            {
                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Error);
                MsgWarning(lc_text[27]);
                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress);
            }
            if (IsOnePointNull == true)
                button1.Visible = false;
        }

        private void modsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send("github.com");
                using (System.Net.WebClient mods = new System.Net.WebClient())
                {
                    string source = mods.DownloadString("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/info/v2.json");
                    var parsed = JsonConvert.DeserializeObject<Dictionary<string, ModsData>>(source);
                    foreach (var data in parsed)
                    {
                        if (modsList.Text == data.Value.Name)
                        {
                            ScreenShot.Enabled = true;
                            darkGroupBox1.Visible = true;
                            nameLabel.Text = lc_text[0] + ": " + data.Value.Name;
                            darkLabel5.Text = lc_text[1] + ": " + data.Value.Version;
                            darkLabel6.Text = lc_text[2] + ": " + data.Value.Author;
                            darkLabel4.Text = data.Value.Description;
                            site_link = data.Value.Site;
                            ScreenShot.ImageLocation = data.Value.Logo;
                            photos_links[0] = ScreenShot.ImageLocation;
                            photos_links[1] = data.Value.Screenshot1;
                            photos_links[2] = data.Value.Screenshot2;
                            zip_link = data.Value.File;
                            YesInstallMe.Checked = File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc_text[0] + ": ", "") + ".zip");
                        }
                    }
                }
            }
            catch
            {
                MsgError(lc_text[21]);
                ModsPanel.Visible = false;
                DSPanel.Visible = false;
                tabFix = false;
                ScreenShot.Enabled = false;
                zip_link = "application/zip";
            }
        }

        private void GamePath_TextChanged(object sender, EventArgs e)
        {
            if (GamePath.Text != "")
            {
                pictureBox11.Visible = true;
                for (int i = 0; i < fl.Length; i++)
                {
                    Data.PathToGame = GamePath.Text;
                    if (File.Exists(GamePath.Text + fl[i] + ".jpb"))
                    {
                        pictureBox10.Visible = true;
                        IsBak = true;
                    }
                }
                //if (((GetMD5(@GamePath.Text + @"\gta_sa.exe") == "6687A315558935B3FC80CDBFF04437A4") ||
                //    (GetMD5(@GamePath.Text + @"\gta-sa.exe") == "6687A315558935B3FC80CDBFF04437A4")) &&
                //    ((!File.Exists(@GamePath.Text + @"\MTLX.dll")) ||
                //    (!File.Exists(@GamePath.Text + @"\index.bin"))))
                //{
                //    pictureBox10.Visible = true;
                //    IsBak = true;
                //    Data.PathToGame = GamePath.Text;
                //}
                if (File.Exists(GamePath.Text + @"\gta_sa.exe"))
                {
                    if ((GetMD5(@GamePath.Text + @"\gta_sa.exe") != "E7697A085336F974A4A6102A51223960")
                        && (GetMD5(@GamePath.Text + @"\gta_sa.exe") != "170B3A9108687B26DA2D8901C6948A18")
                        && (GetMD5(@GamePath.Text + @"\gta_sa.exe") != "91A9F6611ADDFB46682B56F9E247DB84")
                        && (GetMD5(@GamePath.Text + @"\gta_sa.exe") != "9369501599574D19AC93DE41547C4EC1"))
                    {
                        checkBox5.Visible = false;
                        checkBox5.Checked = false;
                        IsOnePointNull = false;
                        button1.Visible = true;
                    }
                    else
                    {
                        checkBox5.Visible = true;
                        IsOnePointNull = true;
                        button1.Visible = false;
                    }
                }
                else
                {
                    if ((GetMD5(@GamePath.Text + @"\gta-sa.exe") != "E7697A085336F974A4A6102A51223960")
                        && (GetMD5(@GamePath.Text + @"\gta-sa.exe") != "170B3A9108687B26DA2D8901C6948A18")
                        && (GetMD5(@GamePath.Text + @"\gta-sa.exe") != "91A9F6611ADDFB46682B56F9E247DB84")
                        && (GetMD5(@GamePath.Text + @"\gta-sa.exe") != "9369501599574D19AC93DE41547C4EC1"))
                    {
                        checkBox5.Visible = false;
                        checkBox5.Checked = false;
                        IsOnePointNull = false;
                        button1.Visible = true;
                    }
                    else
                    {
                        checkBox5.Visible = true;
                        IsOnePointNull = true;
                        button1.Visible = false;
                    }
                }
            }
            else
            {
                pictureBox11.Visible = false;
                pictureBox10.Visible = false;
                IsBak = false;
                IsOnePointNull = false;
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[23], lc_text[33]);
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
                            TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Normal);
                            originalGameRestoreProgressBar.Maximum = fl.Length;
                            for (int i = 0; i < fl.Length; i++)
                            {
                                if (File.Exists(@GamePath.Text + fl[i] + ".jpb"))
                                {
                                    try
                                    {
                                        File.SetAttributes(@GamePath.Text + fl[i], FileAttributes.Normal);
                                        File.Delete(@GamePath.Text + fl[i]);
                                    }
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
                                        try
                                        {
                                            File.SetAttributes(@GamePath.Text + fl[i], FileAttributes.Normal);
                                            File.Delete(@GamePath.Text + fl[i]);
                                        }
                                        catch { }
                                        try { File.Copy(@GamePath.Text + fl[0], @GamePath.Text + fl[1]); } catch { }
                                    }
                                }
                                originalGameRestoreProgressBar.Value = i + 1;
                                TbProgressBar.SetValue(Handle, i + 1, fl.Length);
                            }
                            if (((GetMD5(@GamePath.Text + @"\gta_sa.exe") == "6687A315558935B3FC80CDBFF04437A4") ||
                            (GetMD5(@GamePath.Text + @"\gta-sa.exe") == "6687A315558935B3FC80CDBFF04437A4")) &&
                            ((!File.Exists(@GamePath.Text + @"\MTLX.dll")) ||
                            (!File.Exists(@GamePath.Text + @"\index.bin"))))
                            {
                                TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.Indeterminate);
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
                            TbProgressBar.SetState(Handle, TbProgressBar.TaskbarStates.NoProgress);
                            MsgInfo(lc_text[20]);
                            pictureBox10.Visible = false;
                            IsBak = false;
                            originalGameRestoreProgressBar.Visible = false;
                        });
                    }
                    else
                    {
                        try
                        {
                            File.SetAttributes(@GamePath.Text + @"\gta_sa.exe.jpb", FileAttributes.Normal);
                            File.Delete(@GamePath.Text + @"\gta_sa.exe.jpb");
                        }
                        catch { }
                        try
                        {
                            File.SetAttributes(@GamePath.Text + @"\gta-sa.exe.jpb", FileAttributes.Normal);
                            File.Delete(@GamePath.Text + @"\gta-sa.exe.jpb");
                        }
                        catch { }
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

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[24], lc_text[33]);
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

        private void progressPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (progressPanel.Visible == true)
            {
                LangsPanel.Visible = false;
                HelloUser.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
            }
            else
            {
                HelloUser.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
            }
        }

        private void darkButton8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < photos_links.Length; i++)
            {
                try
                {
                    if (ScreenShotInViewer.ImageLocation == photos_links[i])
                    {
                        ScreenShotInViewer.ImageLocation = photos_links[i + 1];
                        i = photos_links.Length + 1;
                    }
                }
                catch
                {
                    ScreenShotInViewer.ImageLocation = photos_links[i];
                    i = photos_links.Length + 1;
                }
            }
        }

        private void darkButton9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < photos_links.Length; i++)
            {
                try
                {
                    if (ScreenShotInViewer.ImageLocation == photos_links[i])
                    {
                        ScreenShotInViewer.ImageLocation = photos_links[i - 1];
                        i = photos_links.Length + 1;
                    }
                }
                catch
                {
                    ScreenShotInViewer.ImageLocation = photos_links[i];
                    i = photos_links.Length + 1;
                }
            }
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            DialogResult result = VitNX_MessageBox.ShowQuestion(lc_text[18], lc_text[33]);
            if (result == DialogResult.Yes)
            {
                try { Process.Start("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart").WaitForExit(); } catch { }
                try { Process.Start("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart /all").WaitForExit(); } catch { }
                MsgInfo(lc_text[34]);
            }
        }
    }
}