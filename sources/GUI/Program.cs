using System;
using System.Windows.Forms;

using VitNX.Functions.Information;
using VitNX.UI.ControlsV1.Forms;

namespace JetpackGUI
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            bool AvailableNewVersion = false;
            const bool DevVersion = true;
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (DevVersion == false)
                {
                    try
                    {
                        if (Internet.IsHaveInternet("github.com") == Internet.INTERNET_STATUS.CONNECTED)
                        {
                            string toolkit_version = VitNX.Functions.Web.DataFromSites.DownloadString(Data.NewVersionDetector, Application.ProductVersion);
                            if (toolkit_version != Data.JetpackDowngraderVersion)
                                AvailableNewVersion = true;
                        }
                        else
                            AvailableNewVersion = false;
                    }
                    catch { AvailableNewVersion = false; }
                }
                if (AvailableNewVersion == false)
                {
                    GUI mygui = new GUI();
                    mygui.ReadXml();
                    if (mygui.Fields.FirstLaunch == false)
                        Application.Run(new MainForm());
                    else
                        Application.Run(new MyLang());
                }
                else
                {
                    VitNX_MessageBox.ShowInformation("An update is available!\nNow you will be redirected to the download page of the latest version", "Information");
                    if (!VitNX.Functions.AppsAndProcesses.Processes.OpenLink(Urls.Release))
                    {
                        VitNX_MessageBox.ShowWarning("Browser to open the link was not found! The link will be copied to the clipboard!", "Warning");
                        Clipboard.SetText(Urls.Release);
                    }
                }
            }
            catch (Exception ex) { VitNX_MessageBox.ShowError(ex.ToString(), "Error"); }
        }
    }
}