using System;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    public partial class MyLang : Form
    {
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }

        string[] langs = new string[10];

        public MyLang() { InitializeComponent(); }

        void button2_Click(object sender, EventArgs e)
        {
            if (AllLangs.Text != "")
            {
                Properties.Settings.Default.FirstLaunch = false;
                Properties.Settings.Default.Save();
                Application.Restart();
            }
            else { DarkUI.Forms.DarkMessageBox.ShowWarning("You need to select a language from the list!", "Warning"); }
        }

        void MyLang_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(252, 160);
            AllLangs.Items.Clear();
            langs = Directory.GetFiles(@Application.StartupPath + @"\files\languages", "*.ini");
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    IniEditor lang = new IniEditor(langs[i]);
                    string lg = Convert.ToString(lang.GetValue("Interface", "Language"));
                    AllLangs.Items.Add(lg);
                }
            }
        }

        void AllLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    IniEditor lang = new IniEditor(langs[i]);
                    if (AllLangs.Text == Convert.ToString(lang.GetValue("Interface", "Language")))
                    {
                        Properties.Settings.Default.LanguageCode = new FileInfo(langs[i]).Name.Replace(".ini", ""); ;
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }
    }
}
