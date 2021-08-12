using System;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    public partial class About : Form
    {
        [System.Runtime.InteropServices.DllImport("DwmApi")]
        static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        protected override void OnHandleCreated(EventArgs e) { if (DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0) { DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4); } }

        string[] langs = new string[10];

        public About() { InitializeComponent(); }

        void MyLang_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(508, 493);
            MsgInfo("\n- " + lc_text[22] + ": " + Convert.ToString(Application.ProductVersion) + "\n- " + lc_text[23] + ": Zalexanninev15 (" + lc_text[24] + ") && Vadim M. (" + lc_text[25] + ")\n- " + lc_text[26] + ": MIT" + "\n- " + lc_text[27]);
        }
    }
}
