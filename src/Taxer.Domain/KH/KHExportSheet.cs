using System.Xml.Serialization;
using Taxer.Domain.Shared;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "Pisemnost")]
    public class KHExportSheet : ExportSheet
    {
        [XmlElement(ElementName = "DPHKH1")]
        public KHExportBody ExportData { get; set; }

        [XmlElement(ElementName = "Kontrola")]
        public KHExportFooter Footer { get; set; }

        [XmlAttribute(AttributeName = "nazevSW")]
        public string SoftwareName { get; set; }

        [XmlAttribute(AttributeName = "verzeSW")]
        public string SoftwareVersion { get; set; }
    }
}