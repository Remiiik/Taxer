using System.Xml.Serialization;

namespace Taxer.Domain.Shared
{
    [XmlRoot(ElementName = "Soubor")]
    public class FileInfo
    {
        [XmlAttribute(AttributeName = "Delka")]
        public int Length { get; set; }

        [XmlAttribute(AttributeName = "KC")]
        public string CheckSum { get; set; }

        [XmlAttribute(AttributeName = "Nazev")]
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "c_ufo")]
        public string AuthorityOfficeNumber { get; set; }
    }
}