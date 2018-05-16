using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "VetaB3")]
    public class KHSmallExpenses
    {
        [XmlAttribute(AttributeName = "dan1")]
        public decimal PriceTaxH { get; set; }

        [XmlAttribute(AttributeName = "zakl_dane1")]
        public decimal PriceWithoutTaxH { get; set; }

        [XmlAttribute(AttributeName = "dan2")]
        public decimal PriceTaxL { get; set; }

        [XmlAttribute(AttributeName = "zakl_dane2")]
        public decimal PriceWithoutTaxL { get; set; }

        [XmlAttribute(AttributeName = "dan3")]
        public decimal PriceTaxN { get; set; }

        [XmlAttribute(AttributeName = "zakl_dane3")]
        public decimal PriceWithoutTaxN { get; set; }
    }
}