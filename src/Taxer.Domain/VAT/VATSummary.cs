using System.Xml.Serialization;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "Veta6")]
    public class VATSummary
    {
        [XmlAttribute(AttributeName = "dan_zocelk")]
        public decimal TotalTaxInvoiced { get; set; }

        [XmlAttribute(AttributeName = "odp_zocelk")]
        public decimal TotalTaxReceived { get; set; }

        [XmlAttribute(AttributeName = "dano_da")]
        public decimal TotalTaxToPay
        {
            get => TotalTaxInvoiced - TotalTaxReceived;
            set { }
        }

        [XmlAttribute(AttributeName = "dano_no")]
        public decimal Overpaid { get; set; } = 0;

        [XmlAttribute(AttributeName = "dano")]
        public decimal TaxDuty
        {
            get => 0;
            set { }
        }
    }
}