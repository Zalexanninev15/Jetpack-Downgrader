using System;
using System.Windows.Forms;
using System.IO;

namespace JetpackDowngraderGUI
{
    public partial class MainForm : Form
    {
        public MainForm() { InitializeComponent(); }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Loading the localization
                string[] lc = File.ReadAllLines(Application.StartupPath + @"\languages\" + Properties.Settings.Default.LanguageCode + ".jpl");
            }
            catch { MessageBox.Show("Error loading the localization file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            // Loading settings
                   bool[] settings = new bool[16];
                try
                {
                    INIEditor cfg = new INIEditor(@Application.StartupPath + @"\jpd.ini");
                    settings[2] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateBackups"));
                    //settings[0] = Convert.ToBoolean(cfg.GetValue("Downgrader", "SetReadOnly"));
                    settings[6] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateShortcut"));
                    settings[7] = Convert.ToBoolean(cfg.GetValue("Downgrader", "ResetGame"));
                    settings[14] = Convert.ToBoolean(cfg.GetValue("Downgrader", "RGLGarbage"));
                    settings[9] = Convert.ToBoolean(cfg.GetValue("Downgrader", "RegisterGamePath"));
                    settings[10] = Convert.ToBoolean(cfg.GetValue("Downgrader", "CreateNewGamePath"));
                    settings[12] = Convert.ToBoolean(cfg.GetValue("Downgrader", "Forced"));
                    settings[8] = Convert.ToBoolean(cfg.GetValue("JPD", "SelectFolder"));
                    settings[11] = Convert.ToBoolean(cfg.GetValue("JPD", "ConsoleTransparency"));
                    settings[13] = Convert.ToBoolean(cfg.GetValue("JPD", "UseMsg"));
                    settings[15] = Convert.ToBoolean(cfg.GetValue("JPD", "UseProgressBar"));
                    settings[1] = Convert.ToBoolean(cfg.GetValue("JPD", "Component"));
                    settings[3] = Convert.ToBoolean(cfg.GetValue("Only", "GameVersion"));
                    settings[4] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFiles"));
                    settings[5] = Convert.ToBoolean(cfg.GetValue("Only", "NextCheckFilesAndCheckMD5"));
                }
                catch { MessageBox.Show( }
        }
    }
}
