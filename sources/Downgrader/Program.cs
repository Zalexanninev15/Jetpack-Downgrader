using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;

namespace Downgrader
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        [StructLayout(LayoutKind.Sequential)]
        public struct DWM_BLURBEHIND { public DwmBlurBehindDwFlags dwFlags; public bool fEnable; public IntPtr hRgnBlur; public bool fTransitionOnMaximized; }
        [Flags()]
        public enum DwmBlurBehindDwFlags : uint { DWM_BB_ENABLE = 0x1, DWM_BB_BLURREGION = 0x2, DWM_BB_TRANSITIONONMAXIMIZED = 0x4 }
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

        static void EnableBlurBehind()
        {
            IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;
            DWM_BLURBEHIND blur = new DWM_BLURBEHIND();
            blur.dwFlags = DwmBlurBehindDwFlags.DWM_BB_ENABLE;
            blur.fEnable = true;
            blur.fTransitionOnMaximized = true;
            DwmEnableBlurBehindWindow(Handle, ref blur);
        }

        static void MakeTransparent(byte pct)
        {
            IntPtr Handle = Process.GetCurrentProcess().MainWindowHandle;
            int newDwLong = ((int)GetWindowLong(Handle, GWL_EXSTYLE)) ^ WS_EX_LAYERED;
            SetWindowLong(Handle, GWL_EXSTYLE, newDwLong);
            SetLayeredWindowAttributes(Handle, 0, pct, LWA_ALPHA);
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Console.ResetColor();
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
            string[] fl = new string[17]; string[] flmd5 = new string[17]; int er = 0, gv = 0; bool[] settings = new bool[18]; string path = ""; DialogResult result = DialogResult.No;
            // All files for downgrading (universal)
            fl[0] = @"\gta-sa.exe"; fl[1] = @"\gta_sa.exe"; fl[2] = @"\audio\CONFIG\TrakLkup.dat"; fl[3] = @"\audio\streams\BEATS";
            fl[4] = @"\audio\streams\CH"; fl[5] = @"\audio\streams\CR"; fl[6] = @"\audio\streams\CUTSCENE"; fl[7] = @"\audio\streams\DS";
            fl[8] = @"\audio\streams\MH"; fl[9] = @"\audio\streams\MR"; fl[10] = @"\audio\streams\RE"; fl[11] = @"\audio\streams\RG";
            fl[12] = @"\anim\anim.img"; fl[13] = @"\data\script\main.scm"; fl[14] = @"\data\script\script.img"; fl[15] = @"\models\gta_int.img";
            fl[16] = @"\models\gta3.img";
            // Original MD5 for files from game version 1.0
            flmd5[0] = "170B3A9108687B26DA2D8901C6948A18"; flmd5[1] = "E7697A085336F974A4A6102A51223960"; flmd5[2] = "528E75D663B8BAE072A01351081A2145"; flmd5[3] = "E26D86C7805D090D8210086876D6C35C";
            flmd5[4] = "FE31259226E0B4A8A963C70840E1FE8F"; flmd5[5] = "900148B8141EA4C1E782C3A48DBFBF3B"; flmd5[6] = "C25FCAA329B3D48F197FF4ED2A1D2A4D"; flmd5[7] = "9B4C18E4F3E82F0FEE41E30B2EA2246A";
            flmd5[8] = "909E7C4A7A29473E3885A96F987D7221"; flmd5[9] = "A1EC1CBE16DBB9F73022C6F33658ABE2"; flmd5[10] = "49B83551C684E17164F2047DCBA3E5AA"; flmd5[11] = "7491DC5325854C7117AF6E31900F38DD";
            flmd5[12] = "3359BA8CB820299161199EE7EF3F1C02"; flmd5[13] = "60AD23E272C3B0AA937053FE3006BE93"; flmd5[14] = "9598B82CF1E5AE7A8558057A01F6F2CE"; flmd5[15] = "DBE7E372D55914C39EB1D565E8707C8C";
            flmd5[16] = "9282E0DF8D7EEE3C4A49B44758DD694D";
            Console.Title = "Jetpack Downgrader";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[JPD] App: Jetpack Downgrader\n[JPD] Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Replace(".0", "") + "\n[JPD] License: MIT License\n[JPD] Authors: Zalexanninev15 (programmer and creator) & Vadim M. (consultant)\n[JPD] GitHub: https://github.com/Zalexanninev15/Jetpack-Downgrader\n");
            Console.ResetColor();
            try
            {
                if (File.Exists(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\jpd.ini") == false) { File.WriteAllText(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\jpd.ini", Properties.Resources.jpd); }
                IniLoader cfg = new IniLoader(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\jpd.ini");
                settings[2] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackups"));
                settings[6] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateShortcut"));
                settings[7] = Convert.ToBoolean(cfg.GetValue("Downgrader", "ResetGame"));
                settings[14] = Convert.ToBoolean(cfg.GetValue("Downgrader", "GarbageCleaning"));
                settings[9] = Convert.ToBoolean(cfg.GetValue("Downgrader", "RegisterGamePath"));
                settings[10] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateNewGamePath"));
                settings[12] = Convert.ToBoolean(cfg.GetValue("Downgrader", "Forced"));
                settings[16] = Convert.ToBoolean(cfg.GetValue("Downgrader", "EnableDirectPlay"));
                settings[17] = Convert.ToBoolean(cfg.GetValue("Downgrader", "InstallDirectXComponents"));
                settings[8] = Convert.ToBoolean(cfg.GetValue("JPD", "SelectFolder"));
                settings[11] = Convert.ToBoolean(cfg.GetValue("JPD", "ConsoleTransparency"));
                settings[13] = Convert.ToBoolean(cfg.GetValue("JPD", "UseMsg"));
                settings[15] = Convert.ToBoolean(cfg.GetValue("JPD", "UseProgressBar"));
                settings[1] = Convert.ToBoolean(cfg.GetValue("JPD", "Component"));
                settings[3] = Convert.ToBoolean(cfg.GetValue("Only", "GameVersion"));
                settings[4] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFiles"));
                settings[5] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFilesAndCheckMD5"));
                Logger("App", "jpd.ini", "true");
            }
            catch { Logger("App", "jpd.ini", "false"); }
            if (File.Exists(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patcher.exe"))
            {
                if (settings[11] == true) { EnableBlurBehind(); MakeTransparent(50); }
                if ((settings[1] == true) && (settings[8] == false))
                {
                    try { path = args[0]; } catch { }
                    if (Directory.Exists(@path) == false) { Logger("Game", "Path", "null"); }
                }
                if (settings[8] == true)
                {
                    var dialog = new FolderSelectDialog { InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), Title = "Select the game folder" };
                    if (dialog.Show()) { path = dialog.FileName; } else { path = "false"; }
                }
                if ((path != "") && Directory.Exists(@path))
                {
                    Logger("Game", "Path", "true");
                    //
                    // gv[number] - version
                    // 0 - 1.0
                    // 1 - Steam
                    // 2 - 2.0
                    // 3 - Rockstar Games Launcher
                    // 4 - Unknown
                    // 5 - Error
                    // 6 - 1.01
                    //
                    // Get version (EXE)
                    string SaEXE = @path + @"\gta-sa.exe";
                    Logger("GamePath", "Current", @path);
                    Logger("Downgrader", "Process", "Get version (EXE)...");
                    if (File.Exists(SaEXE))
                    {
                        try
                        {
                            string SteamEXEmd5 = GetMD5(SaEXE);
                            if (SteamEXEmd5 == "5BFD4DD83989A8264DE4B8E771F237FD") { gv = 1; Logger("Game", "Version", "Steam"); }
                            else
                            {
                                gv = 4;
                                SaEXE = @path + @"\gta_sa.exe";
                                try
                                {
                                    string OtherEXEmd5 = GetMD5(SaEXE);
                                    if (OtherEXEmd5 == "6687A315558935B3FC80CDBFF04437A4") { gv = 3; Logger("Game", "Version", "Rockstar Games Launcher"); }
                                    if ((OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373") || (OtherEXEmd5 == "4E99D762F44B1D5E7652DFA7E73D6B6F")) { gv = 2; Logger("Game", "Version", "2.0"); }
                                    if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373") && (OtherEXEmd5 != "4E99D762F44B1D5E7652DFA7E73D6B6F"))
                                    {
                                        if ((OtherEXEmd5 != "E7697A085336F974A4A6102A51223960") && (OtherEXEmd5 != "170B3A9108687B26DA2D8901C6948A18") && (OtherEXEmd5 != "91A9F6611ADDFB46682B56F9E247DB84") && (OtherEXEmd5 != "9369501599574D19AC93DE41547C4EC1"))
                                        {
                                            if ((OtherEXEmd5 != "A2929A61E4D63DD3C15749B2B7ED74AE") && (OtherEXEmd5 != "25405921D1C47747FD01FD0BFE0A05AE")) { gv = 4; Logger("Game", "Version", "Unknown [NOT SUPPORTED]"); }
                                            else { gv = 6; Logger("Game", "Version", "1.01"); }
                                        }
                                        else
                                        {
                                            gv = 0;
                                            Logger("Game", "Version", "1.0");
                                            if (settings[13] == true) { result = MessageBox.Show("Would you like to forcibly downgrade the EXE file to version 1.0? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                            if ((result == DialogResult.Yes) || (settings[12] == true)) { settings[12] = true; }
                                        }
                                    }
                                }
                                catch { gv = 4; Logger("Game", "Version", "Unknown [NOT SUPPORTED]"); }
                            }
                        }
                        catch { gv = 5; er = 1; Logger("Game", "Version", "Unknown [ERROR]"); }
                    }
                    else
                    {
                        SaEXE = @path + @"\gta_sa.exe";
                        try
                        {
                            string OtherEXEmd5 = GetMD5(SaEXE);
                            if (OtherEXEmd5 == "5BFD4DD83989A8264DE4B8E771F237FD") { gv = 1; Logger("Game", "Version", "Steam"); }
                            else
                            {
                                gv = 4;
                                if (OtherEXEmd5 == "6687A315558935B3FC80CDBFF04437A4") { gv = 3; Logger("Game", "Version", "Rockstar Games Launcher"); }
                                if ((OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373") || (OtherEXEmd5 == "4E99D762F44B1D5E7652DFA7E73D6B6F")) { gv = 2; Logger("Game", "Version", "2.0"); }
                                if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373") && (OtherEXEmd5 != "4E99D762F44B1D5E7652DFA7E73D6B6F"))
                                {
                                    if ((OtherEXEmd5 != "E7697A085336F974A4A6102A51223960") && (OtherEXEmd5 != "170B3A9108687B26DA2D8901C6948A18") && (OtherEXEmd5 != "91A9F6611ADDFB46682B56F9E247DB84") && (OtherEXEmd5 != "9369501599574D19AC93DE41547C4EC1"))
                                    {
                                        if ((OtherEXEmd5 != "A2929A61E4D63DD3C15749B2B7ED74AE") && (OtherEXEmd5 != "25405921D1C47747FD01FD0BFE0A05AE")) { gv = 4; Logger("Game", "Version", "Unknown [NOT SUPPORTED]"); }
                                        else { gv = 6; Logger("Game", "Version", "1.01"); }
                                    }
                                    else
                                    {
                                        gv = 0;
                                        Logger("Game", "Version", "1.0");
                                        if (settings[13] == true) { result = MessageBox.Show("Would you like to forcibly downgrade the EXE file to version 1.0? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                        if ((result == DialogResult.Yes) || (settings[12] == true)) { settings[12] = true; }
                                    }
                                }
                            }
                        }
                        catch { gv = 5; er = 1; Logger("Game", "Version", "Unknown [ERROR]"); }
                    }
                    if ((gv == 4) || (gv == 5)) { Logger("Downgrader", "Process", "Downgrade is not possible!"); }
                    if ((File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set")) || (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.CommonDocuments) + @"\GTA San Andreas User Files\gta_sa.set")))
                    {
                        if ((settings[13] == true) && (gv != 4) && (gv != 5)) { result = MessageBox.Show("Would you like to reset the game settings to prevent possible difficulties in starting the game?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                        if (((result == DialogResult.Yes) || (settings[7] == true)) && (gv != 4) && (gv != 5))
                        {
                            Logger("Downgrader", "Process", "Deleting gta_sa.set (Documents) file...");
                            try
                            {
                                if (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set"))
                                {
                                    File.Delete(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set");
                                    Logger("ResetGame", "gta_sa.set (Documents)", "true");
                                }
                                else { Logger("ResetGame", "gta_sa.set (Documents)", "false"); }
                            }
                            catch { Logger("ResetGame", "gta_sa.set (Documents)", "false"); }
                            Logger("Downgrader", "Process", "Deleting gta_sa.set (Public Documents) file...");
                            try
                            {
                                if (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.CommonDocuments) + @"\GTA San Andreas User Files\gta_sa.set"))
                                {
                                    File.Delete(@Environment.GetFolderPath(@Environment.SpecialFolder.CommonDocuments) + @"\GTA San Andreas User Files\gta_sa.set");
                                    Logger("ResetGame", "gta_sa.set (Public Documents)", "true");
                                }
                                else { Logger("ResetGame", "gta_sa.set (Public Documents)", "false"); }
                            }
                            catch { Logger("ResetGame", "gta_sa.set (Public Documents)", "false"); }
                        }
                    }
                    if ((settings[13] == true) && (gv != 5)) { result = MessageBox.Show("Would you like to enable DirectPlay to avoid possible problems with running the game? This operation is NECESSARY ONLY on Windows 10, if your version is lower (7/8/8.1), then your answer is No!!!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                    if (((result == DialogResult.Yes) || (settings[16] == true)) && (gv != 5))
                    {
                        Logger("DirectPlay", "Enabled", "false");
                        Logger("DirectPlay", "Enabled", "In process...");
                        try { Process.Start("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart").WaitForExit(); } catch { Logger("DirectPlay", "Enabled", "Error 1"); }
                        try { Process.Start("dism", "/Online /enable-feature /FeatureName:\"DirectPlay\" /NoRestart /all").WaitForExit(); } catch { Logger("DirectPlay", "Enabled", "Error 2"); }
                        Logger("DirectPlay", "Enabled", "true");
                        Logger("DirectPlay", "Guide if DirectPlay not work", "https://docs.microsoft.com/en-us/answers/questions/108291/enable-windows-10-direct-play.html?childToView=111216#answer-111216");
                    }
                    if ((settings[13] == true) && (gv != 5)) { result = MessageBox.Show("Would you like to install DirectX 9.0c files to avoid possible problems with running the game?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                    if (((result == DialogResult.Yes) || (settings[17] == true)) && (gv != 5))
                    {
                        if (Directory.Exists(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\DirectX"))
                        {
                            try
                            {
                                Logger("DirectX", "Process", "Installing...");
                                Logger("DirectX", "Process", "App is not frozen, just busy right now...");
                                Process.Start(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\DirectX\DXSETUP.exe", "/silent").WaitForExit();
                                Logger("DirectX", "Process", "Installation completed successfully");
                            }
                            catch { Logger("DirectX", "Process", "Installation error"); }
                        }
                        else { Logger("DirectX", "Process", "Installation error"); }
                    }
                    if ((settings[12] == true) && (gv == 0)) { gv = 6; settings[12] = true; }
                    if ((gv != 0) && (er == 0) && (settings[3] == false))
                    {
                        // Check files
                        Logger("Downgrader", "Process", "Scanning files...");
                        if ((gv == 6) || (gv == 0)) // 1.01
                        {
                            if (File.Exists(@path + fl[1]))
                            {
                                Logger("Game", @path + fl[1], "true");
                                File.SetAttributes(@path + fl[1], FileAttributes.Normal);
                                try { File.SetAttributes(@path + fl[1] + ".jpb", FileAttributes.Normal); } catch { }
                            }
                            else { er = 1; Logger("Game", @path + fl[1], "false"); }
                        }
                        if (gv == 3) // Rockstar Games Launcher
                        {
                            using (var progress = new ProgressBar())
                            {
                                for (int i = 1; i < fl.Length; i++)
                                {
                                    if (File.Exists(@path + fl[i]))
                                    {
                                        File.SetAttributes(@path + fl[i], FileAttributes.Normal);
                                        try { File.SetAttributes(@path + fl[i] + ".jpb", FileAttributes.Normal); } catch { }
                                        if (settings[15] == false) { progress.DoThis(false); Logger("Game", @path + fl[i], "true"); }
                                    }
                                    else { er = 1; if (settings[15] == false) { progress.DoThis(false); Logger("Game", @path + fl[i], "false"); } }
                                    if (settings[15] == true) { progress.DoText("Checking files progress"); progress.Report((double)i / fl.Length); }
                                }
                            }
                        }
                        if (gv == 2) // Version 2.0
                        {
                            using (var progress = new ProgressBar())
                            {
                                for (int i = 1; i < fl.Length; i++)
                                {
                                    if ((i >= 1) && (i > 11))
                                    {
                                        if (File.Exists(@path + fl[i]))
                                        {
                                            File.SetAttributes(@path + fl[i], FileAttributes.Normal);
                                            try { File.SetAttributes(@path + fl[i] + ".jpb", FileAttributes.Normal); } catch { }
                                            if (settings[15] == false) { progress.DoThis(false); Logger("Game", @path + fl[i], "true"); }
                                        }
                                        else { er = 1; if (settings[15] == false) { progress.DoThis(false); Logger("Game", @path + fl[i], "false"); } }
                                    }
                                    if (settings[15] == true) { progress.DoText("Checking files progress"); progress.Report((double)i / fl.Length); }
                                }
                            }
                        }
                        if (gv == 1) // Steam
                        {
                            using (var progress = new ProgressBar())
                            {
                                for (int i = 0; i < fl.Length; i++)
                                {
                                    if (i != 1)
                                    {
                                        if (File.Exists(@path + fl[i]))
                                        {
                                            File.SetAttributes(@path + fl[i], FileAttributes.Normal);
                                            try { File.SetAttributes(@path + fl[i] + ".jpb", FileAttributes.Normal); } catch { }
                                            if (settings[15] == false) { progress.DoThis(false); Logger("Game", @path + fl[i], "true"); }
                                        }
                                        else
                                        {
                                            er = 1;
                                            if (settings[15] == false) { progress.DoThis(false); Logger("Game", @path + fl[i], "false"); }
                                        }
                                    }
                                    if (settings[15] == true) { progress.DoText("Checking files progress"); progress.Report((double)i / fl.Length); }
                                }
                            }
                        }
                        bool fisv = false;
                        if ((er == 0) && (settings[4] == false))
                        {
                            // Checking files before downgrade (MD5)
                            string GameMD5 = "";
                            Logger("Game", "All", "true");
                            Logger("Downgrader", "Process", "Checking original files before downgrade (MD5)...");
                            if (gv == 3) // Rockstar Games Launcher
                            {
                                using (var progress = new ProgressBar())
                                {
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        try
                                        {
                                            GameMD5 = GetMD5(@path + fl[i]);
                                            if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], GameMD5); }
                                            if (GameMD5 == flmd5[i])
                                            {
                                                fisv = true;
                                                if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "1.0"); }
                                            }
                                            else { if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "Higher than 1.0!"); } }
                                        }
                                        catch { fisv = true; if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "File not found!"); } }
                                        if (settings[15] == true) { progress.DoText("MD5 verification progress"); progress.Report((double)i / fl.Length); }
                                    }
                                }
                            }
                            if (gv == 2) // Version 2.0
                            {
                                using (var progress = new ProgressBar())
                                {
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if ((i >= 2) && (i > 11))
                                        {
                                            try
                                            {
                                                GameMD5 = GetMD5(@path + fl[i]);
                                                if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], GameMD5); }
                                                if (GameMD5 == flmd5[i])
                                                {
                                                    fisv = true;
                                                    if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "1.0"); }
                                                }
                                                else { if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "Higher than 1.0!"); } }
                                            }
                                            catch { fisv = true; if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "File not found!"); } }
                                        }
                                        if (settings[15] == true) { progress.DoText("MD5 verification progress"); progress.Report((double)i / fl.Length); }
                                    }
                                }
                            }
                            if (gv == 1) // Steam version
                            {
                                using (var progress = new ProgressBar())
                                {
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if (i >= 2)
                                        {
                                            try
                                            {
                                                GameMD5 = GetMD5(@path + fl[i]);
                                                if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], GameMD5); }
                                                if (GameMD5 == flmd5[i])
                                                {
                                                    fisv = true;
                                                    if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "1.0"); }
                                                }
                                                else { if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "Higher than 1.0!"); } }
                                            }
                                            catch { fisv = true; if (settings[15] == false) { progress.DoThis(false); Logger("GameMD5", @path + fl[i], "File not found!"); } }
                                        }
                                        if (settings[15] == true) { progress.DoText("MD5 verification progress"); progress.Report((double)i / fl.Length); }
                                    }
                                }
                            }
                            if (settings[12] == true)
                            {
                                Logger("Downgrader", "Process", "Forced downgrade mode is used...");
                                fisv = false;
                                gv = 6;
                            }
                            if ((fisv == false) && (settings[5] == false))
                            {
                                if ((settings[13] == true) && ((gv == 1) || (gv == 3))) { result = MessageBox.Show("Would you like to create a copy of the game folder to prevent accidental updates to the game?\nIf you have a game from Steam/Rockstar Games Launcher - we strongly recommend choosing Yes!!!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                if (((result == DialogResult.Yes) || (settings[10] == true)) && ((gv == 1) || (gv == 3)))
                                {
                                    settings[10] = true;
                                    Logger("Downgrader", "Process", "Copying the game folder before downgrading...");
                                    Logger("Downgrader", "Process", "App is not frozen, just busy right now...");
                                    try
                                    {
                                        try { Directory.Delete(@path + "_Downgraded", true); } catch { }
                                        FileSystem.CopyDirectory(@path, @path + "_Downgraded");
                                        path = @path + "_Downgraded";
                                        Logger("NewGamePath", "Path", @path);
                                    }
                                    catch { er = 0; Logger("NewGamePath", "Path", "false"); }
                                }
                                // Backup (optional)
                                if (settings[13] == true) { result = MessageBox.Show("Do you want to create backups of files?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                if ((result == DialogResult.Yes) || (settings[2] == true))
                                {
                                    settings[2] = true;
                                    Logger("Downgrader", "Process", "Create backups...");
                                    if (gv == 6) // 1.01
                                    {
                                        if (File.Exists(@path + fl[1] + ".jpb")) { File.Delete(@path + fl[1] + ".jpb"); }
                                        try { File.Move(@path + fl[1], @path + fl[1] + ".jpb"); Logger("GameBackup", @path + fl[1], "Done!"); }
                                        catch { er = 1; Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); }
                                    }
                                    if (gv == 3) // Rockstar Games Launcher
                                    {
                                        if (File.Exists(@path + fl[1] + ".jpb")) { File.Delete(@path + fl[1] + ".jpb"); }
                                        try { File.Move(@path + fl[1], @path + fl[1] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "Done!"); } }
                                        catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); } }
                                        using (var progress = new ProgressBar())
                                        {
                                            for (int i = 2; i < fl.Length; i++)
                                            {
                                                if (File.Exists(@path + fl[i] + ".jpb")) { File.Delete(@path + fl[i] + ".jpb"); }
                                                try { File.Move(@path + fl[i], @path + fl[i] + ".jpb"); if (settings[15] == false) { progress.DoThis(false); Logger("GameBackup", @path + fl[i], "Done!"); } }
                                                catch { er = 1; if (settings[15] == false) { progress.DoThis(false); Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); } }
                                                if (settings[15] == true) { progress.DoText("Backup progress"); progress.Report((double)i / fl.Length); }
                                            }
                                        }
                                    }
                                    if (gv == 2) // Version 2.0
                                    {
                                        if (File.Exists(@path + fl[1] + ".jpb")) { File.Delete(@path + fl[1] + ".jpb"); }
                                        try { File.Move(@path + fl[1], @path + fl[1] + ".jpb"); if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "Done!"); } }
                                        catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); } }
                                        using (var progress = new ProgressBar())
                                        {
                                            for (int i = 2; i < fl.Length; i++)
                                            {
                                                if ((i >= 2) && (i > 11))
                                                {
                                                    if (File.Exists(@path + fl[i] + ".jpb")) { File.Delete(@path + fl[i] + ".jpb"); }
                                                    try { File.Move(@path + fl[i], @path + fl[i] + ".jpb"); if (settings[15] == false) { progress.DoThis(false); Logger("GameBackup", @path + fl[i], "Done!"); } }
                                                    catch { er = 1; if (settings[15] == false) { progress.DoThis(false); Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); } }
                                                }
                                                if (settings[15] == true) { progress.DoText("Backup progress"); progress.Report((double)i / fl.Length); }
                                            }
                                        }
                                    }
                                    if (gv == 1) // Steam version
                                    {
                                        if (File.Exists(@path + fl[0] + ".jpb")) { File.Delete(@path + fl[0] + ".jpb"); }
                                        try
                                        {
                                            File.Move(@path + fl[0], @path + fl[0] + ".jpb");
                                            if (settings[15] == false) { Logger("GameBackup", @path + fl[0], "Done!"); }
                                        }
                                        catch { er = 1; if (settings[15] == false) { Logger("GameBackup", @path + fl[0], "File for backup wasn't found!"); } }
                                        using (var progress = new ProgressBar())
                                        {
                                            for (int i = 2; i < fl.Length; i++)
                                            {
                                                if (i >= 2)
                                                {
                                                    if (File.Exists(@path + fl[i] + ".jpb")) { File.Delete(@path + fl[i] + ".jpb"); }
                                                    try { File.Move(@path + fl[i], @path + fl[i] + ".jpb"); if (settings[15] == false) { progress.DoThis(false); Logger("GameBackup", @path + fl[i], "Done!"); } }
                                                    catch { er = 1; if (settings[15] == false) { progress.DoThis(false); Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); } }
                                                }
                                                if (settings[15] == true) { progress.DoText("Backup progress"); progress.Report((double)i / fl.Length); }
                                            }
                                        }
                                    }
                                    if (er == 0) { Logger("GameBackup", "All", "true"); }
                                }
                                if (er == 0)
                                {
                                    if (Directory.Exists(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches"))
                                    {
                                        bool all_patches = true;
                                        for (int i = 2; i < fl.Length; i++) { if (!File.Exists(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches" + fl[i] + ".jpp")) { all_patches = false; } }
                                        if (!File.Exists(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches\game.jpp")) { all_patches = false; }
                                        if (all_patches == true)
                                        {
                                            Logger("Downgrader", "Process", "Downgrading...");
                                            try
                                            {
                                                // For All Versions | EXE
                                                var restoreRGLfiles = new ProcessStartInfo
                                                {
                                                    FileName = @Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\7z.exe",
                                                    Arguments = "x \"" + @Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\patches\\game.jpp\" -o\"" + @path + "\" -y",
                                                    UseShellExecute = false,
                                                    CreateNoWindow = true,
                                                };
                                                restoreRGLfiles.WindowStyle = ProcessWindowStyle.Hidden;
                                                Process.Start(restoreRGLfiles).WaitForExit();
                                                try { File.SetAttributes(@path + fl[1], FileAttributes.Normal); } catch { }
                                                try { File.SetAttributes(@path + @"\game.jpp", FileAttributes.Normal); } catch { }
                                                try { File.Delete(@path + fl[1]); } catch { }
                                                File.Move(@path + @"\game.jpp", @path + fl[1]);
                                                if (settings[15] == false) { Logger("NewGame", @path + fl[1], "1.0"); }
                                                if (gv == 1)
                                                {
                                                    Process.Start(restoreRGLfiles).WaitForExit();
                                                    try { File.SetAttributes(@path + fl[0], FileAttributes.Normal); } catch { }
                                                    try { File.SetAttributes(@path + @"\game.jpp", FileAttributes.Normal); } catch { }
                                                    try { File.Delete(@path + fl[0]); } catch { }
                                                    File.Move(@path + @"\game.jpp", @path + fl[0]);
                                                    if (settings[15] == false) { Logger("NewGame", @path + fl[0], "1.0"); }
                                                }
                                                if ((gv == 3) || (gv == 1))  // Rockstar Games Launcher & Steam
                                                {
                                                    using (var progress = new ProgressBar())
                                                    {
                                                        for (int i = 2; i < fl.Length; i++)
                                                        {
                                                            // Old: par = " -d -s " + '"' ...
                                                            string par = '"' + @path + fl[i] + '"' + " " + '"' + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"';
                                                            if (settings[2] == true) { par = '"' + @path + fl[i] + ".jpb" + '"' + " " + '"' + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"'; }
                                                            Patcher(@par);
                                                            if (settings[2] == false) { File.Delete(@path + fl[i]); }
                                                            File.Move(@path + fl[i] + ".temp", @path + fl[i]);
                                                            if (settings[15] == false) { progress.DoThis(false); Logger("NewGame", @path + fl[i], "1.0"); }
                                                            if (settings[15] == true) { progress.DoText("Downgrade progress"); progress.Report((double)i / fl.Length); }
                                                        }
                                                    }
                                                }
                                                if (gv == 2) // 2.0
                                                {
                                                    using (var progress = new ProgressBar())
                                                    {
                                                        for (int i = 2; i < fl.Length; i++)
                                                        {
                                                            if ((i >= 2) && (i > 11))
                                                            {
                                                                string par = '"' + @path + fl[i] + '"' + " " + '"' + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"';
                                                                if (settings[2] == true) { par = '"' + @path + fl[i] + ".jpb" + '"' + " " + '"' + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches" + fl[i] + ".jpp" + '"' + " " + '"' + path + fl[i] + ".temp" + '"'; }
                                                                Patcher(@par);
                                                                if (settings[2] == false) { File.Delete(@path + fl[i]); }
                                                                File.Move(@path + fl[i] + ".temp", @path + fl[i]);
                                                                if (settings[15] == false) { progress.DoThis(false); Logger("NewGame", @path + fl[i], "1.0"); }
                                                            }
                                                            if (settings[15] == true) { progress.DoText("Downgrade progress"); progress.Report((double)i / fl.Length); }
                                                        }
                                                    }
                                                }
                                            }
                                            catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game files!"); }
                                            if (er == 0)
                                            {
                                                Logger("NewGame", "All", "1.0");
                                                if (settings[0] == true) { Logger("NewGameReadOnly", "All", "true"); }
                                                // Checking files after downgrade (MD5)
                                                Logger("Downgrader", "Process", "Checking files after downgrade (MD5)...");
                                                if (gv == 6) // 1.01
                                                {
                                                    try
                                                    {
                                                        GameMD5 = GetMD5(@path + fl[1]);
                                                        Logger("NewGameMD5", @path + fl[1], GameMD5);
                                                        if (GameMD5 == flmd5[0]) { fisv = true; Logger("NewGameMD5", @path + fl[1], "1.0"); }
                                                        else { fisv = false; Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); }
                                                    }
                                                    catch { fisv = false; Logger("NewGameMD5", @path + fl[1], "File not found!"); }
                                                }
                                                if (gv == 3) // Rockstar Games Launcher
                                                {
                                                    try
                                                    {
                                                        GameMD5 = GetMD5(@path + fl[1]);
                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], GameMD5); }
                                                        if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "1.0"); } }
                                                        else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); } }
                                                    }
                                                    catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "File not found!"); } }
                                                    using (var progress = new ProgressBar())
                                                    {
                                                        for (int i = 2; i < fl.Length; i++)
                                                        {
                                                            try
                                                            {
                                                                GameMD5 = GetMD5(@path + fl[i]);
                                                                if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], GameMD5); }
                                                                if (GameMD5 == flmd5[i])
                                                                {
                                                                    fisv = true;
                                                                    if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "1.0"); }
                                                                }
                                                                else
                                                                {
                                                                    fisv = false;
                                                                    if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "Higher than 1.0!"); }
                                                                }
                                                            }
                                                            catch { fisv = false; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "File not found!"); } }
                                                            if (settings[15] == true) { progress.DoText("MD5 verification progress"); progress.Report((double)i / fl.Length); }
                                                        }
                                                    }
                                                }
                                                if (gv == 2) // 2.0
                                                {
                                                    try
                                                    {
                                                        GameMD5 = GetMD5(@path + fl[1]);
                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], GameMD5); }
                                                        if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "1.0"); } }
                                                        else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); } }
                                                    }
                                                    catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "File not found!"); } }
                                                    using (var progress = new ProgressBar())
                                                    {
                                                        for (int i = 2; i < fl.Length; i++)
                                                        {
                                                            if ((i >= 2) && (i > 11))
                                                            {
                                                                try
                                                                {
                                                                    GameMD5 = GetMD5(@path + fl[i]);
                                                                    if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], GameMD5); }
                                                                    if (GameMD5 == flmd5[i]) { fisv = true; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "1.0"); } }
                                                                    else { fisv = false; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "Higher than 1.0!"); } }
                                                                }
                                                                catch { fisv = false; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "File not found!"); } }
                                                            }
                                                            if (settings[15] == true) { progress.DoText("MD5 verification progress"); progress.Report((double)i / fl.Length); }
                                                        }
                                                    }
                                                }
                                                if (gv == 1) // Steam
                                                {
                                                    try
                                                    {
                                                        GameMD5 = GetMD5(@path + fl[0]);
                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], GameMD5); }
                                                        if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], "1.0"); } }
                                                        else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], "Higher than 1.0!"); } }
                                                    }
                                                    catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[0], "File not found!"); } }
                                                    try
                                                    {
                                                        GameMD5 = GetMD5(@path + fl[1]);
                                                        if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], GameMD5); }
                                                        if (GameMD5 == flmd5[0]) { fisv = true; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "1.0"); } }
                                                        else { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "Higher than 1.0!"); } }
                                                    }
                                                    catch { fisv = false; if (settings[15] == false) { Logger("NewGameMD5", @path + fl[1], "File not found!"); } }
                                                    using (var progress = new ProgressBar())
                                                    {
                                                        for (int i = 2; i < fl.Length; i++)
                                                        {
                                                            try
                                                            {
                                                                GameMD5 = GetMD5(@path + fl[i]);
                                                                if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], GameMD5); }
                                                                if (GameMD5 == flmd5[i]) { fisv = true; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "1.0"); } }
                                                                else { fisv = false; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "Higher than 1.0!"); } }
                                                            }
                                                            catch { fisv = false; if (settings[15] == false) { progress.DoThis(false); Logger("NewGameMD5", @path + fl[i], "File not found!"); } }
                                                            if (settings[15] == true) { progress.DoText("MD5 verification progress"); progress.Report((double)i / fl.Length); }
                                                        }
                                                    }
                                                }
                                                if (fisv == true)
                                                {
                                                    Logger("NewGameMD5", "All", "true");
                                                    Logger("Downgrader", "Game", "Downgrade completed!");
                                                    if (File.Exists(@path + @"\index.bin") || File.Exists(@path + @"\MTLX.dll"))
                                                    {
                                                        if ((settings[13] == true) && (gv == 3)) { result = MessageBox.Show("Do you want remove unneeded files that are not used by the game version 1.0?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                                        if (((result == DialogResult.Yes) || (settings[14] == true)) && (gv == 3))
                                                        {
                                                            Logger("Downgrader", "Process", "Deleting index.bin file...");
                                                            try
                                                            {
                                                                if (File.Exists(@path + @"\index.bin")) { File.Delete(@path + @"\index.bin"); Logger("GarbageCleaning", "index.bin", "true"); }
                                                                else { Logger("GarbageCleaning", "index.bin", "false"); }
                                                            }
                                                            catch { Logger("GarbageCleaning", "index.bin", "false"); }
                                                            Logger("Downgrader", "Process", "Deleting MTLX.dll file...");
                                                            try
                                                            {
                                                                if (File.Exists(@path + @"\MTLX.dll")) { File.Delete(@path + @"\MTLX.dll"); Logger("GarbageCleaning", "MTLX.dll", "true"); }
                                                                else { Logger("GarbageCleaning", "MTLX.dll", "false"); }
                                                            }
                                                            catch { Logger("GarbageCleaning", "MTLX.dll", "false"); }
                                                        }
                                                    }
                                                    if (settings[13] == true) { result = MessageBox.Show("Would you like to create an game shortcut on your Desktop?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                                    if ((result == DialogResult.Yes) || (settings[6] == true))
                                                    {
                                                        Logger("Downgrader", "Process", "Creating a shortcut...");
                                                        try { Create(@Environment.GetFolderPath(@Environment.SpecialFolder.Desktop) + @"\GTA San Andreas 1.0.lnk", @path + @"\gta_sa.exe"); Logger("Downgrader", "CreateShortcut", "true"); }
                                                        catch { Logger("Downgrader", "CreateShortcut", "false"); }
                                                    }
                                                    if (settings[13] == true) { result = MessageBox.Show("Would you like to register the game in the system?\n(for launchers, SAMP and other projects)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                                                    if ((result == DialogResult.Yes) || (settings[9] == true))
                                                    {
                                                        Logger("Downgrader", "Process", "Adding entries to the registry...");
                                                        try
                                                        {
                                                            if (Environment.Is64BitOperatingSystem == true) { Registry.LocalMachine.CreateSubKey(@"SOFTWARE\WOW6432Node\Rockstar Games\GTA San Andreas\Installation"); Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Rockstar Games\GTA San Andreas\Installation", "ExePath", "\"" + path + "\""); }
                                                            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rockstar Games\GTA San Andreas\Installation");
                                                            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\GTA San Andreas\Installation", "ExePath", "\"" + path + "\"");
                                                            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\GTA San Andreas\Installation", "Installed", "1");
                                                            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rockstar Games\Launcher");
                                                            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Rockstar Games\Launcher", "Language", "en-US");
                                                            Logger("Downgrader", "RegisterGamePath", "true");
                                                        }
                                                        catch { Logger("Downgrader", "RegisterGamePath", "false"); }
                                                    }
                                                    if (settings[13] == true) { MessageBox.Show("Downgrade completed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                                }
                                                else { Logger("NewGameMD5", "All", "false"); Logger("Downgrader", "Game", "Error checking files!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); }
                                            }
                                        }
                                        else { Logger("Downgrader", "NewGame", "Please make sure that you have downloaded the patches, otherwise, the downgrader will not be able to start its work!"); }
                                    }
                                    else { Logger("Downgrader", "NewGame", "Please make sure that you have downloaded the patches, otherwise, the downgrader will not be able to start its work!"); }
                                }
                                else { Logger("GameBackup", "All", "Some game files were not found, so it is not possible to continue working!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); }
                            }
                            else { if (settings[5] == false) { Logger("GameMD5", "All", "It is impossible to determine exactly which version some files are taken from, because some of them have 1.0, and others are Higher than 1.0!!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); } }
                        }
                        else { if (settings[4] == false) { Logger("Game", "All", "Some game files were not found, so it is not possible to continue working!"); Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!"); } }
                    }
                    if (gv == 0) { Logger("Downgrader", "Process", "Downgrade is not required!"); }
                }
                else { Logger("Game", "Path", "false"); }
            }
            else { Logger("Downgrader", "Process", "File patcher.exe was not found!"); }
            bool IsGUI = false;
            string str = "app";
            foreach (Process process2 in Process.GetProcesses()) { if (process2.ProcessName.ToLower().Contains(str.ToLower())) { IsGUI = true; } }
            if ((settings[1] == false) || (IsGUI == true)) { Logger("GamePath", "Current", @path); Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Press Enter to Exit"); Console.ResetColor(); Console.ReadLine(); }
        }

        static void Patcher(string argument)
        {
            Process start_info = new Process();
            start_info.StartInfo.FileName = @Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patcher.exe";
            start_info.StartInfo.Arguments = @argument;
            start_info.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            start_info.StartInfo.CreateNoWindow = true;
            start_info.StartInfo.UseShellExecute = false;
            start_info.Start();
            start_info.WaitForExit();
        }

        static void Logger(string type, string ido, string status)
        {
            if ((type == "NewGameMD5") || ((type == "GamePath") && (ido == "Current"))) { Console.ForegroundColor = ConsoleColor.Yellow; }
            if ((type == "NewGamePath") || (status == "Forced downgrade mode is used...") || (status == "Installation completed successfully") || (status == "1.0") || (status == "true") || (status == "Downgrade completed!") || (status == "Done!")) { Console.ForegroundColor = ConsoleColor.Green; }
            if ((status == "Deleting MTLX.dll file...") || (status == "Deleting index.bin file...") || (status == "Deleting gta_sa.set (Public Documents) file...") || (status == "Deleting gta_sa.set (Documents) file...") || (status == "Adding entries to the registry...") || (status == "Creating a shortcut...") || (status == "Checking files after downgrade (MD5)...") || (status == "Downgrading...") || (status == "Create backups...") || (status == "Checking original files before downgrade (MD5)...") || (status == "Scanning files...") || (status == "Get version (EXE)...") || (status == "Copying the game folder before downgrading...") || (status == "App is not frozen, just busy right now...") || (status == "Downloading installer...") || (status == "Installing...") || (status == "In process...") || (status == "Preparing installer...")) { Console.ForegroundColor = ConsoleColor.Blue; }
            if (((type == "GameMD5") && (status != "Higher than 1.0!") && (status != "1.0")) || (ido == "Guide if DirectPlay not work") || ((type == "GamePath") && (ido == "new")) || (status == "Rockstar Games Launcher") || (status == "Steam") || (status == "1.01") || (status == "2.0")) { Console.ForegroundColor = ConsoleColor.Yellow; }
            if ((status == "Installation error") || ((type == "Game") && (ido == "Path") && (status == "null")) || (status == "Please make sure that you have downloaded the patches (patches folder), otherwise, the downgrader will not be able to start its work!") || (status == "File patcher.exe was not found!") || (status == "File not found!") || (status == "Higher than 1.0!") || (status == "Unknown [NOT SUPPORTED]") || (status == "Unknown [ERROR]") || (status == "false") || (status == "File for backup wasn't found!") || (status == "Please make sure that you have downloaded the patches, otherwise, the downgrader will not be able to start its work!") || (status == "Downgrade is not possible!") || (status == "An error occurred accessing the game files!")) { Console.ForegroundColor = ConsoleColor.Red; }
            if (((type == "GameMD5") && (status == "Higher than 1.0!")) || (status == "Downgrade is not required!")) { Console.ForegroundColor = ConsoleColor.Green; }
            Console.WriteLine("[" + type + "] " + ido + "=" + status);
            Console.ResetColor();
        }

        static void Create(string ShortcutPath, string TargetPath)
        {
            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutPath);
            Shortcut.TargetPath = TargetPath;
            Shortcut.WorkingDirectory = TargetPath.Replace(@"\gta_sa.exe", "");
            Shortcut.Save();
        }

        static string GetMD5(string file)
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
    }
}