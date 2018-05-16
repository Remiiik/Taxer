using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "VetaA4")]
    public class KHInvoice
    {
        [XmlAttribute(AttributeName = "dan1")]
        public decimal PriceTaxH { get; set; }

        [XmlAttribute(AttributeName = "zakl_dane1")]
        public decimal PriceWithoutTaxH { get; set; }

        [XmlAttribute(AttributeName = "dppd")]
        public string InvoiceDate { get; set; }

        [XmlAttribute(AttributeName = "dan2")]
        public decimal PriceTaxL { get; set; }

        [XmlAttribute(AttributeName = "zakl_dane2")]
        public decimal PriceWithoutTaxL { get; set; }

        [XmlAttribute(AttributeName = "zdph_44")]
        public string UNKNOWN_Zdph_44 { get; set; } = "N";

        [XmlAttribute(AttributeName = "dan3")]
        public decimal PriceTaxN { get; set; }

        [XmlAttribute(AttributeName = "c_evid_dd")]
        public string InvoiceNumber { get; set; }

        [XmlAttribute(AttributeName = "c_radku")]
        public int LineNumber { get; set; }

        [XmlAttribute(AttributeName = "zakl_dane3")]
        public decimal PriceWithoutTaxN { get; set; }

        [XmlAttribute(AttributeName = "kod_rezim_pl")]
        public string UNKNOWN_Kod_rezim_pl { get; set; } = "0";

        [XmlAttribute(AttributeName = "dic_odb")]
        public string PartnerVATNumber { get; set; }
    }
}