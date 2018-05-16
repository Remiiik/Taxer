using System.Xml.Serialization;
using Taxer.Domain.Shared;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "VetaD")]

    public class KHPeriodInfo : PeriodInfo
    {
        public KHPeriodInfo()
        {
            DocumentName = "KH1";
        }

        [XmlAttribute(AttributeName = "khdph_forma")]
        public string TaxForm { get; set; } = "B";
    }
}