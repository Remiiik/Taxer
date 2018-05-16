using System.Xml.Serialization;

namespace Taxer.Domain.KH
{
    [XmlRoot(ElementName = "VetaP")]
    public class KHSubject
    {
        [XmlAttribute(AttributeName = "typ_ds")]
        public string SubjectType { get; set; } = "F";

        [XmlAttribute(AttributeName = "stat")]
        public string Country { get; set; }

        [XmlAttribute(AttributeName = "ulice")]
        public string Street { get; set; }

        [XmlAttribute(AttributeName = "c_orient")]
        public string StreetNo { get; set; }

        [XmlAttribute(AttributeName = "sest_jmeno")]
        public string UNKNOWN_FirstName2 { get; set; }

        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "c_ufo")]
        public string AuthorityOfficeNumber { get; set; }

        [XmlAttribute(AttributeName = "jmeno")]
        public string FirstName { get; set; }

        [XmlAttribute(AttributeName = "sest_prijmeni")]
        public string UNKNOWN_Surname2 { get; set; }

        [XmlAttribute(AttributeName = "prijmeni")]
        public string Surname { get; set; }

        [XmlAttribute(AttributeName = "psc")]
        public string Zip { get; set; }

        [XmlAttribute(AttributeName = "c_pop")]
        public string StreetNoOfficial { get; set; }

        [XmlAttribute(AttributeName = "dic")]
        public string VATNumber { get; set; }

        [XmlAttribute(AttributeName = "naz_obce")]
        public string City { get; set; }

        [XmlAttribute(AttributeName = "c_pracufo")]
        public string AuthorityOffice2 { get; set; }

        [XmlAttribute(AttributeName = "c_telef")]
        public string Phone { get; set; }
    }
}