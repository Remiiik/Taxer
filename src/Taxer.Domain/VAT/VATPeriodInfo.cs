using System.Xml.Serialization;
using Taxer.Domain.Shared;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "VetaD")]
    public class VATPeriodInfo : PeriodInfo
    {
        public VATPeriodInfo()
        {
            DocumentName = "DP3";
        }

        [XmlAttribute(AttributeName = "dapdph_forma")]
        public string TaxForm { get; set; } = "B";


        [XmlAttribute(AttributeName = "trans")]
        public string Trans { get; set; } = "A";


        [XmlAttribute(AttributeName = "typ_platce")]
        public string PayerType { get; set; } = "P";

        [XmlAttribute(AttributeName = "c_okec")]
        public string Area { get; set; } = "620000";
    }
}