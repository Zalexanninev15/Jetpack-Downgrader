using System.Runtime.InteropServices;
using System.Text;

namespace JetpackDowngrader
{
    public class IniLoader
    {
        private const int SIZE = 1024;
        private string path = null;
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetValue(string section, string key, string def, StringBuilder buffer, int size, string path);
        public IniLoader(string aPath) { path = aPath; }
        public string GetValue(string aSection, string aKey) { StringBuilder buffer = new StringBuilder(SIZE); GetValue(aSection, aKey, null, buffer, SIZE, path); return buffer.ToString(); }
    }
}