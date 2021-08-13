using System.Xml.Serialization;

namespace JetpackDowngrader
{
    [XmlRoot(ElementName = "SettingsEditor")]
    public class SettingsEditor
    {
        [XmlElement(ElementName = "CreateBackups")]
        public bool CreateBackups { get; set; }
        [XmlElement(ElementName = "CreateShortcut")]
        public bool CreateShortcut { get; set; }
        [XmlElement(ElementName = "ResetGame")]
        public bool ResetGame { get; set; }
        [XmlElement(ElementName = "RGL_GarbageCleaning")]
        public bool RGLGarbageCleaning { get; set; }
        [XmlElement(ElementName = "RegisterGamePath")]
        public bool RegisterGamePath { get; set; }
        [XmlElement(ElementName = "CopyGameToNewPath")]
        public bool CopyGameToNewPath { get; set; }
        [XmlElement(ElementName = "EnableDirectPlay")]
        public bool EnableDirectPlay { get; set; }
        [XmlElement(ElementName = "InstallDirectXComponents")]
        public bool InstallDirectXComponents { get; set; }
        [XmlElement(ElementName = "Forced")]
        public bool Forced { get; set; }
        [XmlElement(ElementName = "UserMode")]
        public bool UserMode { get; set; }
        [XmlAttribute(AttributeName = "xsd")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}