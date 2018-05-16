using Taxer.Domain.Shared;

namespace Taxer.Core.Infrastructure
{
    public interface IXmlGenerator
    {
        string Generate(ExportSheet getSampleData);
    }
}