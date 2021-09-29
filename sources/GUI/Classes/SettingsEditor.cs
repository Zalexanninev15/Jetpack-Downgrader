using System;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    public class SettingsEditor
    {
        public Boolean CreateBackups = false;
        public Boolean CreateShortcut = true;
        public Boolean ResetGame = true;
        public Boolean RGL_GarbageCleaning = false;
        public Boolean RegisterGamePath = false;
        public Boolean CopyGameToNewPath = true;
        public Boolean EnableDirectPlay = true;
        public Boolean InstallDirectXComponents = false;
        public Boolean Forced = false;
        public Boolean UserMode = true;
    }
    
    public class Props
    {
        public SettingsEditor Fields;
        public Props() { Fields = new SettingsEditor(); }

        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(SettingsEditor));
            using (TextWriter writer = new StreamWriter(Application.StartupPath + @"\files\downgrader.xml"))  { ser.Serialize(writer, Fields);  } 
        }

        public void ReadXml()
        {
            if (File.Exists(Application.StartupPath + @"\files\downgrader.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(SettingsEditor));
                using (TextReader reader = new StreamReader(Application.StartupPath + @"\files\downgrader.xml"))  { Fields = ser.Deserialize(reader) as SettingsEditor; } 
            }
        }
    }
}