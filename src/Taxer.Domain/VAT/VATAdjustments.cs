using System.Xml.Serialization;

namespace Taxer.Domain.VAT
{
    [XmlRoot(ElementName = "Veta5")]
    public class VATAdjustments
    {
        [XmlAttribute(AttributeName = "odp_uprav_kf")]
        public double Coef { get; set; } = 0;
    }
}