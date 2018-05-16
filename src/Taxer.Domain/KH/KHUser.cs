using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "Uzivatel")]
    public class KHUser
    {
        [XmlAttribute(AttributeName = "jmeno")]
        public string FirstName { get; set; }

        [XmlAttribute(AttributeName = "prijmeni")]
        public string Surname { get; set; }
    }
}