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
			
			// List of files, gta-sa.exe - only for Steam:
			fl[0] = @"\gta-sa.exe"; fl[1] = @"\gta_sa.exe"; fl[2] = @"\audio\CONFIG\TrakLkup.dat"; fl[3] = @"\audio\streams\BEATS";
            fl[4] = @"\audio\streams\CH"; fl[5] = @"\audio\streams\CR"; fl[6] = @"\audio\streams\CUTSCENE"; fl[7] = @"\audio\streams\DS";
            fl[8] = @"\audio\streams\MH"; fl[9] = @"\audio\streams\MR"; fl[10] = @"\audio\streams\RE"; fl[11] = @"\audio\streams\RG";
            fl[12] = @"\anim\anim.img"; fl[13] = @"\data\script\main.scm"; fl[14] = @"\data\script\script.img"; fl[15] = @"\models\gta_int.img";
            fl[16] = @"\models\gta3.img";
			
		    int er = 0, gv = 0;
		    bool[] settings = new bool[2];
			string path = "";
			Console.Title = "SADRW2";
			Console.WriteLine("[App] SA Downgrader RW2 version 0.1.3.2 by Zalexanninev15");
			try { string[] fpath = File.ReadAllLines(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\path.txt"); path = fpath[0]; Logger("App", "path.txt", "true"); } catch { Console.WriteLine("Status: 1"); Logger("App", "path.txt", "false"); }
			try 
			{
				IniLoader cfg = new IniLoader(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\config.ini");
				settings[0] = Convert.ToBoolean(cfg.GetValue("Downgrader", "ReadOnly"));
				settings[1] = Convert.ToBoolean(cfg.GetValue("SADRW2", "Component"));
				Logger("App", "config.ini", "true");
			} 
			catch { Logger("App", "config.ini", "false"); }
			if (path != "")
			{
				string SaEXE = @path + @"\gta-sa.exe";
				Logger("Downgrader", "Process", "Get version (EXE)...");
				
				// 0 - 1.0 {not usage, but soon}
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
					  if ((SteamEXEmd5 == "170B3A9108687B26DA2D8901C6948A18") || (SteamEXEmd5 == "5BFD4DD83989A8264DE4B8E771F237FD"))
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
					       if (OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373")
					       { 
					       	  gv = 2; 
					       	  Logger("Game", "Version", "2.0");
					       }
				           if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373"))
					       { 
				           	  gv = 4; 
				           	  Logger("Game", "Version", "Unknown"); 
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
					  if (OtherEXEmd5 == "BF25C28E9F6C13BD2D9E28F151899373")
					  { 
					       gv = 2; 
					       Logger("Game", "Version", "2.0");
					  }
				      if ((OtherEXEmd5 != "6687A315558935B3FC80CDBFF04437A4") && (OtherEXEmd5 != "BF25C28E9F6C13BD2D9E28F151899373"))
					  { 
				           gv = 4; 
				           Logger("Game", "Version", "Unknown"); 
				      }
					}
					catch { gv = 5; er = 1; Logger("Game", "Version", "false"); } //error
				}
				if (er == 0)
				{
				   // Check files
				   Logger("Downgrader", "Process", "Check files...");
				   if ((gv == 3) || (gv == 4)) // RGL/Unknown
				   {
				   	for (int i = 1; i < fl.Length; i++)
                    {
					    if (File.Exists(@path + fl[i]))
						   Logger("GameFiles", @path + fl[i], "true");
						else
						{
							   er = 1;
							   Logger("GameFiles", @path + fl[i], "false");
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
						   Logger("GameFiles", @path + fl[i], "true");
						  else
						  {
							   er = 1;
							   Logger("GameFiles", @path + fl[i], "false");
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
							        Logger("GameFiles", @path + fl[i], "true");
						        else
						        {
							        er = 1;
							        Logger("GameFiles", @path + fl[i], "false");
						        }
						   }
					    }
				   }
				   if (er == 0)
				   {
					  Logger("GameFiles", "All", "true");
				        // 4. Scan MD5 | Full game files
				       if (gv != 1) // not a Steam version
				       {
				   	      // List:
				       }
				       else // other versions
				      {
				   	     // List:
				       } 
				
				        // 5. Downgrade
				
				       // 6. Scan MD5 & Comparison
				       if (gv != 1) // not a Steam version
				      {
				   	       // List:
				       }
				       else // other versions
				      {
				   	      // List:
				      }
				 }
				else 
				{
					Logger("GameFiles", "All", "false");   
				}
		      }
			}
			else { Logger("App", "Path", "false"); }
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
		
		public string Scanner(string file, string comMD5)
		{
			string realMD5 = Cache(@file);
			try 
			{
			  if (realMD5 == comMD5)
			   return "Status: 4"; // it is downgraded
			  else
				return "Status: Good"; // file to downgrade
			}
			catch { return "Status: 5"; } // file is not found
		}
	}
}