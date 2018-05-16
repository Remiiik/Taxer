using System.Collections.Generic;
using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "DPHKH1")]
    public class KHExportBody
    {
        [XmlElement(ElementName = "VetaD")]
        public KHPeriodInfo PeriodInfo { get; set; }

        [XmlElement(ElementName = "VetaP")]
        public KHSubject Subject { get; set; }

        [XmlElement(ElementName = "VetaA4")]
        public List<KHInvoice> Invoices { get; set; } = new List<KHInvoice>();

        [XmlElement(ElementName = "VetaB2")]
        public List<KHInvoicedExpense> InvoicedExpenses { get; set; } = new List<KHInvoicedExpense>();

        [XmlElement(ElementName = "VetaB3")]
        public KHSmallExpenses SmallExpenses { get; set; } = new KHSmallExpenses();

        [XmlElement(ElementName = "VetaC")]
        public KHSummary Summary { get; set; }

        [XmlAttribute(AttributeName = "verzePis")]
        public string Version { get; set; }
    }
}