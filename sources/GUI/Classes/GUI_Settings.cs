using System;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    public class GUI_Settings
    {
        public String LanguageCode = "EN";
        public Boolean FirstLaunch = true;
    }
    
    public class GUI
    {
        public GUI_Settings Fields;
        public GUI() { Fields = new GUI_Settings(); }

        public void WriteXml()
        {
            XmlSerializer ser = new XmlSerializer(typeof(GUI_Settings));
            using (TextWriter writer = new StreamWriter(Application.StartupPath + @"\files\gui.xml")) { ser.Serialize(writer, Fields); }
        }

        public void ReadXml()
        {
            if (File.Exists(Application.StartupPath + @"\files\gui.xml"))
            {
                XmlSerializer ser = new XmlSerializer(typeof(GUI_Settings));
                using (TextReader reader = new StreamReader(Application.StartupPath + @"\files\gui.xml"))  {  Fields = ser.Deserialize(reader) as GUI_Settings;  } 
            }
        }
    }
}