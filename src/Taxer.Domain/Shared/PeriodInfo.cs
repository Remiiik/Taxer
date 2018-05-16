using System.Xml.Serialization;

namespace Taxer.Domain.Shared
{
    public class PeriodInfo
    {
        [XmlAttribute(AttributeName = "mesic")]
        public int Month { get; set; }

        [XmlAttribute(AttributeName = "rok")]
        public int Year { get; set; }

        [XmlAttribute(AttributeName = "d_poddp")]
        public string ExportDate { get; set; }

        [XmlAttribute(AttributeName = "k_uladis")]
        public string DocumentReason { get; set; } = "DPH";

        [XmlAttribute(AttributeName = "dokument")]
        public string DocumentName { get; set; }
    }
}