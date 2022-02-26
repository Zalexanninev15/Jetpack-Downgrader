using System;
using System.Windows.Forms;

using VitNX.Functions.Windows.Win32;

namespace JetpackGUI
{
    public partial class ChooseGame : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            if (Import.DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                Import.DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
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