using System.Xml.Serialization;
using Taxer.Domain.Shared;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "Pisemnost")]
    public class VATExportSheet : ExportSheet
    {
        [XmlElement(ElementName = "DPHDP3")]
        public VATExportBody ExportData { get; set; }

        [XmlAttribute(AttributeName = "nazevSW")]
        public string SoftwareName { get; set; }

        [XmlAttribute(AttributeName = "verzeSW")]
        public string SoftwareVersion { get; set; }
    }
}