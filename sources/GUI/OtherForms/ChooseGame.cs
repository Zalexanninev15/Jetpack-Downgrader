using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using VitNX.Win32;

namespace JetpackGUI
{
    public partial class ChooseGame : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            if (NativeFunctions.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                NativeFunctions.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        private GUI mygui = new GUI();

        public ChooseGame()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void MyLang_Load(object sender, EventArgs e)
        {

        }
    }
}