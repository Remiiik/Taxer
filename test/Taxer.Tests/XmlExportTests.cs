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
                Period = 4,
                ExportMode = ExportMode.Month,
                OfficeNo = "461",
                OfficeDepartmentNo = "3002"
            };
        }
    }
}
