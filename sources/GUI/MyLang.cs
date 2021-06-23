using System;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    public partial class MyLang : Form
    {
        string[] langs = new string[10];
        public MyLang()
        {
            InitializeComponent();
        }

        void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FirstLaunch = false;
            Properties.Settings.Default.Save();
            Application.Restart();
        }

        void MyLang_Load(object sender, EventArgs e)
        {
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
