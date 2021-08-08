using System;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GUI mygui = new GUI();
            mygui.ReadXml();
            if (mygui.Fields.FirstRun == false) { Application.Run(new MainForm()); } else { Application.Run(new MyLang()); }
        }
    }
}