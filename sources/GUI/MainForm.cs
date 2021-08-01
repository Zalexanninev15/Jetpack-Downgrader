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
using Microsoft.VisualBasic.FileIO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace JetpackGUI
{
    public partial class MainForm : Form
    {
        [DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }
        [DllImport("user32.dll")]
        extern static IntPtr SetFocus(IntPtr hWnd);

        IniEditor cfg = new IniEditor(@Application.StartupPath + @"\files\jpd.ini");
        string[] lc = new string[44];
        string[] mse = new string[10];
        string[] langs = new string[10];
        bool tabFix = false;
        bool lpFix = false;
        bool IsBak = false;
        bool sp = true;
        bool NotDone = true;
        bool IsDD = false;
        bool db = false;
        int BrokenOrLowered = 0;
        // 0 - normal
        // 1 - 1.0
        // 2 - unknown
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
                    DialogResult result = DarkMessageBox.ShowWarning(lc[29] + " " + lc[30], lc[8], DarkDialogButton.YesNo);
                    if (result == DialogResult.No) { Application.Exit(); }
                }
            }
            catch
            {
                DialogResult result = DarkMessageBox.ShowWarning(lc[29] + " " + lc[30], lc[8], DarkDialogButton.YesNo);
                if (result == DialogResult.No) { Application.Exit(); }
            }
            SettingsLoader();
        }

        void SettingsLoader()
        {
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
            catch (Exception ex) { MsgError(lc[2]); MsgError(ex.ToString()); }
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
                        DialogResult result = DarkMessageBox.ShowInformation(lc[36], lc[0], DarkDialogButton.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Indeterminate);
                            button1.Enabled = false;
                            int d = 0;
                            cfg.SetValue("JPD", "SelectFolder", "false");
                            cfg.SetValue("JPD", "UseMsg", "false");
                            cfg.SetValue("JPD", "Component", "true");
                            //DowngradeNative());
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
                                            if (modName != "ASI_Loader") { MsgInfo(lc[23] + " \"" + modName + "\" (" + lc[37] + " \"ASI Loader\") " + lc[24]); }
                                            if ((modName == "ASI_Loader") && (modsZip.Length == 1)) { MsgInfo(lc[23] + " \"" + modName + "\" " + lc[24]); }
                                        }
                                        catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgError(lc[23] + " \"" + modName + "\" " + lc[25]); }
                                    }
                                    else
                                    {
                                        if (checkBox3.Checked == true) { MessageBox.Show(lc[39] + ": \"" + GamePath.Text + "_Downgraded\"!\n" + lc[40], lc[8], MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification); }
                                        File.Move(modsZip[i], modsZip[i].Replace(".zip", ".exe"));
                                        Process.Start(modsZip[i].Replace(".zip", ".exe")).WaitForExit();
                                        File.Move(modsZip[i].Replace(".zip", ".exe"), modsZip[i]);
                                        MsgInfo(lc[23] + " \"" + modName + "\"" + lc[24]);
                                    }
                                }
                            }
                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                            MsgInfo(lc[4]);
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
                            button3.Visible = true;
                            darkButton5.Visible = true;
                        }
                    }
                    else { MsgWarning(lc[7]); pictureBox10.Visible = false; IsBak = false; NotDone = true; }
                }
                else { try { Directory.Delete(@Application.StartupPath + @"\files\patches", true); } catch { } button1.Visible = false; darkButton4.Visible = true; NotDone = true; }
            }
            else { button1.Visible = false; darkButton4.Visible = true; NotDone = true; }
        }

        //async void DowngradeNative()
        //{
        //    darkListView2.Items.Clear();
        //    AllProgressBar.Value = 0;
        //    PartProgressBar.Value = 0;
        //    progressPanel.Visible = true;
        //    stagesPanel.Visible = false;
        //    string path = GamePath.Text;
        //    string[] fl = new string[17]; string[] flmd5 = new string[17]; int er = 0, gv = 0; bool[] settings = new bool[18]; DialogResult result = DialogResult.No;
        //    // All files for downgrading (universal)
        //    fl[0] = @"\gta-sa.exe"; fl[1] = @"\gta_sa.exe"; fl[2] = @"\audio\CONFIG\TrakLkup.dat"; fl[3] = @"\audio\streams\BEATS";
        //    fl[4] = @"\audio\streams\CH"; fl[5] = @"\audio\streams\CR"; fl[6] = @"\audio\streams\CUTSCENE"; fl[7] = @"\audio\streams\DS";
        //    fl[8] = @"\audio\streams\MH"; fl[9] = @"\audio\streams\MR"; fl[10] = @"\audio\streams\RE"; fl[11] = @"\audio\streams\RG";
        //    fl[12] = @"\anim\anim.img"; fl[13] = @"\data\script\main.scm"; fl[14] = @"\data\script\script.img"; fl[15] = @"\models\gta_int.img";
        //    fl[16] = @"\models\gta3.img";
        //    // Original MD5 for files from game version 1.0
        //    flmd5[0] = "170B3A9108687B26DA2D8901C6948A18"; flmd5[1] = "E7697A085336F974A4A6102A51223960"; flmd5[2] = "528E75D663B8BAE072A01351081A2145"; flmd5[3] = "E26D86C7805D090D8210086876D6C35C";
        //    flmd5[4] = "FE31259226E0B4A8A963C70840E1FE8F"; flmd5[5] = "900148B8141EA4C1E782C3A48DBFBF3B"; flmd5[6] = "C25FCAA329B3D48F197FF4ED2A1D2A4D"; flmd5[7] = "9B4C18E4F3E82F0FEE41E30B2EA2246A";
        //    flmd5[8] = "909E7C4A7A29473E3885A96F987D7221"; flmd5[9] = "A1EC1CBE16DBB9F73022C6F33658ABE2"; flmd5[10] = "49B83551C684E17164F2047DCBA3E5AA"; flmd5[11] = "7491DC5325854C7117AF6E31900F38DD";
        //    flmd5[12] = "3359BA8CB820299161199EE7EF3F1C02"; flmd5[13] = "60AD23E272C3B0AA937053FE3006BE93"; flmd5[14] = "9598B82CF1E5AE7A8558057A01F6F2CE"; flmd5[15] = "DBE7E372D55914C39EB1D565E8707C8C";
        //    flmd5[16] = "9282E0DF8D7EEE3C4A49B44758DD694D";
        //    try
        //    {
        //        if (File.Exists(@Application.StartupPath + @"\files\jpd.ini") == false) { File.WriteAllText(@Application.StartupPath + @"\files\jpd.ini", Properties.Resources.jpd1); }
        //        IniEditor cfg = new IniEditor(@Application.StartupPath + @"\files\jpd.ini");
        //        settings[2] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackups"));
        //        settings[6] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateShortcut"));
        //        settings[7] = Convert.ToBoolean(cfg.GetValue("Downgrader", "ResetGame"));
        //        settings[14] = Convert.ToBoolean(cfg.GetValue("Downgrader", "GarbageCleaning"));
        //        settings[9] = Convert.ToBoolean(cfg.GetValue("Downgrader", "RegisterGamePath"));
        //        settings[10] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateNewGamePath"));
        //        settings[12] = Convert.ToBoolean(cfg.GetValue("Downgrader", "Forced"));
        //        settings[16] = Convert.ToBoolean(cfg.GetValue("Downgrader", "EnableDirectPlay"));
        //        settings[17] = Convert.ToBoolean(cfg.GetValue("Downgrader", "InstallDirectXComponents"));
        //        Logger("App", "jpd.ini", "true");
        //    }
        //    catch { Logger("App", "jpd.ini", "false"); }
        //    if (File.Exists(@Application.StartupPath + @"\files\patcher.exe"))
        //    {
        //        if ((path != "") && Directory.Exists(@path))
        //        {
        //            Logger("Game", "Path", "true");
        //            //
        //            // gv[number] - version
        //            // 0 - 1.0
        //            // 1 - Steam
        //            // 2 - 2.0
        //            // 3 - Rockstar Games Launcher
        //            // 4 - Unknown
        //            // 5 - Error
        //            // 6 - 1.01
        //            //
        //            // Get version (EXE)
        //            string SaEXE = @path + @"\gta-sa.exe";
        //            Logger("GamePath", "Current", @path);
        //            Logger("Downgrader", "Process", "Get version (EXE)...");
        //            if (File.Exists(SaEXE))
        //            {
        //                try
        //                {
        //                    string SteamEXEmd5 = GetMD5(SaEXE);
        //                    if (SteamEXEmd5 == "5BFD4DD83989A8264DE4B8E771F237FD") { gv = 1; Logger("Game", "Version", "Steam"); }
        //                    else
        //                    {
        //                        gv = 4;
        //                        SaEXE = @path + @"\gta_sa.exe";
        //                        try
        //                        {
        //                            string OtherEXEmd5 = GetMD5(SaEXE);
        //                            if (OtherEXEmd5 == "6687A315558935B3FC80CDBFF04437A4") { gv = 3; Logger("Game", "Version", "Rockstar Games Launcher"); }
        //                            if ((OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373") || (OtherEXEmd5 == "4E99D762F44B1D5E7652DFA7E73D6B6F")) { gv = 2; Logger("Game", "Version", "2.0"); }
        //                            if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373") && (OtherEXEmd5 != "4E99D762F44B1D5E7652DFA7E73D6B6F"))
        //                            {
        //                                if ((OtherEXEmd5 != "E7697A085336F974A4A6102A51223960") && (OtherEXEmd5 != "170B3A9108687B26DA2D8901C6948A18") && (OtherEXEmd5 != "91A9F6611ADDFB46682B56F9E247DB84") && (OtherEXEmd5 != "9369501599574D19AC93DE41547C4EC1"))
        //                                {
        //                                    if ((OtherEXEmd5 != "A2929A61E4D63DD3C15749B2B7ED74AE") && (OtherEXEmd5 != "25405921D1C47747FD01FD0BFE0A05AE")) { gv = 4; Logger("Game", "Version", "Unknown [NOT SUPPORTED]"); }
        //                                    else { gv = 6; Logger("Game", "Version", "1.01"); }
        //                                }
        //                                else
        //                                {
        //                                    gv = 0;
        //                                    Logger("Game", "Version", "1.0");
        //                                    if (settings[13] == true) { result = MessageBox.Show("Would you like to forcibly downgrade the EXE file to version 1.0? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                                    if ((result == DialogResult.Yes) || (settings[12] == true)) { settings[12] = true; }
        //                                }
        //                            }
        //                        }
        //                        catch { gv = 4; Logger("Game", "Version", "Unknown [NOT SUPPORTED]"); }
        //                    }
        //                }
        //                catch { gv = 5; er = 1; Logger("Game", "Version", "Unknown [ERROR]"); }
        //            }
        //            else
        //            {
        //                SaEXE = @path + @"\gta_sa.exe";
        //                try
        //                {
        //                    string OtherEXEmd5 = GetMD5(SaEXE);
        //                    if (OtherEXEmd5 == "5BFD4DD83989A8264DE4B8E771F237FD") { gv = 1; Logger("Game", "Version", "Steam"); }
        //                    else
        //                    {
        //                        gv = 4;
        //                        if (OtherEXEmd5 == "6687A315558935B3FC80CDBFF04437A4") { gv = 3; Logger("Game", "Version", "Rockstar Games Launcher"); }
        //                        if ((OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373") || (OtherEXEmd5 == "4E99D762F44B1D5E7652DFA7E73D6B6F")) { gv = 2; Logger("Game", "Version", "2.0"); }
        //                        if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373") && (OtherEXEmd5 != "4E99D762F44B1D5E7652DFA7E73D6B6F"))
        //                        {
        //                            if ((OtherEXEmd5 != "E7697A085336F974A4A6102A51223960") && (OtherEXEmd5 != "170B3A9108687B26DA2D8901C6948A18") && (OtherEXEmd5 != "91A9F6611ADDFB46682B56F9E247DB84") && (OtherEXEmd5 != "9369501599574D19AC93DE41547C4EC1"))
        //                            {
        //                                if ((OtherEXEmd5 != "A2929A61E4D63DD3C15749B2B7ED74AE") && (OtherEXEmd5 != "25405921D1C47747FD01FD0BFE0A05AE")) { gv = 4; Logger("Game", "Version", "Unknown [NOT SUPPORTED]"); }
        //                                else { gv = 6; Logger("Game", "Version", "1.01"); }
        //                            }
        //                            else
        //                            {
        //                                gv = 0;
        //                                Logger("Game", "Version", "1.0");
        //                                if (settings[13] == true) { result = MessageBox.Show("Would you like to forcibly downgrade the EXE file to version 1.0? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                                if ((result == DialogResult.Yes) || (settings[12] == true)) { settings[12] = true; }
        //                            }
        //                        }
        //                    }
        //                }
        //                catch { gv = 5; er = 1; Logger("Game", "Version", "Unknown [ERROR]"); }
        //            }
        //            if ((gv == 4) || (gv == 5)) { Logger("Downgrader", "Process", "Downgrade is not possible!"); }
        //            if ((File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set")) || (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.CommonDocuments) + @"\GTA San Andreas User Files\gta_sa.set")))
        //            {
        //                if ((settings[13] == true) && (gv != 4) && (gv != 5)) { result = MessageBox.Show("Would you like to reset the game settings to prevent possible difficulties in starting the game?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                if (((result == DialogResult.Yes) || (settings[7] == true)) && (gv != 4) && (gv != 5))
        //                {
        //                    Logger("Downgrader", "Process", "Deleting gta_sa.set (Documents) file...");
        //                    try
        //                    {
        //                        if (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set"))
        //                        {
        //                            File.Delete(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set");
        //                            Logger("ResetGame", "gta_sa.set (Documents)", "true");
        //                        }
        //                        else { Logger("ResetGame", "gta_sa.set (Documents)", "false"); }
        //                    }
        //                    catch { Logger("ResetGame", "gta_sa.set (Documents)", "false"); }
        //                    Logger("Downgrader", "Process", "Deleting gta_sa.set (Public Documents) file...");
        //                    try
        //                    {
        //                        if (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.CommonDocuments) + @"\GTA San Andreas User Files\gta_sa.set"))
        //                        {
        //                            File.Delete(@Environment.GetFolderPath(@Environment.SpecialFolder.CommonDocuments) + @"\GTA San Andreas User Files\gta_sa.set");
        //                            Logger("ResetGame", "gta_sa.set (Public Documents)", "true");
        //                        }
        //                        else { Logger("ResetGame", "gta_sa.set (Public Documents)", "false"); }
        //                    }
        //                    catch { Logger("ResetGame", "gta_sa.set (Public Documents)", "false"); }
        //                }
        //            }
        //            if ((settings[13] == true) && (gv != 5)) { result = MessageBox.Show("Would you like to enable DirectPlay to avoid possible problems with running the game? This operation is NECESSARY ONLY on Windows 10, if your version is lower (7/8/8.1), then your answer is No!!!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //            if (((result == DialogResult.Yes) || (settings[16] == true)) && (gv != 5))
        //            {
        //                Logger("DirectPlay", "Enabled", "false");
        //                Logger("DirectPlay", "Enabled", "In process...");
        //                try { Process.Start("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart").WaitForExit(); } catch { Logger("DirectPlay", "Enabled", "Error 1"); }
        //                try { Process.Start("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart /all").WaitForExit(); } catch { Logger("DirectPlay", "Enabled", "Error 2"); }
        //                Logger("DirectPlay", "Enabled", "true");
        //                Logger("DirectPlay", "Guide if DirectPlay not work", "https://docs.microsoft.com/en-us/answers/questions/108291/enable-windows-10-direct-play.html?childToView=111216#answer-111216");
        //            }
        //            if ((settings[13] == true) && (gv != 5)) { result = MessageBox.Show("Would you like to install DirectX 9.0c files to avoid possible problems with running the game?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //            if (((result == DialogResult.Yes) || (settings[17] == true)) && (gv != 5))
        //            {
        //                if (Directory.Exists(@Application.StartupPath + @"\files\DirectX"))
        //                {
        //                    try
        //                    {
        //                        Logger("DirectX", "Process", "Installing...");
        //                        Logger("DirectX", "Process", "App is not frozen, just busy right now...");
        //                        Process.Start(@Application.StartupPath + @"\files\DirectX\DXSETUP.exe", "/silent").WaitForExit();
        //                        Logger("DirectX", "Process", "Installation completed successfully");
        //                    }
        //                    catch { Logger("DirectX", "Process", "Installation error"); }
        //                }
        //                else { Logger("DirectX", "Process", "Installation error"); }
        //            }
        //            if ((settings[12] == true) && (gv == 0)) { gv = 6; settings[12] = true; }
        //            if ((gv != 0) && (er == 0) && (settings[3] == false))
        //            {
        //                // Check files
        //                Logger("Downgrader", "Process", "Scanning files...");
        //                if ((gv == 6) || (gv == 0)) // 1.01
        //                {
        //                    if (File.Exists(@path + fl[1]))
        //                    {
        //                        Logger("Game", @path + fl[1], "true");
        //                        File.SetAttributes(@path + fl[1], FileAttributes.Normal);
        //                        try { File.SetAttributes(@path + fl[1] + ".jpb", FileAttributes.Normal); } catch { }
        //                    }
        //                    else { er = 1; Logger("Game", @path + fl[1], "false"); }
        //                }
        //                if (gv == 3) // Rockstar Games Launcher
        //                {
        //                    using (var progress = new ProgressBar())
        //                    {
        //                        for (int i = 1; i < fl.Length; i++)
        //                        {
        //                            if (File.Exists(@path + fl[i]))
        //                            {
        //                                File.SetAttributes(@path + fl[i], FileAttributes.Normal);
        //                                try { File.SetAttributes(@path + fl[i] + ".jpb", FileAttributes.Normal); } catch { }
        //                                if (settings[15] == false) { Logger("Game", @path + fl[i], "true"); }
        //                            }
        //                            else { er = 1; if (settings[15] == false) { Logger("Game", @path + fl[i], "false"); } }
        //                            if (settings[15] == true) { labelPartProgress.Text = "Checking files progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                        }
        //                    }
        //                }
        //                if (gv == 2) // Version 2.0
        //                {
        //                    using (var progress = new ProgressBar())
        //                    {
        //                        for (int i = 1; i < fl.Length; i++)
        //                        {
        //                            if ((i >= 1) && (i > 11))
        //                            {
        //                                if (File.Exists(@path + fl[i]))
        //                                {
        //                                    File.SetAttributes(@path + fl[i], FileAttributes.Normal);
        //                                    try { File.SetAttributes(@path + fl[i] + ".jpb", FileAttributes.Normal); } catch { }
        //                                    if (settings[15] == false) { Logger("Game", @path + fl[i], "true"); }
        //                                }
        //                                else { er = 1; if (settings[15] == false) { Logger("Game", @path + fl[i], "false"); } }
        //                            }
        //                            if (settings[15] == true) { labelPartProgress.Text = "Checking files progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                        }
        //                    }
        //                }
        //                if (gv == 1) // Steam
        //                {
        //                    using (var progress = new ProgressBar())
        //                    {
        //                        for (int i = 0; i < fl.Length; i++)
        //                        {
        //                            if (i != 1)
        //                            {
        //                                if (File.Exists(@path + fl[i]))
        //                                {
        //                                    File.SetAttributes(@path + fl[i], FileAttributes.Normal);
        //                                    try { File.SetAttributes(@path + fl[i] + ".jpb", FileAttributes.Normal); } catch { }
        //                                    if (settings[15] == false) { Logger("Game", @path + fl[i], "true"); }
        //                                }
        //                                else
        //                                {
        //                                    er = 1;
        //                                    if (settings[15] == false) { Logger("Game", @path + fl[i], "false"); }
        //                                }
        //                            }
        //                            if (settings[15] == true) { labelPartProgress.Text = "Checking files progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                        }
        //                    }
        //                }
        //                bool fisv = false;
        //                if ((er == 0) && (settings[4] == false))
        //                {
        //                    // Checking files before downgrade (MD5)
        //                    string GameMD5 = "";
        //                    Logger("Game", "All", "true");
        //                    Logger("Downgrader", "Process", "Checking original files before downgrade (MD5)...");
        //                    if (gv == 3) // Rockstar Games Launcher
        //                    {
        //                        using (var progress = new ProgressBar())
        //                        {
        //                            for (int i = 2; i < fl.Length; i++)
        //                            {
        //                                try
        //                                {
        //                                    GameMD5 = GetMD5(@path + fl[i]);
        //                                    if (settings[15] == false) { Logger("GameMD5", @path + fl[i], GameMD5); }
        //                                    if (GameMD5 == flmd5[i])
        //                                    {
        //                                        fisv = true;
        //                                        if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "1.0"); }
        //                                    }
        //                                    else { if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "Higher than 1.0!"); } }
        //                                }
        //                                catch { fisv = true; if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "File not found!"); } }
        //                                if (settings[15] == true) { labelPartProgress.Text = "MD5 verification progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                            }
        //                        }
        //                    }
        //                    if (gv == 2) // Version 2.0
        //                    {
        //                        using (var progress = new ProgressBar())
        //                        {
        //                            for (int i = 2; i < fl.Length; i++)
        //                            {
        //                                if ((i >= 2) && (i > 11))
        //                                {
        //                                    try
        //                                    {
        //                                        GameMD5 = GetMD5(@path + fl[i]);
        //                                        if (settings[15] == false) { Logger("GameMD5", @path + fl[i], GameMD5); }
        //                                        if (GameMD5 == flmd5[i])
        //                                        {
        //                                            fisv = true;
        //                                            if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "1.0"); }
        //                                        }
        //                                        else { if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "Higher than 1.0!"); } }
        //                                    }
        //                                    catch { fisv = true; if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "File not found!"); } }
        //                                }
        //                                if (settings[15] == true) { labelPartProgress.Text = "MD5 verification progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                            }
        //                        }
        //                    }
        //                    if (gv == 1) // Steam version
        //                    {
        //                        using (var progress = new ProgressBar())
        //                        {
        //                            for (int i = 2; i < fl.Length; i++)
        //                            {
        //                                if (i >= 2)
        //                                {
        //                                    try
        //                                    {
        //                                        GameMD5 = GetMD5(@path + fl[i]);
        //                                        if (settings[15] == false) { Logger("GameMD5", @path + fl[i], GameMD5); }
        //                                        if (GameMD5 == flmd5[i])
        //                                        {
        //                                            fisv = true;
        //                                            if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "1.0"); }
        //                                        }
        //                                        else { if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "Higher than 1.0!"); } }
        //                                    }
        //                                    catch { fisv = true; if (settings[15] == false) { Logger("GameMD5", @path + fl[i], "File not found!"); } }
        //                                }
        //                                if (settings[15] == true) { labelPartProgress.Text = "MD5 verification progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                            }
        //                        }
        //                    }
        //                    if (settings[12] == true)
        //                    {
        //                        Logger("Downgrader", "Process", "Forced downgrade mode is used...");
        //                        fisv = false;
        //                        gv = 6;
        //                    }
        //                    if ((fisv == false) && (settings[5] == false))
        //                    {
        //                        if ((settings[13] == true) && ((gv == 1) || (gv == 3))) { result = MessageBox.Show("Would you like to create a copy of the game folder to prevent accidental updates to the game?\nIf you have a game from Steam/Rockstar Games Launcher - we strongly recommend choosing Yes!!!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                        if (((result == DialogResult.Yes) || (settings[10] == true)) && ((gv == 1) || (gv == 3)))
        //                        {
        //                            settings[10] = true;
        //                            Logger("Downgrader", "Process", "Copying the game folder before downgrading...");
        //                            Logger("Downgrader", "Process", "App is not frozen, just busy right now...");
        //                            try
        //                            {
        //                                try { Directory.Delete(@path + "_Downgraded", true); } catch { }
        //                                FileSystem.CopyDirectory(@path, @path + "_Downgraded");
        //                                path = @path + "_Downgraded";
        //                                Logger("NewGamePath", "Path", @path);
        //                            }
        //                            catch { er = 0; Logger("NewGamePath", "Path", "false"); }
        //                        }
        //                        // Backup (optional)
        //                        if (settings[13] == true) { result = MessageBox.Show("Do you want to create backups of files?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                        if ((result == DialogResult.Yes) || (settings[2] == true))
        //                        {
        //                            settings[2] = true;
        //                            Logger("Downgrader", "Process", "Create backups...");
        //                            if (gv == 6) // 1.01
        //                            {
        //                                if (File.Exists(@path + fl[1] + ".jpb")) { File.Delete(@path + fl[1] + ".jpb"); }
        //                                try { File.Move(@path + fl[1], @path + fl[1] + ".jpb"); Logger("GameBackup", @path + fl[1], "Done!"); }
        //                                catch { er = 1; Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); }
        //                            }
        //                            if (gv == 3) // Rockstar Games Launcher
        //                            {
        //                                if (File.Exists(@path + fl[1] + ".jpb")) { File.Delete(@path + fl[1] + ".jpb"); }
        //                                try { File.Move(@path + fl[1], @path + fl[1] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "Done!"); } }
        //                                catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); } }
        //                                using (var progress = new ProgressBar())
        //                                {
        //                                    for (int i = 2; i < fl.Length; i++)
        //                                    {
        //                                        if (File.Exists(@path + fl[i] + ".jpb")) { File.Delete(@path + fl[i] + ".jpb"); }
        //                                        try { File.Move(@path + fl[i], @path + fl[i] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[i], "Done!"); } }
        //                                        catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); } }
        //                                        if (settings[15] == true) { labelPartProgress.Text = "Backup progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                    }
        //                                }
        //                            }
        //                            if (gv == 2) // Version 2.0
        //                            {
        //                                if (File.Exists(@path + fl[1] + ".jpb")) { File.Delete(@path + fl[1] + ".jpb"); }
        //                                try { File.Move(@path + fl[1], @path + fl[1] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "Done!"); } }
        //                                catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); } }
        //                                using (var progress = new ProgressBar())
        //                                {
        //                                    for (int i = 2; i < fl.Length; i++)
        //                                    {
        //                                        if ((i >= 2) && (i > 11))
        //                                        {
        //                                            if (File.Exists(@path + fl[i] + ".jpb")) { File.Delete(@path + fl[i] + ".jpb"); }
        //                                            try { File.Move(@path + fl[i], @path + fl[i] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[i], "Done!"); } }
        //                                            catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); } }
        //                                        }
        //                                        if (settings[15] == true) { labelPartProgress.Text = "Backup progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                    }
        //                                }
        //                            }
        //                            if (gv == 1) // Steam version
        //                            {
        //                                if (File.Exists(@path + fl[0] + ".jpb")) { File.Delete(@path + fl[0] + ".jpb"); }
        //                                try
        //                                {
        //                                    File.Move(@path + fl[0], @path + fl[0] + ".jpb");
        //                                    if (settings[15] == false) { Logger("GameBackup", @path + fl[0], "Done!"); }
        //                                }
        //                                catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[0], "File for backup wasn't found!"); } }
        //                                using (var progress = new ProgressBar())
        //                                {
        //                                    for (int i = 2; i < fl.Length; i++)
        //                                    {
        //                                        if (i >= 2)
        //                                        {
        //                                            if (File.Exists(@path + fl[i] + ".jpb")) { File.Delete(@path + fl[i] + ".jpb"); }
        //                                            try { File.Move(@path + fl[i], @path + fl[i] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[i], "Done!"); } }
        //                                            catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); } }
        //                                        }
        //                                        if (settings[15] == true) { labelPartProgress.Text = "Backup progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                    }
        //                                }
        //                            }
        //                            if (er == 0) { Logger("GameBackup", "All", "true"); }
        //                        }
        //                        if (er == 0)
        //                        {
        //                            if (Directory.Exists(@Application.StartupPath + @"\files\patches"))
        //                            {
        //                                Logger("Downgrader", "Process", "Downgrading...");
        //                                try
        //                                {
        //                                        Process.Start(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\7z.exe", "x \"" + @Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\patches\\game.jpp\" -o\"" + @path + "\"" + "-y").WaitForExit();
        //        File.Move(@path + @"\game.jpp", @path + fl[1]);
        //                                                if (settings[15] == false) { Logger("NewGame", @path + fl[1], "1.0");
        //    }
        //                                                if (gv == 1)
        //                                                {
        //                                                    Process.Start(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\7z.exe", "x \"" + @Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\patches\\game.jpp\" -o\"" + @path + "\"" + "-y").WaitForExit();
        //    File.Move(@path + @"\game.jpp", @path + fl[0]);
        //                                                    if (settings[15] == false) { Logger("NewGame", @path + fl[0], "1.0");
        //}
        //                                                }
        //                                      if ((gv == 3) || (gv == 1)) // Rockstar Games Launcher & Steam
        //                                    {
        //                                        using (var progress = new ProgressBar())
        //                                        {
        //                                            for (int i = 2; i < fl.Length; i++)
        //                                            {
        //                                                // Old: par = " -d -s " + '"' ...
        //                                                string par = '"' + @path + fl[i] + '"' + " " + '"' + @Application.StartupPath + @"\files\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"';
        //                                                if (settings[2] == true) { par = '"' + @path + fl[i] + ".jpb" + '"' + " " + '"' + @Application.StartupPath + @"\files\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"'; }
        //                                                Patcher(@par);
        //                                                if (settings[2] == false) { File.Delete(@path + fl[i]); }
        //                                                File.Move(@path + fl[i] + ".temp", @path + fl[i]);
        //                                                if (settings[15] == false) { Logger("NewGame", @path + fl[i], "1.0"); }
        //                                                if (settings[15] == true) { labelPartProgress.Text = "Downgrade progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                            }
        //                                        }
        //                                    }
        //                                    if (gv == 2) // 2.0
        //                                    {
        //                                        using (var progress = new ProgressBar())
        //                                        {
        //                                            for (int i = 2; i < fl.Length; i++)
        //                                            {
        //                                                if ((i >= 2) && (i > 11))
        //                                                {
        //                                                    string par = '"' + @path + fl[i] + '"' + " " + '"' + @Application.StartupPath + @"\files\patches\" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"';
        //                                                    if (settings[2] == true) { par = '"' + @path + fl[i] + ".jpb" + '"' + " " + '"' + @Application.StartupPath + @"\files\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"'; }
        //                                                    Patcher(@par);
        //                                                    if (settings[2] == false) { File.Delete(@path + fl[i]); }
        //                                                    File.Move(@path + fl[i] + ".temp", @path + fl[i]);
        //                                                    if (settings[15] == false) { Logger("NewGame", @path + fl[i], "1.0"); }
        //                                                }
        //                                                if (settings[15] == true) { labelPartProgress.Text = "Downgrade progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                                catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game files!"); }
        //                                if (er == 0)
        //                                {
        //                                    Logger("NewGame", "All", "1.0");
        //                                    if (settings[0] == true) { Logger("NewGameReadOnly", "All", "true"); }
        //                                    // Checking files after downgrade (MD5)
        //                                    Logger("Downgrader", "Process", "Checking files after downgrade (MD5)...");
        //                                    if (gv == 6) // 1.01
        //                                    {
        //                                        try
        //                                        {
        //                                            GameMD5 = GetMD5(@path + fl[1]);
        //                                            Logger("NewGameMD5", @path + fl[1], GameMD5);
        //                                            if (GameMD5 == flmd5[0]) { fisv = true; Logger("NewGameMD5", @path + fl[1], "1.0"); }
        //                                            else { fisv = false; Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); }
        //                                        }
        //                                        catch { fisv = false; Logger("NewGameMD5", @path + fl[1], "File not found!"); }
        //                                    }
        //                                    if (gv == 3) // Rockstar Games Launcher
        //                                    {
        //                                        try
        //                                        {
        //                                            GameMD5 = GetMD5(@path + fl[1]);
        //                                            if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], GameMD5); }
        //                                            if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "1.0"); } }
        //                                            else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); } }
        //                                        }
        //                                        catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "File not found!"); } }
        //                                        using (var progress = new ProgressBar())
        //                                        {
        //                                            for (int i = 2; i < fl.Length; i++)
        //                                            {
        //                                                try
        //                                                {
        //                                                    GameMD5 = GetMD5(@path + fl[i]);
        //                                                    if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], GameMD5); }
        //                                                    if (GameMD5 == flmd5[i])
        //                                                    {
        //                                                        fisv = true;
        //                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "1.0"); }
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        fisv = false;
        //                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "Higher than 1.0!"); }
        //                                                    }
        //                                                }
        //                                                catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "File not found!"); } }
        //                                                if (settings[15] == true) { labelPartProgress.Text = "MD5 verification progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                            }
        //                                        }
        //                                    }
        //                                    if (gv == 2) // 2.0
        //                                    {
        //                                        try
        //                                        {
        //                                            GameMD5 = GetMD5(@path + fl[1]);
        //                                            if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], GameMD5); }
        //                                            if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "1.0"); } }
        //                                            else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); } }
        //                                        }
        //                                        catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "File not found!"); } }
        //                                        using (var progress = new ProgressBar())
        //                                        {
        //                                            for (int i = 2; i < fl.Length; i++)
        //                                            {
        //                                                if ((i >= 2) && (i > 11))
        //                                                {
        //                                                    try
        //                                                    {
        //                                                        GameMD5 = GetMD5(@path + fl[i]);
        //                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], GameMD5); }
        //                                                        if (GameMD5 == flmd5[i]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "1.0"); } }
        //                                                        else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "Higher than 1.0!"); } }
        //                                                    }
        //                                                    catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "File not found!"); } }
        //                                                }
        //                                                if (settings[15] == true) { labelPartProgress.Text = "MD5 verification progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                            }
        //                                        }
        //                                    }
        //                                    if (gv == 1) // Steam
        //                                    {
        //                                        try
        //                                        {
        //                                            GameMD5 = GetMD5(@path + fl[0]);
        //                                            if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], GameMD5); }
        //                                            if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], "1.0"); } }
        //                                            else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], "Higher than 1.0!"); } }
        //                                        }
        //                                        catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], "File not found!"); } }
        //                                        try
        //                                        {
        //                                            GameMD5 = GetMD5(@path + fl[1]);
        //                                            if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], GameMD5); }
        //                                            if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "1.0"); } }
        //                                            else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); } }
        //                                        }
        //                                        catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "File not found!"); } }
        //                                        using (var progress = new ProgressBar())
        //                                        {
        //                                            for (int i = 2; i < fl.Length; i++)
        //                                            {
        //                                                try
        //                                                {
        //                                                    GameMD5 = GetMD5(@path + fl[i]);
        //                                                    if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], GameMD5); }
        //                                                    if (GameMD5 == flmd5[i]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "1.0"); } }
        //                                                    else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "Higher than 1.0!"); } }
        //                                                }
        //                                                catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[i], "File not found!"); } }
        //                                                if (settings[15] == true) { labelPartProgress.Text = "MD5 verification progress"; PartProgressBar.Value = (int)((double)i / fl.Length); }
        //                                            }
        //                                        }
        //                                    }
        //                                    if (fisv == true)
        //                                    {
        //                                        Logger("NewGameMD5", "All", "true");
        //                                        Logger("Downgrader", "Game", "Downgrade completed!");
        //                                        if (File.Exists(@path + @"\index.bin") || File.Exists(@path + @"\MTLX.dll"))
        //                                        {
        //                                                 if ((settings[13] == true) && (gv == 3)) { result = MessageBox.Show("Do you want remove unneeded files that are not used by the game version 1.0?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                                                 if (((result == DialogResult.Yes) || (settings[14] == true)) && (gv == 3))
        //                                                 {
        //                                                     Logger("Downgrader", "Process", "Deleting index.bin file...");
        //                                                     try
        //                                                     {
        //                                                         if (File.Exists(@path + @"\index.bin")) { File.Delete(@path + @"\index.bin"); Logger("GarbageCleaning", "index.bin", "true"); }
        //                                                         else { Logger("GarbageCleaning", "index.bin", "false"); }
        //                                                     }
        //                                                     catch { Logger("GarbageCleaning", "index.bin", "false"); }
        //                                                     Logger("Downgrader", "Process", "Deleting MTLX.dll file...");
        //                                                     try
        //                                                     {
        //                                                         if (File.Exists(@path + @"\MTLX.dll")) { File.Delete(@path + @"\MTLX.dll"); Logger("GarbageCleaning", "MTLX.dll", "true"); }
        //                                                         else { Logger("GarbageCleaning", "MTLX.dll", "false"); }
        //                                                     }
        //                                                     catch { Logger("GarbageCleaning", "MTLX.dll", "false"); }
        //                                                 }
        //                                        }
        // //                                     if (settings[13] == true) { result = MessageBox.Show("Would you like to create an game shortcut on your Desktop?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                                        if ((result == DialogResult.Yes) || (settings[6] == true))
        //                                        {
        //                                            Logger("Downgrader", "Process", "Creating a shortcut...");
        //                                            try { Create(@Environment.GetFolderPath(@Environment.SpecialFolder.Desktop) + @"\GTA San Andreas 1.0.lnk", @path + @"\gta_sa.exe"); Logger("Downgrader", "CreateShortcut", "true"); }
        //                                            catch { Logger("Downgrader", "CreateShortcut", "false"); }
        //                                        }
        //                                        if (settings[13] == true) { result = MessageBox.Show("Would you like to register the game in the system?\n(for launchers, SAMP and other projects)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
        //                                        if ((result == DialogResult.Yes) || (settings[9] == true))
        //                                        {
        //                                            Logger("Downgrader", "Process", "Adding entries to the registry...");
        //                                            try
        //                                            {
        //                                                if (x64 == true)
        //                                                {
        //                                                    Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\GTA San Andreas\Installation");
        //                                                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Rockstar Games\GTA San Andreas\Installation", "ExePath", "\"" + GamePath.Text + "\"");
        //                                                }
        //                                                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rockstar Games\GTA San Andreas\Installation");
        //                                                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\GTA San Andreas\Installation", "ExePath", "\"" + GamePath.Text + "\"");
        //                                                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\GTA San Andreas\Installation", "Installed", "1");
        //                                                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rockstar Games\Launcher");
        //                                                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\Launcher", "Language", "en-US");
        //                                                Logger("Downgrader", "RegisterGamePath", "true");
        //                                            }
        //                                            catch { Logger("Downgrader", "RegisterGamePath", "false"); }
        //                                        }
        //                                        if (settings[13] == true) { MessageBox.Show("Downgrade completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //                                    }
        //                                    else { Logger("NewGameMD5", "All", "false"); Logger("Downgrader", "Game", "Error checking files!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); }
        //                                }
        //                            }
        //                            else { Logger("Downgrader", "NewGame", "Please make sure that you have downloaded the patches (patches folder), otherwise, the downgrader will not be able to start its work!"); }
        //                        }
        //                        else { Logger("GameBackup", "All", "Some game files were not found, so it is not possible to continue working!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); }
        //                    }
        //                    else { if (settings[5] == false) { Logger("GameMD5", "All", "It is impossible to determine exactly which version some files are taken from, because some of them have 1.0, and others are Higher than 1.0!!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); } }
        //                }
        //                else { if (settings[4] == false) { Logger("Game", "All", "Some game files were not found, so it is not possible to continue working!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); } }
        //            }
        //            if (gv == 0) { Logger("Downgrader", "Process", "Downgrade is not required!"); }
        //        }
        //        else { Logger("Game", "Path", "false"); }
        //    }
        //    else { Logger("Downgrader", "Process", "File patcher.exe was not found!"); }
        //    if (settings[1] == false) { Logger("GamePath", "Current", @path); Console.WriteLine("Press Enter to Exit"); }
        //}

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
        void checkBox2_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateShortcut", Convert.ToString(checkBox2.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox1_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateBackups", Convert.ToString(checkBox1.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox4_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "GarbageCleaning", Convert.ToString(checkBox4.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox6_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "RegisterGamePath", Convert.ToString(checkBox6.Checked).Replace("T", "t").Replace("F", "f")); }
        public MainForm() { InitializeComponent(); this.KeyPreview = true; }
        void checkBox5_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "Forced", Convert.ToString(checkBox5.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox3_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "CreateNewGamePath", Convert.ToString(checkBox3.Checked).Replace("T", "t").Replace("F", "f")); }
        void checkBox9_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "ResetGame", Convert.ToString(checkBox9.Checked).Replace("T", "t").Replace("F", "f")); }
        void darkButton3_Click(object sender, EventArgs e) { try { Process.Start(site_link); } catch { MsgError(lc[5]); } }
        void darkButton5_Click(object sender, EventArgs e) { Application.Exit(); }
        void ScreenShot_Click(object sender, EventArgs e) { try { Process.Start(ScreenShot.ImageLocation); } catch { MsgError(lc[5]); } }
        void listBox1_MouseDown(object sender, MouseEventArgs e) { SetFocus(IntPtr.Zero); }
        void button3_Click(object sender, EventArgs e) { try { Process.Start(GamePath.Text + @"\gta_sa.exe"); } catch { MsgWarning(lc[7]); pictureBox10.Visible = false; IsBak = false; } }
        void MsgInfo(string message) { DarkMessageBox.ShowInformation(message, lc[0]); }
        void MainForm_FormClosed(object sender, FormClosedEventArgs e) { if (Directory.Exists(cache)) { Directory.Delete(cache, true); } }
        void MsgError(string message) { DarkMessageBox.ShowError(message, lc[1]); }
        void MsgWarning(string message) { DarkMessageBox.ShowWarning(message, lc[8]); }
        void button1_VisibleChanged(object sender, EventArgs e) { IsDD = button1.Visible; }
        void stagesPanel_VisibleChanged(object sender, EventArgs e) { sp = stagesPanel.Visible; }
        //void Logger(string type, string ido, string status) { darkListView2.Items.Add(new DarkListItem("[" + type + "]  " + ido + "=" + status)); }
        void pictureBox3_Click(object sender, EventArgs e) { MsgInfo("- " + lc[38] + ": Jetpack GUI\n- " + lc[9] + ": " + Convert.ToString(Application.ProductVersion).Replace(".0", "") + "\n- " + lc[10] + ": Zalexanninev15 (programmer and creator) && Vadim M. (consultant)\n- " + lc[14]); }
        void pictureBox4_Click(object sender, EventArgs e) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader"); } catch { MsgError(lc[5]); } }
        void checkBox7_CheckedChanged(object sender, EventArgs e) { cfg.SetValue("Downgrader", "EnableDirectPlay", Convert.ToString(checkBox7.Checked).Replace("T", "t").Replace("F", "f")); }

        async void MegaDownloader(string url, string file, string label, int code)
        {
            try { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Normal); } catch { }
            if (File.Exists(file)) { File.Delete(file); }
            var client = new MegaApiClient();
            client.LoginAnonymous();
            Uri zip_link_uri = new Uri(url);
            INodeInfo node = client.GetNodeFromLink(zip_link_uri);
            labelPartProgress.Text = label + " (" + Convert.ToDouble(node.Size / 1048576).ToString("#.# " + lc[41] + ")");
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
                    catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgWarning(lc[29]); ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
                    TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                }
                else { ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
            }
            catch { MsgWarning(lc[29]); ModsPanel.Visible = false; DSPanel.Visible = false; tabFix = false; }
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
                langs = Directory.GetFiles(@Application.StartupPath + @"\files\languages", "*.ini");
                for (int i = 0; i < langs.Length; i++)
                {
                    if (langs[i] != "")
                    {
                        IniEditor lang = new IniEditor(langs[i]);
                        string lg = Convert.ToString(lang.GetValue("Interface", "Language"));
                        darkComboBox2.Items.Add(lg);
                        if (langcode == new FileInfo(langs[i]).Name.Replace(".ini", "")) { darkComboBox2.SelectedItem = lg; }
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
            langcode = Convert.ToString(cfg.GetValue("GUI", "LanguageCode"));
            try
            {
                IniEditor lang = new IniEditor(@Application.StartupPath + @"\files\languages\" + langcode + ".ini");
                // Text(GUI) loading
                darkLabel1.Text = Convert.ToString(lang.GetValue("Interface", "Languages"));
                HelloUser.Text = Convert.ToString(lang.GetValue("Interface", "Stage"));
                label2.Text = Convert.ToString(lang.GetValue("Interface", "OtherActions"));
                DSPanel.SectionHeader = Convert.ToString(lang.GetValue("Interface", "Tab1"));
                label1.Text = Convert.ToString(lang.GetValue("Interface", "PathLabel")) + ":";
                button6.Text = "1. " + DSPanel.SectionHeader;
                darkTitle1.Text = Convert.ToString(lang.GetValue("Interface", "CBG1"));
                darkTitle2.Text = Convert.ToString(lang.GetValue("Interface", "CBG2"));
                ModsPanel.SectionHeader = Convert.ToString(lang.GetValue("Interface", "Tab2"));
                button2.Text = "2. " + ModsPanel.SectionHeader;
                darkLabel9.Text = Convert.ToString(lang.GetValue("Interface", "List"));
                darkLabel2.Text = Convert.ToString(lang.GetValue("Interface", "ASILoader"));
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
                lc[28] = Convert.ToString(lang.GetValue("Interface", "DownloadingDirectXFiles"));
                lc[23] = Convert.ToString(lang.GetValue("Interface", "ModWord"));
                lc[37] = Convert.ToString(lang.GetValue("Interface", "WithASIL"));
                lc[41] = Convert.ToString(lang.GetValue("Interface", "Mbyte"));
                label3.Text = Convert.ToString(lang.GetValue("Interface", "WishPlay"));
                button3.Text = Convert.ToString(lang.GetValue("Interface", "Play"));
                darkButton5.Text = Convert.ToString(lang.GetValue("Interface", "CloseApp"));
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
                lc[27] = Convert.ToString(lang.GetValue("Title", "Request"));
                lc[0] = Convert.ToString(lang.GetValue("Title", "Info"));
                lc[1] = Convert.ToString(lang.GetValue("Title", "Error"));
                lc[8] = Convert.ToString(lang.GetValue("Title", "Warning"));
                lc[6] = Convert.ToString(lang.GetValue("Title", "FolderSelectDialog"));
                // InfoMsg loading
                lc[20] = Convert.ToString(lang.GetValue("InfoMsg", "WishDownloadPatches"));
                lc[22] = Convert.ToString(lang.GetValue("InfoMsg", "WishDownloadDirectXFiles"));
                lc[26] = Convert.ToString(lang.GetValue("InfoMsg", "InstallModQuestion"));
                lc[36] = Convert.ToString(lang.GetValue("InfoMsg", "WishDowngrader"));
                lc[24] = Convert.ToString(lang.GetValue("InfoMsg", "ModSucces"));
                lc[4] = Convert.ToString(lang.GetValue("InfoMsg", "Succes"));
                lc[34] = Convert.ToString(lang.GetValue("InfoMsg", "BindingOK"));
                lc[31] = Convert.ToString(lang.GetValue("InfoMsg", "ReturnUsingBackups"));
                lc[38] = Convert.ToString(lang.GetValue("InfoMsg", "App"));
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
                lc[32] = Convert.ToString(lang.GetValue("WarningMsg", "WishReturnUsingBackups"));
                lc[33] = Convert.ToString(lang.GetValue("WarningMsg", "WishRegGame"));
                lc[7] = Convert.ToString(lang.GetValue("WarningMsg", "PathNotFound"));
                lc[5] = Convert.ToString(lang.GetValue("WarningMsg", "BrowserNotFound"));
                lc[29] = Convert.ToString(lang.GetValue("WarningMsg", "NetworkNotFound"));
                lc[30] = Convert.ToString(lang.GetValue("WarningMsg", "OfflineMode"));
                lc[39] = Convert.ToString(lang.GetValue("WarningMsg", "NewPath"));
                lc[40] = Convert.ToString(lang.GetValue("WarningMsg", "YouCanDelete"));
                // Debug loading
                lc[12] = Convert.ToString(lang.GetValue("DebugMode", "Activation"));
                lc[13] = Convert.ToString(lang.GetValue("DebugMode", "Deactivation"));
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
                    if (!File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip"))
                    {
                        if (!Directory.Exists(cache + @"\zips")) { Directory.CreateDirectory(cache + @"\zips"); }
                        try
                        {
                            DialogResult result = DarkMessageBox.ShowInformation(lc[26], lc[27], DarkDialogButton.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                if (zip_link.Contains("mega.nz")) { MegaDownloader(zip_link, cache + @"\zips\" + @nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip", lc[17] + " \"" + @nameLabel.Text.Replace(lc[16] + ": ", "") + "\"...", 2); }
                                else
                                {
                                    TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Normal);
                                    progressPanel.Visible = true;
                                    stagesPanel.Visible = false;
                                    labelPartProgress.Text = lc[17] + " \"" + @nameLabel.Text.Replace(lc[16] + ": ", "") + "\"...";
                                    PartProgressBar.Value = 0;
                                    using (System.Net.WebClient wc = new System.Net.WebClient())
                                    {
                                        var r = wc.OpenRead(zip_link);
                                        labelPartProgress.Text += " (" + (Convert.ToDouble(wc.ResponseHeaders["Content-Length"]) / 1048576).ToString("#.# " + lc[41] + ")");
                                        r.Close();
                                        wc.DownloadProgressChanged += (s, a) => { TaskBarProgressBar.SetValue(this.Handle, a.ProgressPercentage, 100); PartProgressBar.Value = a.ProgressPercentage; };
                                        wc.DownloadFileCompleted += (s, a) =>
                                        {
                                            PartProgressBar.Value = 0;
                                            progressPanel.Visible = false;
                                            stagesPanel.Visible = true;
                                            TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress);
                                        };
                                        wc.DownloadFileAsync(new Uri(zip_link), @cache + @"\zips\" + @nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip");
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgError(lc[15]); MsgError(ex.ToString() + "\nFile: " + cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip"); TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); }
                    }
                    else { YesInstallMe.Checked = true; }
                }
                if (YesInstallMe.Checked == false)
                {
                    progressPanel.Visible = false;
                    stagesPanel.Visible = true;
                    if (File.Exists(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip")) { File.Delete(cache + @"\zips\" + nameLabel.Text.Replace(lc[16] + ": ", "") + ".zip"); }
                }
            }
            catch { MsgWarning(lc[29]); YesInstallMe.Checked = false; }
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
                        DialogResult result = DarkMessageBox.ShowInformation(lc[22], lc[27], DarkDialogButton.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            try { MegaDownloader("https://mega.nz/file/0pFRwAqa#Arguk9cQLpXYeQgXnFfAp6cw6F5OIZFKP2tRTwNCArI", @Application.StartupPath + @"\files\ddirectx.7z", lc[28], 1); }
                            catch { checkBox8.Checked = false; }
                        }
                        else { checkBox8.Checked = false; }
                    }
                    catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgWarning(lc[29]); checkBox8.Checked = false; TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); }
                }
            }
            cfg.SetValue("Downgrader", "InstallDirectXComponents", Convert.ToString(checkBox8.Checked).Replace("T", "t").Replace("F", "f"));
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F1) { try { Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/main/README.md#usage"); } catch { MsgError(lc[5]); } }
            if (e.KeyData == Keys.F4) { Process.Start("notepad.exe", @Application.StartupPath + @"\files\jpd.ini"); }
            if (e.KeyData == Keys.F12)
            {
                if (db == false) { MsgWarning(lc[12]); cfg.SetValue("JPD", "UseProgressBar", "false"); db = true; }
                else { MsgWarning(lc[13]); cfg.SetValue("JPD", "UseProgressBar", "true"); db = false; }
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
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Y))  {  if (GamePath.Text != "") { pictureBox11_Click(sender, e); } }
            if ((e.Modifiers == Keys.Control) && (e.KeyCode == Keys.Z))  {  if (IsBak == true) { pictureBox10_Click(sender, e); }  }
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
                    IniEditor lang = new IniEditor(langs[i]);
                    if (darkComboBox2.Text == Convert.ToString(lang.GetValue("Interface", "Language"))) { cfg.SetValue("GUI", "LanguageCode", new FileInfo(langs[i]).Name.Replace(".ini", "")); Translate(); }
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
                DialogResult result = DarkMessageBox.ShowInformation(lc[20], lc[27], DarkDialogButton.YesNo);
                if (result == DialogResult.Yes)
                {
                    try { MegaDownloader("https://mega.nz/file/4tcXiSqA#8JzulAC0oABzinb7914sq2xkyxE7c6atSeMval-YWms", @Application.StartupPath + @"\files\dpatches.rar", lc[21], 0); }
                    catch { progressPanel.Visible = false; stagesPanel.Visible = true; button1.Visible = false; }
                }
                else { progressPanel.Visible = false; stagesPanel.Visible = true; button1.Visible = false; }
            }
            catch { TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.Error); MsgWarning(lc[29]); TaskBarProgressBar.SetState(this.Handle, TaskBarProgressBar.TaskbarStates.NoProgress); }
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
            catch (Exception ex) { MsgError(lc[15]); MsgError(ex.ToString()); ScreenShot.Enabled = false; zip_link = "application/zip"; }
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
            DialogResult result = DarkMessageBox.ShowWarning(lc[32], lc[8], DarkDialogButton.YesNo);
            if (result == DialogResult.Yes)
            {
                originalGameRestoreProgressBar.Visible = true;
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
                                try { File.SetAttributes(@GamePath.Text + fl[i], FileAttributes.Normal); File.Delete(@GamePath.Text + fl[i]); }
                                catch { }
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
                    MsgInfo(lc[31]);
                    pictureBox10.Visible = false;
                    IsBak = false;
                    originalGameRestoreProgressBar.Visible = false;
                });
            }
        }

        void pictureBox11_Click(object sender, EventArgs e)
        {
            DialogResult result = DarkMessageBox.ShowWarning(lc[33], lc[8], DarkDialogButton.YesNo);
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
                MsgInfo(lc[34]);
            }
        }

       void progressPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (progressPanel.Visible == true) { HelloUser.Visible = false; pictureBox5.Visible = false; pictureBox6.Visible = false; pictureBox7.Visible = false; }
            else { HelloUser.Visible = true; pictureBox5.Visible = true; pictureBox6.Visible = true; pictureBox7.Visible = true; }
        }
    }
}