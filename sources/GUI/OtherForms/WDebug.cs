using System.Windows.Forms;

namespace JetpackGUI
{
    public partial class WDebug : Form
    {
        public WDebug()
        {
            InitializeComponent();
        }

        private void WDebug_Load(object sender, System.EventArgs e)
        {
            WDebug.ActiveForm.Size = new System.Drawing.Size(816, 489);
            consoleControl1.StartProcess(@Application.StartupPath + @"\files\jpd.exe", "\"" + Data.PathToGame + "\"");
        }
    }
}
