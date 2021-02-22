using System.IO;
using System.Windows.Forms;

namespace JetpackDowngraderGUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { File.Delete(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\cache.exe"); } catch { } // For old versions (SADRW2)
            try { File.Delete(@Path.GetDirectoryName(@System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\patches.exe"); } catch { }
            Application.Run(new MainForm());
        }
    }
}