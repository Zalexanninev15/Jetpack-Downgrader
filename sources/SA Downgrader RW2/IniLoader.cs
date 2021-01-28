using System.Runtime.InteropServices;
using System.Text;

namespace SA_Downgrader_RW2
{
	public class IniLoader
{
    public IniLoader(string aPath)
    {
        path = aPath;
    }
 
    public IniLoader() : this("") { }

    public string GetValue(string aSection, string aKey)
    {
        StringBuilder buffer = new StringBuilder(SIZE);
        GetValue(aSection, aKey, null, buffer, SIZE, path);
        return buffer.ToString();
    }
 
    public string Path { get { return path; } set { path = value; } }
    private const int SIZE = 1024;
    private string path = null;
 
    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
    private static extern int GetValue(string section, string key, string def, StringBuilder buffer, int size, string path);
	}
}