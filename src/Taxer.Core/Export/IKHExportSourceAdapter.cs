using Taxer.Domain.KH;

namespace Taxer.Core.Export
{
    public interface IKHExportSourceAdapter
    {
        KHExportSheet GetExportForPeriod(ExportSetup setup);
    }
}