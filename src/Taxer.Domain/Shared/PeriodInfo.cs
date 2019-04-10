using System.Xml.Serialization;

namespace Taxer.Domain.Shared
{
    public class PeriodInfo
    {
        private int? _month;
        private int? _quarter;

        [XmlIgnore]
        public int? Month
        {
            get => _month;
            set
            {
                _month = value;
                MonthWrapper = value?.ToString();
            }
        }

        [XmlIgnore]
        public int? Quarter
        {
            get => _quarter;
            set
            {
                _quarter = value;
                QuarterWrapper = value?.ToString();
            }
        }

        [XmlAttribute(AttributeName = "mesic")]
        public string MonthWrapper { get; set; }

        [XmlAttribute(AttributeName = "ctvrt")]
        public string QuarterWrapper { get; set; }

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