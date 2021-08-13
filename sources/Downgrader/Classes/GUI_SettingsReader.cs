using System.Xml.Serialization;

namespace JetpackDowngrader
{
    [XmlRoot(ElementName = "GUI_Settings")]
    public class GUI_Settings
    {
        [XmlElement(ElementName = "LanguageCode")]
        public string LnguageCode { get; set; }
        [XmlElement(ElementName = "FirstLaunch")]
        public string FirstLaunch { get; set; }
        [XmlAttribute(AttributeName = "xsd")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}