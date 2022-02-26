using System;
using System.Windows.Forms;

namespace JetpackGUI
{
    public partial class ChooseGame : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            VitNX.Functions.Windows.WindowSAndControls.WindowS.SetWindowsTenAndHighStyleForWinFormTitleToDark(Handle);
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