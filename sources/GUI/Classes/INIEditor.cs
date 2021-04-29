using System.Runtime.InteropServices;
using System.Text;

namespace JetpackDowngraderGUI
{
    public class IniEditor
    {
        private const int SIZE = 1024;
        private string path = null;
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetValue(string section, string key, string def, StringBuilder buffer, int size, string path);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);
        public IniEditor(string aPath) { path = aPath; }

        public string GetValue(string aSection, string aKey)
        {
            StringBuilder buffer = new StringBuilder(SIZE);
            GetValue(aSection, aKey, null, buffer, SIZE, path);
            // A fallback way to change the text encoding
            //
            // File.WriteAllText("text", buffer.ToString(), Encoding.Default);
            // string rt = File.ReadAllText("text");
            // File.Delete("text");
            byte[] local = Encoding.Default.GetBytes(buffer.ToString());
            string localText = Encoding.UTF8.GetString(local);
            return localText;
        }

        public void WritePrivateString(string aSection, string aKey, string aValue)
        {
            WritePrivateString(aSection, aKey, aValue, path);
        }
    }
}