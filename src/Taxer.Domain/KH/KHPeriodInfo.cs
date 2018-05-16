using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "VetaD")]
    public class KHPeriodInfo
    {
        [XmlAttribute(AttributeName = "mesic")]
        public int Month { get; set; }

        [XmlAttribute(AttributeName = "rok")]
        public int Year { get; set; }

        [XmlAttribute(AttributeName = "khdph_forma")]
        public string VATForm { get; set; } = "B";

        [XmlAttribute(AttributeName = "d_poddp")]
        public string ExportDate { get; set; }

        [XmlAttribute(AttributeName = "k_uladis")]
        public string DocumentReason { get; set; } = "DPH";

        [XmlAttribute(AttributeName = "dokument")]
        public string DocumentName { get; set; } = "KH1";
    }
}