using System.Runtime.InteropServices;
using System.Text;

namespace JetpackDowngraderGUI
{
    public class INIEditor
    {
        private const int SIZE = 1024;
        private string path = null;
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetValue(string section, string key, string def, StringBuilder buffer, int size, string path);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);

        public INIEditor(string aPath) { path = aPath; }

        public string GetValue(string aSection, string aKey)
        {
            StringBuilder buffer = new StringBuilder(SIZE);
            GetValue(aSection, aKey, null, buffer, SIZE, path);
            //
            // Fix this for other languages!!!
            // 
            //Encoding utf = Encoding.Unicode;
            //Encoding win = Encoding.Default;
            //byte[] utfArr = utf.GetBytes(buffer.ToString());
            //byte[] winArr = Encoding.Convert(win, utf, utfArr);
            //
            //return win.GetString(winArr);
            return buffer.ToString();
        }

        public void WritePrivateString(string aSection, string aKey, string aValue)
        {
            WritePrivateString(aSection, aKey, aValue, path);
        }
    }
}