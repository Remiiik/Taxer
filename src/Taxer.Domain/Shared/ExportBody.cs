using System.Xml.Serialization;
using Taxer.Domain.KH;

namespace Taxer.Domain.Shared
{
    public class ExportBody
    {
        [XmlAttribute(AttributeName = "verzePis")]
        public string Version { get; set; }
    }
}