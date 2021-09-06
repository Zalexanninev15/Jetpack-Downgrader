using System;
using System.IO;
using System.Windows.Forms;
using ZCF;

namespace JetpackGUI
{
    public partial class MyLang : Form
    {
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }

        GUI mygui = new GUI();
        string[] langs = new string[10];

        public MyLang() { InitializeComponent(); }

        void button2_Click(object sender, EventArgs e)
        {
            if (AllLangs.Text != "")
            {
                mygui.Fields.FirstLaunch = false;
                mygui.WriteXml();
                System.Threading.Tasks.Task.Delay(300);
                Application.Restart();
            }
            else { DarkUI.Forms.DarkMessageBox.ShowWarning("You need to select a language from the list!", "Information"); }
        }

        void MyLang_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(263, 159);
            AllLangs.Items.Clear();
            langs = Directory.GetFiles(@Application.StartupPath + @"\files\languages", "*.zcf");
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    Editor lang = new Editor(langs[i]);
                    string lg = lang.GetValue("Language");
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
                    Editor lang = new Editor(langs[i]);
                    if (AllLangs.Text == lang.GetValue("Language")) 
                    { 
                        mygui.Fields.LanguageCode = new FileInfo(langs[i]).Name.Replace(".zcf", ""); 
                        mygui.WriteXml();
                        MyLang.ActiveForm.Text = lang.GetValue("FirstTitle");
                        darkLabel1.Text = lang.GetValue("SelectLang");
                        button2.Text = lang.GetValue("ApplyAndLaunch");
                    }
                }
            }
        }
    }
}
