using System;
using System.Windows.Forms;

namespace JetpackGUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                GUI mygui = new GUI();
                mygui.ReadXml();
                if (mygui.Fields.FirstLaunch == false) { Application.Run(new MainForm()); } else { Application.Run(new MyLang()); }
            }
            catch(Exception ex) { DarkUI.Forms.DarkMessageBox.ShowError(ex.ToString(), "Error"); }
        }
    }
}