using System;
using System.Xml.Serialization;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "Veta4")]
    public class VATReceived
    {
        [XmlAttribute(AttributeName = "pln23")]
        public decimal WithoutTaxH { get; set; }

        [XmlAttribute(AttributeName = "odp_tuz23_nar")]
        public decimal TaxH { get; set; }

        [XmlAttribute(AttributeName = "pln5")]
        public decimal WithoutTaxL { get; set; }

        [XmlAttribute(AttributeName = "odp_tuz5_nar")]
        public decimal TaxL { get; set; }


        [XmlAttribute(AttributeName = "odp_sum_nar")]
        public decimal TaxTotal
        {
            get => TaxH + TaxL;
            set { }
        }
    }
}