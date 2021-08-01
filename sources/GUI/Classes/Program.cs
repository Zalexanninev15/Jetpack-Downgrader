using System;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            if (File.Exists(@Application.StartupPath + @"\files\jpd.ini") == false) { File.WriteAllText(@Application.StartupPath + @"\files\jpd.ini", Properties.Resources.jpd1); }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IniEditor cfg = new IniEditor(@Application.StartupPath + @"\files\jpd.ini");
            if (Convert.ToBoolean(cfg.GetValue("GUI", "FirstRun")) == false) { Application.Run(new MainForm()); } else { Application.Run(new MyLang()); }
        }
    }
}