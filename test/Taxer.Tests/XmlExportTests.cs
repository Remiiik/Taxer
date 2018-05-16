using System.IO;
using Taxer.Core.Export;
using Taxer.Core.Infrastructure;
using Taxer.FakturoidAdapter;
using Taxer.InMemoryAdapter;
using Xunit;

namespace Taxer.Tests
{
    public class XmlExportTests
    {
        private static ExportSetup CreateExportSetup()
        {
            return new ExportSetup()
            {
                Year = 2018,
                Month = 4,
                OfficeNo = "461",
                OfficeDepartmentNo = "3002"
            };
        }
    }
}
