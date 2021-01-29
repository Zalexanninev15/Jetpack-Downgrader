using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace SA_Downgrader_RW2
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try { File.Delete(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache.exe"); } catch { }
            string[] fl = new string[17];
            string[] flmd5 = new string[17];

            // A list of files:
            fl[0] = @"\gta-sa.exe"; fl[1] = @"\gta_sa.exe"; fl[2] = @"\audio\CONFIG\TrakLkup.dat"; fl[3] = @"\audio\streams\BEATS";
            fl[4] = @"\audio\streams\CH"; fl[5] = @"\audio\streams\CR"; fl[6] = @"\audio\streams\CUTSCENE"; fl[7] = @"\audio\streams\DS";
            fl[8] = @"\audio\streams\MH"; fl[9] = @"\audio\streams\MR"; fl[10] = @"\audio\streams\RE"; fl[11] = @"\audio\streams\RG";
            fl[12] = @"\anim\anim.img"; fl[13] = @"\data\script\main.scm"; fl[14] = @"\data\script\script.img"; fl[15] = @"\models\gta_int.img";
            fl[16] = @"\models\gta3.img";

            // A list of hashes of various files of the game; 0 & 1 - only for final MD5 checks:
            flmd5[0] = "170B3A9108687B26DA2D8901C6948A18"; flmd5[1] = "E7697A085336F974A4A6102A51223960"; flmd5[2] = "528E75D663B8BAE072A01351081A2145"; flmd5[3] = "E26D86C7805D090D8210086876D6C35C";
            flmd5[4] = "FE31259226E0B4A8A963C70840E1FE8F"; flmd5[5] = "900148B8141EA4C1E782C3A48DBFBF3B"; flmd5[6] = "C25FCAA329B3D48F197FF4ED2A1D2A4D"; flmd5[7] = "9B4C18E4F3E82F0FEE41E30B2EA2246A";
            flmd5[8] = "909E7C4A7A29473E3885A96F987D7221"; flmd5[9] = "A1EC1CBE16DBB9F73022C6F33658ABE2"; flmd5[10] = "49B83551C684E17164F2047DCBA3E5AA"; flmd5[11] = "7491DC5325854C7117AF6E31900F38DD";
            flmd5[12] = "3359BA8CB820299161199EE7EF3F1C02"; flmd5[13] = "60AD23E272C3B0AA937053FE3006BE93"; flmd5[14] = "9598B82CF1E5AE7A8558057A01F6F2CE"; flmd5[15] = "DBE7E372D55914C39EB1D565E8707C8C";
            flmd5[16] = "9282E0DF8D7EEE3C4A49B44758DD694D";

            int er = 0, gv = 0;
            bool[] settings = new bool[10];
            string path = "";
            Console.Title = "SA Downgrader RW2";
            Console.WriteLine("[App] SA Downgrader RW2 version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() +  " by Vadim M. & Zalexanninev15");
            try
            {
                IniLoader cfg = new IniLoader(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\config.ini");
                settings[2] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackups"));
                settings[0] = Convert.ToBoolean(cfg.GetValue("Downgrader", "SetReadOnly"));
                settings[6] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateShortcut"));
                settings[7] = Convert.ToBoolean(cfg.GetValue("Downgrader", "ResetGame"));
                settings[9] = Convert.ToBoolean(cfg.GetValue("Downgrader", "GamePath"));
                settings[1] = Convert.ToBoolean(cfg.GetValue("SADRW2", "Component"));
                settings[8] = Convert.ToBoolean(cfg.GetValue("SADRW2", "SelectFolderUI"));
                settings[3] = Convert.ToBoolean(cfg.GetValue("Only", "GameVersion"));
                settings[4] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFiles"));
                settings[5] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFilesAndCheckMD5"));
                Logger("App", "config.ini", "true");
            }
            catch { Logger("App", "config.ini", "false"); }
            if (settings[1] == false)
            { try { string[] fpath = File.ReadAllLines(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\path.txt"); path = fpath[0]; Logger("App", "path.txt", "true"); } catch { Logger("App", "path.txt", "false"); } }
            if (settings[1] == true)
                path = args[0];
            if (settings[8] == true)
            {
                System.Windows.Forms.FolderBrowserDialog pathf = new System.Windows.Forms.FolderBrowserDialog();
                if (pathf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    path = pathf.SelectedPath;
                else
                    path = "";
            }
            if ((path != "") && (Directory.Exists(@path)))
            {
                Logger("Game", "Path", "true");

                // 0 - 1.0
                // 1 - Steam
                // 2 - 2.0
                // 3 - Rockstar Games Launcher
                // 4 - Unknown
                // 5 - Error
                // 6 - 1.01


                // Get version (EXE)
                string SaEXE = @path + @"\gta-sa.exe";
                Logger("Downgrader", "Process", "Get version (EXE)...");
                if (File.Exists(SaEXE))
                {
                    // Steam
                    try
                    {
                        string SteamEXEmd5 = Cache(SaEXE);
                        if (SteamEXEmd5 == "5BFD4DD83989A8264DE4B8E771F237FD")
                        {
                            gv = 1;
                            Logger("Game", "Version", "Steam");
                        }
                        else
                        {
                            gv = 4;
                            //not a Steam, this is other
                            SaEXE = @path + @"\gta_sa.exe";
                            try
                            {
                                string OtherEXEmd5 = Cache(SaEXE);
                                if (OtherEXEmd5 == "6687A315558935B3FC80CDBFF04437A4")
                                {
                                    gv = 3;
                                    Logger("Game", "Version", "Rockstar Games Launcher");
                                }
                                if ((OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373") || (OtherEXEmd5 == "4E99D762F44B1D5E7652DFA7E73D6B6F"))
                                {
                                    gv = 2;
                                    Logger("Game", "Version", "2.0");
                                }
                                if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373") && (OtherEXEmd5 != "4E99D762F44B1D5E7652DFA7E73D6B6F"))
                                {
                                    if ((OtherEXEmd5 != "E7697A085336F974A4A6102A51223960") && (OtherEXEmd5 != "170B3A9108687B26DA2D8901C6948A18"))
                                    {
                                        if ((OtherEXEmd5 != "A2929A61E4D63DD3C15749B2B7ED74AE") && (OtherEXEmd5 != "25405921D1C47747FD01FD0BFE0A05AE"))
                                        {
                                            gv = 4;
                                            Logger("Game", "Version", "Unknown");
                                        }
                                        else
                                        {
                                            gv = 6;
                                            Logger("Game", "Version", "1.01");
                                        }
                                    }
                                    else
                                    {
                                        gv = 0;
                                        Logger("Game", "Version", "1.0");
                                    }
                                }
                            }
                            catch { gv = 4; Logger("Game", "Version", "Unknown"); } //error, but it not bad					
                        }
                    }
                    catch { gv = 5; er = 1; Logger("Game", "Version", "Unknown [ERROR]"); } //error
                }
                else
                {
                    // Others
                    SaEXE = @path + @"\gta_sa.exe";
                    try
                    {
                        string OtherEXEmd5 = Cache(SaEXE);
                        if (OtherEXEmd5 == "6687A315558935B3FC80CDBFF04437A4")
                        {
                            gv = 3;
                            Logger("Game", "Version", "Rockstar Games Launcher");
                        }
                        if ((OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373") || (OtherEXEmd5 == "4E99D762F44B1D5E7652DFA7E73D6B6F"))
                        {
                            gv = 2;
                            Logger("Game", "Version", "2.0");
                        }
                        if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373") && (OtherEXEmd5 != "4E99D762F44B1D5E7652DFA7E73D6B6F"))
                        {
                            if ((OtherEXEmd5 != "E7697A085336F974A4A6102A51223960") && (OtherEXEmd5 != "170B3A9108687B26DA2D8901C6948A18"))
                            {
                                if ((OtherEXEmd5 != "A2929A61E4D63DD3C15749B2B7ED74AE") && (OtherEXEmd5 != "25405921D1C47747FD01FD0BFE0A05AE"))
                                {
                                    gv = 4;
                                    Logger("Game", "Version", "Unknown");
                                }
                                else
                                {
                                    gv = 6;
                                    Logger("Game", "Version", "1.01");
                                }
                            }
                            else
                            {
                                gv = 0;
                                Logger("Game", "Version", "1.0");
                            }
                        }
                    }
                    catch { gv = 5; er = 1; Logger("Game", "Version", "Unknown [ERROR]"); } //error
                }
                if (settings[7] == true)
                {
                    Logger("Downgrader", "Process", "Deleting gta_sa.set file...");
                    try
                    {
                        if (File.Exists(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set"))
                        {
                            File.Delete(@Environment.GetFolderPath(@Environment.SpecialFolder.MyDocuments) + @"\GTA San Andreas User Files\gta_sa.set");
                            Logger("ResetGameSettings", "gta_sa.set", "true");
                        }
                        else
                            Logger("ResetGameSettings", "gta_sa.set", "false");
                    }
                    catch { Logger("ResetGameSettings", "gta_sa.set", "false"); }
                }
                if ((gv != 0) && (er == 0) && (settings[3] == false))
                {
                    // Check files
                    Logger("Downgrader", "Process", "Scanning files...");
                    if (gv == 6) // 1.01
                    {
                            if (File.Exists(@path + fl[1]))
                                Logger("Game", @path + fl[1], "true");
                            else
                            {
                                er = 1;
                                Logger("Game", @path + fl[1], "false");
                            }
                    }
                    if (gv == 3) // RGL
                    {
                        for (int i = 1; i < fl.Length; i++)
                        {
                            if (File.Exists(@path + fl[i]))
                                Logger("Game", @path + fl[i], "true");
                            else
                            {
                                er = 1;
                                Logger("Game", @path + fl[i], "false");
                            }
                        }
                    }
                    if (gv == 2) // Version 2.0
                    {
                        for (int i = 1; i < fl.Length; i++)
                        {
                            if ((i >= 1) && (i > 11))
                            {
                                if (File.Exists(@path + fl[i]))
                                    Logger("Game", @path + fl[i], "true");
                                else
                                {
                                    er = 1;
                                    Logger("Game", @path + fl[i], "false");
                                }
                            }
                        }
                    }
                    if (gv == 1) // Steam version
                    {
                        for (int i = 0; i < fl.Length; i++)
                        {
                            if (i != 1)
                            {
                                if (File.Exists(@path + fl[i]))
                                    Logger("Game", @path + fl[i], "true");
                                else
                                {
                                    er = 1;
                                    Logger("Game", @path + fl[i], "false");
                                }
                            }
                        }
                    }
                    if (gv == 4)
                        Logger("Downgrader", "Process", "Downgrade is not possible!");
                    bool fisv = false;
                    if ((er == 0) && (settings[4] == false))
                    {
                        // Checking files (MD5)
                        string GameMD5 = "";
                        Logger("Game", "AllFiles", "true");
                        Logger("Downgrader", "Process", "Checking files (MD5)...");
                        if (gv == 6) // 1.01
                        {
                            try
                            {
                                GameMD5 = Cache(@path + fl[1]);
                                Logger("GameMD5", @path + fl[1], GameMD5);
                                if (GameMD5 == flmd5[0])
                                {
                                    fisv = true;
                                    Logger("GameMD5", @path + fl[1], "1.0");
                                }
                                else
                                    Logger("GameMD5", @path + fl[1], "Higher than 1.0");
                            }
                            catch { Logger("GameMD5", @path + fl[1], "File not found!"); }
                        }
                        if (gv == 3) // RGL
                        {
                            for (int i = 2; i < fl.Length; i++)
                            {
                                try
                                {
                                    GameMD5 = Cache(@path + fl[i]);
                                    Logger("GameMD5", @path + fl[i], GameMD5);
                                    if (GameMD5 == flmd5[i])
                                    {
                                        fisv = true;
                                        Logger("GameMD5", @path + fl[i], "1.0");
                                    }
                                    else
                                        Logger("GameMD5", @path + fl[i], "Higher than 1.0");
                                }
                                catch { Logger("GameMD5", @path + fl[i], "File not found!"); }
                            }
                        }
                        if (gv == 2) // Version 2.0
                        {
                            for (int i = 2; i < fl.Length; i++)
                            {
                                if ((i >= 2) && (i > 11))
                                {
                                    try
                                    {
                                        GameMD5 = Cache(@path + fl[i]);
                                        Logger("GameMD5", @path + fl[i], GameMD5);
                                        if (GameMD5 == flmd5[i])
                                        {
                                            fisv = true;
                                            Logger("GameMD5", @path + fl[i], "1.0");
                                        }
                                        else
                                            Logger("GameMD5", @path + fl[i], "Higher than 1.0");
                                    }
                                    catch { Logger("GameMD5", @path + fl[i], "File not found!"); }
                                }
                            }
                        }
                        if (gv == 1) // Steam version
                        {
                            try
                            {
                                GameMD5 = Cache(@path + fl[0]);
                                Logger("GameMD5", @path + fl[0], GameMD5);
                                if (GameMD5 == flmd5[0])
                                {
                                    fisv = true;
                                    Logger("GameMD5", @path + fl[0], "1.0");
                                }
                                else
                                    Logger("GameMD5", @path + fl[0], "Higher than 1.0");
                            }
                            catch { Logger("GameMD5", @path + fl[0], "File not found!"); }
                            try
                            {
                                GameMD5 = Cache(@path + fl[1]);
                                Logger("GameMD5", @path + fl[1], GameMD5);
                                if (GameMD5 == flmd5[1])
                                {
                                    fisv = true;
                                    Logger("GameMD5", @path + fl[1], "1.0");
                                }
                                else
                                    Logger("GameMD5", @path + fl[1], "Higher than 1.0");
                            }
                            catch { Logger("GameMD5", @path + fl[1], "Without a difference"); }
                            for (int i = 2; i < fl.Length; i++)
                            {
                                if (i >= 2)
                                {
                                    try
                                    {
                                        GameMD5 = Cache(@path + fl[i]);
                                        Logger("GameMD5", @path + fl[i], GameMD5);
                                        if (GameMD5 == flmd5[i])
                                        {
                                            fisv = true;
                                            Logger("GameMD5", @path + fl[i], "1.0");
                                        }
                                        else
                                            Logger("GameMD5", @path + fl[i], "Higher than 1.0");
                                    }
                                    catch { Logger("GameMD5", @path + fl[i], "File not found!"); }
                                }
                            }
                        }
                        if ((fisv == false) && (settings[5] == false))
                        {
                            // Backup (optional)
                            if (settings[2] == true)
                            {
                                Logger("Downgrader", "Process", "Create backups...");
                                if (gv == 6) // 1.01
                                {
                                    if (File.Exists(@path + fl[1] + ".bak"))
                                        File.Delete(@path + fl[1] + ".bak");
                                    try
                                    {
                                        File.Move(@path + fl[1], @path + fl[1] + ".bak");
                                        Logger("GameBackup", @path + fl[1], "Done!");
                                    }
                                    catch { er = 1; Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); }
                                }
                                if (gv == 3) // RGL
                                {
                                    if (File.Exists(@path + fl[1] + ".bak"))
                                        File.Delete(@path + fl[1] + ".bak");
                                    try
                                    {
                                        File.Move(@path + fl[1], @path + fl[1] + ".bak");
                                        Logger("GameBackup", @path + fl[1], "Done!");
                                    }
                                    catch { er = 1; Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); }
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if (File.Exists(@path + fl[i] + ".bak"))
                                            File.Delete(@path + fl[i] + ".bak");
                                        try
                                        {
                                            File.Move(@path + fl[i], @path + fl[i] + ".bak");
                                            Logger("GameBackup", @path + fl[i], "Done!");
                                        }
                                        catch { er = 1; Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); }
                                    }
                                }
                                if (gv == 2) // Version 2.0
                                {
                                    if (File.Exists(@path + fl[1] + ".bak"))
                                        File.Delete(@path + fl[1] + ".bak");
                                    try
                                    {
                                        File.Move(@path + fl[1], @path + fl[1] + ".bak");
                                        Logger("GameBackup", @path + fl[1], "Done!");
                                    }
                                    catch { er = 1; Logger("GameBackup", @path + fl[1], "File for backup wasn't found!"); }
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if ((i >= 2) && (i > 11))
                                        {
                                            if (File.Exists(@path + fl[i] + ".bak"))
                                                File.Delete(@path + fl[i] + ".bak");
                                            try
                                            {
                                                File.Move(@path + fl[i], @path + fl[i] + ".bak");
                                                Logger("GameBackup", @path + fl[i], "Done!");
                                            }
                                            catch { er = 1; Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); }
                                        }
                                    }
                                }
                                if (gv == 1) // Steam version
                                {
                                    if (File.Exists(@path + fl[0] + ".bak"))
                                        File.Delete(@path + fl[0] + ".bak");
                                    try
                                    {
                                        File.Move(@path + fl[0], @path + fl[0] + ".bak");
                                        Logger("GameBackup", @path + fl[0], "Done!");
                                    }
                                    catch { er = 1; Logger("GameBackup", @path + fl[0], "File for backup wasn't found!"); }
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if (i >= 2)
                                        {
                                            if (File.Exists(@path + fl[i] + ".bak"))
                                                File.Delete(@path + fl[i] + ".bak");
                                            try
                                            {
                                                File.Move(@path + fl[i], @path + fl[i] + ".bak");
                                                Logger("GameBackup", @path + fl[i], "Done!");
                                            }
                                            catch { er = 1; Logger("GameBackup", @path + fl[i], "File for backup wasn't found!"); }
                                        }
                                    }
                                }
                            }
                            if (er == 0)
                            {
                                // 5. Downgrader [1.1.3-Beta]
                                Logger("Downgrader", "Process", "Downgrading...");
                                try
                                {
                                    if (gv == 6) // C_1.01
                                    {
                                        File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[0] + "[16]", @path + fl[1], true);
                                        Logger("NewGame", @path + fl[1], "1.0");
                                        try
                                        {
                                            File.SetAttributes(@path + fl[1], FileAttributes.ReadOnly);
                                            Logger("NewGameReadOnly", @path + fl[1], "true");
                                        }
                                        catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game file!"); }
                                    }
                                    if (gv == 3)  // C_RGL
                                    {
                                        File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[1] + "[" + gv + "]", @path + fl[1], true);
                                        Logger("NewGame", @path + fl[1], "1.0");
                                        if (settings[0] == true)
                                        {
                                            try
                                            {
                                                File.SetAttributes(@path + fl[1], FileAttributes.ReadOnly);
                                                Logger("NewGameReadOnly", @path + fl[1], "true");
                                            }
                                            catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game file!"); }
                                        }
                                        for (int i = 2; i < fl.Length; i++)
                                        {
                                            File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[i], @path + fl[i], true);
                                            Logger("NewGame", @path + fl[i], "1.0");
                                            if (settings[0] == true)
                                            {
                                                try
                                                {
                                                    File.SetAttributes(@path + fl[i], FileAttributes.ReadOnly);
                                                    Logger("NewGameReadOnly", @path + fl[i], "true");
                                                }
                                                catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game files!"); }
                                            }

                                        }
                                    }
                                    if (gv == 2) // C_2.0
                                    {
                                        File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[1] + "[" + gv + "]", @path + fl[1], true);
                                        Logger("NewGame", @path + fl[1], "1.0");
                                        if (settings[0] == true)
                                        {
                                            try
                                            {
                                                File.SetAttributes(@path + fl[1], FileAttributes.ReadOnly);
                                                Logger("NewGameReadOnly", @path + fl[1], "true");
                                            }
                                            catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game file!"); }
                                        }
                                        for (int i = 2; i < fl.Length; i++)
                                        {
                                            if ((i >= 2) && (i > 11))
                                            {
                                                File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[i], @path + fl[i], true);
                                                Logger("NewGame", @path + fl[i], "1.0");
                                                if (settings[0] == true)
                                                {
                                                    try
                                                    {
                                                        File.SetAttributes(@path + fl[i], FileAttributes.ReadOnly);
                                                        Logger("NewGameReadOnly", @path + fl[i], "true");
                                                    }
                                                    catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game files!"); }
                                                }
                                            }
                                        }
                                    }
                                    if (gv == 1) // C_Steam
                                    {
                                        File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[0] + "[16]", @path + fl[0], true);
                                        Logger("NewGame", @path + fl[0], "1.0");
                                        if (settings[0] == true)
                                        {
                                            try
                                            {
                                                File.SetAttributes(@path + fl[0], FileAttributes.ReadOnly);
                                                Logger("NewGameReadOnly", @path + fl[0], "true");
                                            }
                                            catch { er = 1; Logger("NewGame", @path + fl[0], "An error occurred accessing the game file!"); }
                                        }
                                        File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[0] + "[16]", @path + fl[1], true);
                                        Logger("NewGame", @path + fl[1], "1.0");
                                        if (settings[0] == true)
                                        {
                                            try
                                            {
                                                File.SetAttributes(@path + fl[1], FileAttributes.ReadOnly);
                                                Logger("NewGameReadOnly", @path + fl[1], "true");
                                            }
                                            catch { er = 1; Logger("NewGame", @path + fl[1], "An error occurred accessing the game file!"); }
                                        }
                                        for (int i = 2; i < fl.Length; i++)
                                        {
                                            File.Copy(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache [!!!DO NOT DELETE!!!]" + fl[i], @path + fl[i], true);
                                            Logger("NewGame", @path + fl[i], "1.0");
                                            if (settings[0] == true)
                                            {
                                                try
                                                {
                                                    File.SetAttributes(@path + fl[i], FileAttributes.ReadOnly);
                                                    Logger("NewGameReadOnly", @path + fl[i], "true");
                                                }
                                                catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game files!"); }
                                            }
                                        }
                                    }
                                }
                                catch { er = 1; Logger("NewGame", "All", "An error occurred accessing the game files!"); }
                                if (er == 0)
                                {
                                    Logger("NewGame", "All", "1.0");
                                    if (settings[0] == true)
                                        Logger("NewGameReadOnly", "All", "true");
                                    // 6. Check for downgraded files
                                    Logger("Downgrader", "Process", "Check for downgraded files...");
                                    if (gv == 6) // 1.01
                                    {
                                        try
                                        {
                                            GameMD5 = Cache(@path + fl[1]);
                                            Logger("NewGameMD5", @path + fl[1], GameMD5);
                                            if (GameMD5 == flmd5[0])
                                            {
                                                fisv = true;
                                                Logger("NewGameMD5", @path + fl[1], "1.0");
                                            }
                                            else
                                            {
                                                fisv = false;
                                                Logger("NewGameMD5", @path + fl[1], "Higher than 1.0");
                                            }
                                        }
                                        catch { fisv = false; Logger("NewGameMD5", @path + fl[1], "File not found!"); }
                                    }
                                    if (gv == 3) // RGL
                                    {
                                        try
                                        {
                                            GameMD5 = Cache(@path + fl[1]);
                                            Logger("NewGameMD5", @path + fl[1], GameMD5);
                                            if (GameMD5 == flmd5[0])
                                            {
                                                fisv = true;
                                                Logger("NewGameMD5", @path + fl[1], "1.0");
                                            }
                                            else
                                            {
                                                fisv = false;
                                                Logger("NewGameMD5", @path + fl[1], "Higher than 1.0");
                                            }
                                        }
                                        catch { fisv = false; Logger("NewGameMD5", @path + fl[1], "File not found!"); }
                                        for (int i = 2; i < fl.Length; i++)
                                        {
                                            try
                                            {
                                                GameMD5 = Cache(@path + fl[i]);
                                                Logger("NewGameMD5", @path + fl[i], GameMD5);
                                                if (GameMD5 == flmd5[i])
                                                {
                                                    fisv = true;
                                                    Logger("NewGameMD5", @path + fl[i], "1.0");
                                                }
                                                else
                                                {
                                                    fisv = false;
                                                    Logger("NewGameMD5", @path + fl[i], "Higher than 1.0");
                                                }
                                            }
                                            catch { fisv = false; Logger("NewGameMD5", @path + fl[i], "File not found!"); }
                                        }
                                    }
                                    if (gv == 2) // 2.0
                                    {
                                        try
                                        {
                                            GameMD5 = Cache(@path + fl[1]);
                                            Logger("NewGameMD5", @path + fl[1], GameMD5);
                                            if (GameMD5 == flmd5[0])
                                            {
                                                fisv = true;
                                                Logger("NewGameMD5", @path + fl[1], "1.0");
                                            }
                                            else
                                            {
                                                fisv = false;
                                                Logger("NewGameMD5", @path + fl[1], "Higher than 1.0");
                                            }
                                        }
                                        catch { fisv = false; Logger("NewGameMD5", @path + fl[1], "File not found!"); }
                                        for (int i = 2; i < fl.Length; i++)
                                        {
                                            if ((i >= 2) && (i > 11))
                                            {
                                                try
                                                {
                                                    GameMD5 = Cache(@path + fl[i]);
                                                    Logger("NewGameMD5", @path + fl[i], GameMD5);
                                                    if (GameMD5 == flmd5[i])
                                                    {
                                                        fisv = true;
                                                        Logger("NewGameMD5", @path + fl[i], "1.0");
                                                    }
                                                    else
                                                    {
                                                        fisv = false;
                                                        Logger("NewGameMD5", @path + fl[i], "Higher than 1.0");
                                                    }
                                                }
                                                catch { fisv = false; Logger("NewGameMD5", @path + fl[i], "File not found!"); }
                                            }
                                        }
                                    }
                                    if (gv == 1) // Steam
                                    {
                                        try
                                        {
                                            GameMD5 = Cache(@path + fl[0]);
                                            Logger("NewGameMD5", @path + fl[0], GameMD5);
                                            if (GameMD5 == flmd5[0])
                                            {
                                                fisv = true;
                                                Logger("NewGameMD5", @path + fl[0], "1.0");
                                            }
                                            else
                                            {
                                                fisv = false;
                                                Logger("NewGameMD5", @path + fl[0], "Higher than 1.0");
                                            }
                                        }
                                        catch { fisv = false; Logger("NewGameMD5", @path + fl[0], "File not found!"); }
                                        try
                                        {
                                            GameMD5 = Cache(@path + fl[1]);
                                            Logger("NewGameMD5", @path + fl[1], GameMD5);
                                            if (GameMD5 == flmd5[0])
                                            {
                                                fisv = true;
                                                Logger("NewGameMD5", @path + fl[1], "1.0");
                                            }
                                            else
                                            {
                                                fisv = false;
                                                Logger("NewGameMD5", @path + fl[1], "Higher than 1.0");
                                            }
                                        }
                                        catch { fisv = false; Logger("NewGameMD5", @path + fl[1], "File not found!"); }
                                        for (int i = 2; i < fl.Length; i++)
                                        {
                                                try
                                                {
                                                    GameMD5 = Cache(@path + fl[i]);
                                                    Logger("NewGameMD5", @path + fl[i], GameMD5);
                                                    if (GameMD5 == flmd5[i])
                                                    {
                                                        fisv = true;
                                                        Logger("NewGameMD5", @path + fl[i], "1.0");
                                                    }
                                                    else
                                                    {
                                                        fisv = false;
                                                        Logger("NewGameMD5", @path + fl[i], "Higher than 1.0");
                                                    }
                                                }
                                                catch { fisv = false; Logger("NewGameMD5", @path + fl[i], "File not found!"); }
                                        }
                                    }
                                    if (fisv == true)
                                    {
                                        Logger("NewGameMD5", "All", "true");
                                        Logger("Downgrader", "Game", "Downgrade completed!");
                                        if (settings[6] == true)
                                        {
                                            Logger("Downgrader", "Process", "Creating a shortcut...");
                                            try
                                            {
                                                Create(@Environment.GetFolderPath(@Environment.SpecialFolder.Desktop) + @"\GTA San Andreas 1.0.lnk", @path + @"\gta_sa.exe");
                                                Logger("Downgrader", "Shortcut", "true");
                                            }
                                            catch { Logger("Downgrader", "Shortcut", "false"); }
                                        }
                                        if (settings[9] == true)
                                        {
                                            Logger("Downgrader", "Process", "Adding entries to the registry...");
                                            try
                                            {
                                                bool is64BitOS = Environment.Is64BitOperatingSystem;
                                                if (is64BitOS == true)
                                                {
                                                    Registry.LocalMachine.CreateSubKey("SOFTWARE\\WOW6432Node\\Rockstar Games\\GTA San Andreas\\Installation");
                                                    Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\Rockstar Games\\GTA San Andreas\\Installation", "ExePath", "\"" + path.ToString() + "\"");
                                                }
                                                Registry.LocalMachine.CreateSubKey("SOFTWARE\\Rockstar Games\\GTA San Andreas\\Installation");
                                                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Rockstar Games\\GTA San Andreas\\Installation", "ExePath", "\"" + path.ToString() + "\"");
                                                Logger("Downgrader", "Registry", "true");
                                            }
                                            catch { Logger("Downgrader", "Registry", "false"); }
                                        }
                                    }
                                    else
                                    {
                                        Logger("NewGameMD5", "All", "false");
                                        Logger("Downgrader", "Game", "Error checking files!");
                                        Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!");
                                    }
                                }
                            }
                            else
                            {
                                Logger("GameBackup", "AllFiles", "Some game files were not found, so it is not possible to continue working!");
                                Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!");
                            }
                        }
                        else
                        {
                            if (settings[5] == false)
                            {
                                Logger("GameMD5", "AllFiles", "It is impossible to determine exactly which version some files are taken from, because some of them have 1.0, and others are Higher than 1.0!");
                                Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!");
                            }
                        }
                    }
                    else
                    {
                        if (settings[4] == false)
                        {
                            Logger("Game", "AllFiles", "Some game files were not found, so it is not possible to continue working!");
                            Logger("Downgrader", "Game", "Please check the original files and, if necessary, reinstall the game!");
                        }
                    }
                }
                if (gv == 0)
                    Logger("Downgrader", "Process", "Downgrade is not required!");
            }
            else
                Logger("Game", "Path", "Value is not found!");
            if (settings[1] == false)
            {
                Console.WriteLine("Press Enter to Exit");
                Console.ReadLine();
            }
        }

        // For future :D
        //
        // xdelta patcher [0.1-Dev 1.1]
        //   
        //  
        //  for (int i = 2; i < fl.Length; i++)
        //  {
        //    int error = 0; string error_message = "Error: ";
        //    string cmds = "-d -v -s \"" + @file + "\" \"" + @Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches\" + fl[i] + "\" \"" + @path + fl[i] + ".temp\"";
        //    ProcessStartInfo start_info = new ProcessStartInfo("xdelta.exe", cmds);
        //    start_info.UseShellExecute = false;
        //    start_info.CreateNoWindow = true;
        //    start_info.WindowStyle = ProcessWindowStyle.Hidden;
        //    start_info.RedirectStandardOutput = true;
        //    start_info.RedirectStandardError = true;
        //    Process proc = new Process();
        //    proc.StartInfo = start_info;
        //    proc.Start();
        //    StreamReader std_out = proc.StandardOutput;
        //    StreamReader std_err = proc.StandardError;
        //    string resultStr = std_err.ReadToEnd().ToString();
        //    bool flag = resultStr.Contains("finished");
        //    if (flag)
        //    {
        //        Logger("NewGame", @path + fl[i], "1.0");
        //        error = 0;
        //    }
        //    else
        //    {
        //        error_message += resultStr;
        //        Logger("NewGame", @path + fl[i], error_message);
        //        error = 1;
        //    }
        //   }
        //    if (error == 0)
        //    {
        //        for (int i = 2; i < fl.Length; i++)
        //          File.Move(@path + fl[i] + ".temp",@path + fl[i]);
        //        Logger("NewGame", "All", "1.0");
        //     }
        //        
        //    else
        //     Logger("NewGame", "All", "An error occurred accessing the game files!");
        //
        
        public static string Logger(string type, string ido, string status)
        {
            Console.WriteLine("[" + type + "] " + ido + "=" + status);
            return "";
        }

        public static void Create(string ShortcutPath, string TargetPath)
        {
            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(ShortcutPath);
            Shortcut.TargetPath = TargetPath;
            Shortcut.WorkingDirectory = TargetPath.Replace(@"\gta_sa.exe", "");
            Shortcut.Save();
        }
        public static string Cache(string file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(@file))
                {
                    var hashBytes = md5.ComputeHash(stream);
                    var sb = new StringBuilder();
                    foreach (var t in hashBytes)
                        sb.Append(t.ToString("X2"));
                    return Convert.ToString(sb);
                }
            }
        }
    }
}