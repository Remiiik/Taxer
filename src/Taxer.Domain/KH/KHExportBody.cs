using System.Collections.Generic;
using System.Xml.Serialization;
using Taxer.Domain.Shared;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "DPHKH1")]
    public class KHExportBody : ExportBody
    {
        [XmlElement(ElementName = "VetaD")]
        public KHPeriodInfo PeriodInfo { get; set; }

        [XmlElement(ElementName = "VetaP")]
        public Subject Subject { get; set; }


        [XmlElement(ElementName = "VetaA4")]
        public List<KHInvoice> Invoices { get; set; } = new List<KHInvoice>();

        [XmlElement(ElementName = "VetaB2")]
        public List<KHInvoicedExpense> InvoicedExpenses { get; set; } = new List<KHInvoicedExpense>();

        [XmlElement(ElementName = "VetaB3")]
        public KHSmallExpenses SmallExpenses { get; set; } = new KHSmallExpenses();

        [XmlElement(ElementName = "VetaC")]
        public KHSummary Summary { get; set; }
    }
}