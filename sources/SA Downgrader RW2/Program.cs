using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SA_Downgrader_RW2
{
    class Program
    {
        public static void Main(string[] args)
        {
            string[] fl = new string[17];
            string[] flmd5 = new string[17];

            // A list of files:
            fl[0] = @"\gta-sa.exe"; fl[1] = @"\gta_sa.exe"; fl[2] = @"\audio\CONFIG\TrakLkup.dat"; fl[3] = @"\audio\streams\BEATS";
            fl[4] = @"\audio\streams\CH"; fl[5] = @"\audio\streams\CR"; fl[6] = @"\audio\streams\CUTSCENE"; fl[7] = @"\audio\streams\DS";
            fl[8] = @"\audio\streams\MH"; fl[9] = @"\audio\streams\MR"; fl[10] = @"\audio\streams\RE"; fl[11] = @"\audio\streams\RG";
            fl[12] = @"\anim\anim.img"; fl[13] = @"\data\script\main.scm"; fl[14] = @"\data\script\script.img"; fl[15] = @"\models\gta_int.img";
            fl[16] = @"\models\gta3.img";

            // A list of hashes of various files of the game; 0 & 1 - only for final MD5 checks:
            flmd5[0] = "E7697A085336F974A4A6102A51223960"; flmd5[1] = "E7697A085336F974A4A6102A51223960"; flmd5[2] = "528E75D663B8BAE072A01351081A2145"; flmd5[3] = "E26D86C7805D090D8210086876D6C35C";
            flmd5[4] = "FE31259226E0B4A8A963C70840E1FE8F"; flmd5[5] = "900148B8141EA4C1E782C3A48DBFBF3B"; flmd5[6] = "C25FCAA329B3D48F197FF4ED2A1D2A4D"; flmd5[7] = "9B4C18E4F3E82F0FEE41E30B2EA2246A";
            flmd5[8] = "909E7C4A7A29473E3885A96F987D7221"; flmd5[9] = "A1EC1CBE16DBB9F73022C6F33658ABE2"; flmd5[10] = "49B83551C684E17164F2047DCBA3E5AA"; flmd5[11] = "7491DC5325854C7117AF6E31900F38DD";
            flmd5[12] = "3359BA8CB820299161199EE7EF3F1C02"; flmd5[13] = "60AD23E272C3B0AA937053FE3006BE93"; flmd5[14] = "9598B82CF1E5AE7A8558057A01F6F2CE"; flmd5[15] = "DBE7E372D55914C39EB1D565E8707C8C";
            flmd5[16] = "9282E0DF8D7EEE3C4A49B44758DD694D";

            int er = 0, gv = 0;
            bool[] settings = new bool[3];
            string path = "";
            Console.Title = "SA Downgrader RW2";
            Console.WriteLine("[App] SA Downgrader RW2 version 0.2 by Vadim M & Zalexanninev15");
            try
            {
                IniLoader cfg = new IniLoader(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\config.ini");
                settings[0] = Convert.ToBoolean(cfg.GetValue("Downgrader", "SetReadOnly"));
                settings[1] = Convert.ToBoolean(cfg.GetValue("SADRW2", "Component"));
                settings[2] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackup"));
                Logger("App", "config.ini", "true");
            }
            catch { Logger("App", "config.ini", "false"); }
            if (settings[1] == false)
            { try { string[] fpath = File.ReadAllLines(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\path.txt"); path = fpath[0]; Logger("App", "path.txt", "true"); } catch { Logger("App", "path.txt", "false"); } }
            if (settings[1] == true)
                path = args[0];
            if ((path != "") && (Directory.Exists(@path)))
            {
                Logger("Game", "Directory", "true");

                // Get version (EXE)
                string SaEXE = @path + @"\gta-sa.exe";
                Logger("Downgrader", "Process", "Get version (EXE)...");

                // 0 - 1.0
                // 1 - Steam
                // 2 - 2.0
                // 3 - Rockstar Games Launcher
                // 4 - Unknown
                // 5 - Error

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
                                        gv = 4;
                                        Logger("Game", "Version", "Unknown");
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
                    catch { gv = 5; er = 1; Logger("Game", "Version", "false"); } //error
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
                                gv = 4;
                                Logger("Game", "Version", "Unknown");
                            }
                            else
                            {
                                gv = 0;
                                Logger("Game", "Version", "1.0");
                            }
                        }
                    }
                    catch { gv = 5; er = 1; Logger("Game", "Version", "false"); } //error
                }
                if ((gv != 0) && (er == 0))
                {
                    // Check files
                    Logger("Downgrader", "Process", "Scanning files...");
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
                    if (er == 0)
                    {
                        // Checking files (MD5)
                        string GameMD5 = "";
                        Logger("Game", "AllFiles", "true");
                        Logger("Downgrader", "Process", "Checking files (MD5)...");
                        if (gv == 3) // RGL
                        {
                            for (int i = 2; i < fl.Length; i++)
                            {
                                GameMD5 = Cache(@path + fl[i]);
                                Logger("GameMD5", @path + fl[i], GameMD5);
                                if (GameMD5 == flmd5[i])
                                {
                                    fisv = true;
                                    Logger("GameMD5", @path + fl[i], "1.0");
                                }
                                else
                                    Logger("GameMD5", @path + fl[i], "higher than 1.0");
                            }
                        }
                        if (gv == 2) // Version 2.0
                        {
                            for (int i = 2; i < fl.Length; i++)
                            {
                                if ((i >= 2) && (i > 11))
                                {
                                    GameMD5 = Cache(@path + fl[i]);
                                    Logger("GameMD5", @path + fl[i], GameMD5);
                                    if (GameMD5 == flmd5[i])
                                    {
                                        fisv = true;
                                        Logger("GameMD5", @path + fl[i], "1.0");
                                    }
                                    else
                                        Logger("GameMD5", @path + fl[i], "higher than 1.0");
                                }
                            }
                        }
                        if (gv == 1) // Steam version
                        {
                            for (int i = 2; i < fl.Length; i++)
                            {
                                if (i >= 2)
                                {
                                    GameMD5 = Cache(@path + fl[i]);
                                    Logger("GameMD5", @path + fl[i], GameMD5);
                                    if (GameMD5 == flmd5[i])
                                    {
                                        fisv = true;
                                        Logger("GameMD5", @path + fl[i], "1.0");
                                    }
                                    else
                                        Logger("GameMD5", @path + fl[i], "higher than 1.0");
                                }
                            }
                        }
                        if (fisv == false)
                        {
                            // Backup
                            Logger("Downgrader", "Process", "Create backups...");
                            if (settings[2] == true)
                            {
                                if (gv == 3) // RGL
                                {
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if (File.Exists(@path + fl[i] + ".bak"))
                                            File.Delete(@path + fl[i] + ".bak");
                                        try
                                        {
                                            File.Copy(@path + fl[i], @path + fl[i] + ".bak");
                                            // File.Delete(@path + fl[i]);
                                            Logger("GameBackup", @path + fl[i], "generated");
                                        }
                                        catch { er = 1; Logger("GameBackup", @path + fl[i], "file for backup wasn't found"); }
                                    }
                                }
                                if (gv == 2) // Version 2.0
                                {
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if ((i >= 2) && (i > 11))
                                        {
                                            if (File.Exists(@path + fl[i] + ".bak"))
                                                File.Delete(@path + fl[i] + ".bak");
                                            try
                                            {
                                                File.Copy(@path + fl[i], @path + fl[i] + ".bak");
                                                // File.Delete(@path + fl[i]);
                                                Logger("GameBackup", @path + fl[i], "generated");
                                            }
                                            catch { er = 1; Logger("GameBackup", @path + fl[i], "file for backup wasn't found"); }
                                        }
                                    }
                                }
                                if (gv == 1) // Steam version
                                {
                                    for (int i = 2; i < fl.Length; i++)
                                    {
                                        if (i >= 2)
                                        {
                                            if (File.Exists(@path + fl[i] + ".bak"))
                                                File.Delete(@path + fl[i] + ".bak");
                                            try
                                            {
                                                File.Copy(@path + fl[i], @path + fl[i] + ".bak");
                                                // File.Delete(@path + fl[i]);
                                                Logger("GameBackup", @path + fl[i], "generated");
                                            }
                                            catch { er = 1; Logger("GameBackup", @path + fl[i], "file for backup wasn't found"); }
                                        }
                                    }
                                }
                            }
                            if (er == 0)
                            {


                                // 5. Downgrade

                                // 6. Check for downgraded files

                            }
                            else
                            {
                                Logger("GameBackup", "AllFiles", "some game files were not found, so it is not possible to continue working");
                                Logger("Downgrader", "Game", "please check the original files and, if necessary, reinstall the game");
                            }
                        }
                        else
                        {
                            Logger("GameMD5", "AllFiles", "it is impossible to determine exactly which version some files are taken from, because some of them have 1.0, and others are higher than 1.0");
                            Logger("Downgrader", "Game", "please check the original files and, if necessary, reinstall the game");
                        }
                    }
                    else
                    {
                        Logger("Game", "AllFiles", "some game files were not found, so it is not possible to continue working");
                        Logger("Downgrader", "Game", "please check the original files and, if necessary, reinstall the game");
                    }
                }
                if (gv == 0)
                    Logger("Downgrader", "Process", "Downgrade is not required!");
            }
            else
                Logger("Game", "Directory", "false");
            if (settings[1] == false)
            {
                Console.WriteLine("Press Enter to Exit");
                Console.ReadLine();
            }
        }

        public static string Logger(string type, string ido, string status)
        {
            Console.WriteLine("[" + type + "] " + ido + "=" + status);
            return "";
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