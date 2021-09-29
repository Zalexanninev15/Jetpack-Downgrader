using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace JetpackGUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            bool AvailableNewVersion = false;
            const bool DevVersion = false;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (DevVersion == false)
                {
                    try
                    {
                        Ping ping = new Ping();
                        PingReply pingReply = null;
                        pingReply = ping.Send("github.com");
                        using (WebClient wc = new WebClient())
                        {
                            string toolkit_version = wc.DownloadString("https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/Version.txt");
                            if (toolkit_version != Data.JetpackDowngraderVersion) { AvailableNewVersion = true; }
                        }
                    }
                    catch(Exception ex) { AvailableNewVersion = false; VitNX.Forms.VitNX_MessageBox.ShowError(ex.ToString(), "Error"); }
                }
                if (AvailableNewVersion == false)
                {
                    GUI mygui = new GUI();
                    mygui.ReadXml();
                    if (mygui.Fields.FirstLaunch == false) { Application.Run(new MainForm()); } else { Application.Run(new MyLang()); }
                }
                else
                {
                    VitNX.Forms.VitNX_MessageBox.ShowInfo("An update is available!\nNow you will be redirected to the download page of the latest version", "Information");
                    try { System.Diagnostics.Process.Start("https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/latest"); } catch { VitNX.Forms.VitNX_MessageBox.ShowWarning("Browser to open the link was not found! The link will be copied to the clipboard!", "Warning"); Clipboard.SetText("https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/latest"); }
                }
            }
            catch(Exception ex) { VitNX.Forms.VitNX_MessageBox.ShowError(ex.ToString(), "Error"); }
        }
    }
}