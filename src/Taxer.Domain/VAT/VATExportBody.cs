using System.Xml.Serialization;
using Taxer.Domain.Shared;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "DPHDP3")]
    public class VATExportBody : ExportBody
    {
        [XmlElement(ElementName = "VetaD")]
        public VATPeriodInfo PeriodInfo { get; set; }

        [XmlElement(ElementName = "VetaP")]
        public Subject Subject { get; set; }


        [XmlElement(ElementName = "Veta1")]
        public VATInvoiced Invoiced { get; set; }

        [XmlElement(ElementName = "Veta4")]
        public VATReceived Received { get; set; }

        [XmlElement(ElementName = "Veta5")]
        public VATAdjustments Adjustments { get; set; }

        [XmlElement(ElementName = "Veta6")]
        public VATSummary Summary { get; set; }
    }
}