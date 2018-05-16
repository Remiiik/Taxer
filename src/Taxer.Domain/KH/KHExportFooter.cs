using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "Kontrola")]
    public class KHExportFooter
    {
        [XmlElement(ElementName = "Uzivatel")]
        public KHUser User { get; set; }

        [XmlElement(ElementName = "Soubor")]
        public FileInfo FileInfo { get; set; }
    }
}