using Taxer.Domain.VAT;

namespace Taxer.Core.Export
{
    public interface IVATExportSourceAdapter
    {
        VATExportSheet GetExportForPeriod(ExportSetup setup);
    }
}