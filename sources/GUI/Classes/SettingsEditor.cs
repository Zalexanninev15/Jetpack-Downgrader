using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace JetpackGUI
{
    public class SettingsEditor
    {
        public bool CreateBackups = false;
        public bool CreateShortcut = true;
        public bool ResetGame = true;
        public bool RGL_GarbageCleaning = false;
        public bool RegisterGamePath = false;
        public bool CopyGameToNewPath = true;
        public bool EnableDirectPlay = true;
        public bool InstallDirectXComponents = false;
        public bool Forced = false;
        public bool UserMode = true;
    }

    public class Props
    {
        public SettingsEditor Fields;

        public Props()
        { Fields = new SettingsEditor(); }

        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(SettingsEditor));
            using (TextWriter writer = new StreamWriter($@"{Application.StartupPath}\files\downgrader.xml"))
                ser.Serialize(writer, Fields);
        }

        public void ReadXml()
        {
            if (File.Exists($@"{Application.StartupPath}\files\downgrader.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SettingsEditor));
                using (TextReader reader = new StreamReader($@"{Application.StartupPath}\files\downgrader.xml"))
                    Fields = ser.Deserialize(reader) as SettingsEditor;
            }
        }
    }
}