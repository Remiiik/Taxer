using System.Text;
using System.Xml.Serialization;
using Taxer.Core.Infrastructure;
using Taxer.Domain.Shared;

namespace Taxer.Core.Infrastructure
{
    public class XmlGenerator : IXmlGenerator
    {
        public string Generate(ExportSheet sampleData)
        {
            var serializer = new XmlSerializer(sampleData.GetType());

            var stringwriter = new StringWriterWithEncoding(Encoding.UTF8);
            serializer.Serialize(stringwriter, sampleData);
            return stringwriter.ToString();
        }
    }
}