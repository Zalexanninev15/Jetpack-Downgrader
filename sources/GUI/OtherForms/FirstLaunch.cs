using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using VitNX.UI.ControlsV1.Forms;

namespace JetpackGUI
{
    public partial class MyLang : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            VitNX.Functions.WindowAndControls.Window.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
        }

        private GUI mygui = new GUI();
        private XmlSerializer lzol = new XmlSerializer(typeof(LanguagesString));
        private string[] langs = new string[10];

        public MyLang()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AllLangs.Text != "")
            {
                mygui.Fields.FirstLaunch = false;
                mygui.WriteXml();
                System.Threading.Tasks.Task.Delay(300);
                Application.Restart();
            }
            else { VitNX_MessageBox.ShowInformation("You need to select a language from the list!", "Information"); }
        }

        private void MyLang_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(265, 152);
            AllLangs.Items.Clear();
            langs = Directory.GetFiles($@"{Application.StartupPath}\files\languages", "*.xml");
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    string lg = "English";
                    using (StringReader reader = new StringReader(File.ReadAllText(langs[i])))
                    {
                        var LOCAL = (LanguagesString)lzol.Deserialize(reader);
                        lg = LOCAL.Language;
                    }
                    AllLangs.Items.Add(lg);
                }
            }
        }

        private void AllLangs_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < langs.Length; i++)
            {
                if (langs[i] != "")
                {
                    using (StringReader reader = new StringReader(File.ReadAllText(langs[i])))
                    {
                        var LOCAL = (LanguagesString)lzol.Deserialize(reader);
                        if (AllLangs.Text == LOCAL.Language)
                        {
                            mygui.Fields.LanguageCode = new FileInfo(langs[i]).Name.Replace(".xml", "");
                            mygui.WriteXml();
                            ActiveForm.Text = LOCAL.FirstTitle;
                            darkLabel1.Text = LOCAL.SelectLang;
                            button2.Text = LOCAL.ApplyAndLaunch;
                        }
                    }
                }
            }
        }
    }
}