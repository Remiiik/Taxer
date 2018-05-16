using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "VetaC")]
    public class KHSummary
    {
        [XmlAttribute(AttributeName = "pln23")]
        public decimal SumExpendedH { get; set; }

        [XmlAttribute(AttributeName = "obrat5")]
        public decimal SumInvoicedL { get; set; }

        [XmlAttribute(AttributeName = "rez_pren5")]
        public string UNKNOWN_Rez_pren5 { get; set; }

        [XmlAttribute(AttributeName = "pln5")]
        public decimal SumExpendedL { get; set; }

        [XmlAttribute(AttributeName = "obrat23")]
        public decimal SumInvoicedH { get; set; }

        [XmlAttribute(AttributeName = "pln_rez_pren")]
        public string UNKNOWN_Pln_rez_pren { get; set; }

        [XmlAttribute(AttributeName = "rez_pren23")]
        public string UNKNOWN_Rez_pren23 { get; set; }

        [XmlAttribute(AttributeName = "celk_zd_a2")]
        public string UNKNOWN_Celk_zd_a2 { get; set; }
    }
}