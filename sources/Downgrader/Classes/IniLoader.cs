using System.Runtime.InteropServices;
using System.Text;

namespace Downgrader
{
    public class IniLoader
    {
        const int SIZE = 1024;
        string path = null;
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        static extern int GetValue(string section, string key, string def, StringBuilder buffer, int size, string path);
        public IniLoader(string aPath) { path = aPath; }
        public string GetValue(string aSection, string aKey) 
        { 
            StringBuilder buffer = new StringBuilder(SIZE); 
            GetValue(aSection, aKey, null, buffer, SIZE, path); 
            return buffer.ToString(); 
        }
    }
}