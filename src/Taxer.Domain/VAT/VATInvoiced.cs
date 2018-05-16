using System.Xml.Serialization;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "Veta1")]
    public class VATInvoiced
    {
        [XmlAttribute(AttributeName = "obrat23")]
        public decimal WithoutTaxH { get; set; }

        [XmlAttribute(AttributeName = "dan23")]
        public decimal TaxH { get; set; }

        [XmlAttribute(AttributeName = "obrat5")]
        public decimal WithoutTaxL { get; set; }
        [XmlAttribute(AttributeName = "dan5")]
        public decimal TaxL { get; set; }
    }
}