using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Taxer.Core.Export;
using Taxer.Core.Infrastructure;
using Taxer.FakturoidAdapter;

namespace Taxer.Functions
{
    public static class VATReports
    {
        [FunctionName("VATReportPost")]
        public static async Task<IActionResult> VATReportPost([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "VATReportPost")]HttpRequest req, TraceWriter log)
        {
            var bodyString = await req.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ExportDto>(bodyString);

            return GetReport(dto, log);
        }


        [FunctionName("VATMonthReportGet")]
        public static IActionResult VATMonthReportGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route =
                "VATMonthReportGet/key/{key}/accountname/{accountname}/login/{login}/year/{year}/month/{month}")]
            HttpRequest req,
            string key,
            string accountName,
            string login,
            int year,
            int month,
            TraceWriter log)
        {
            return GetReport(new ExportDto
            {
                Key = key,
                AccountName = accountName,
                Login=login,
                Year = year,
                Month = month,
                OfficeDepartmentId = 3002,
                OfficeNo = 461
            }, log);
        }

        [FunctionName("VATQuarterReportGet")]
        public static IActionResult VATQuarterReportGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route =
                "VATQuarterReportGet/key/{key}/accountname/{accountname}/login/{login}/year/{year}/quarter/{quarter}")]
            HttpRequest req,
            string key,
            string accountName,
            string login,
            int year,
            int quarter,
            TraceWriter log)
        {
            return GetReport(new ExportDto
            {
                Key = key,
                AccountName = accountName,
                Login = login,
                Year = year,
                Quarter = quarter,
                OfficeDepartmentId = 3002,
                OfficeNo = 461
            }, log);
        }

        private static IActionResult GetReport(ExportDto exportDto, TraceWriter log)
        {

            log.Info("Function started");
            IActionResult check = exportDto.Validate();

            if (check != null)
                return check;

            var setup = new ExportSetup()
            {
                Year = exportDto.Year,
                Period = exportDto.Quarter ?? exportDto.Month.GetValueOrDefault(),
                ExportMode = exportDto.Quarter.HasValue ? ExportMode.Quarter : ExportMode.Month,

                OfficeDepartmentNo = exportDto.OfficeDepartmentId.ToString(),
                OfficeNo = exportDto.OfficeNo.ToString(),
            };

            var connectConfig = new FakturoidConfiguration()
            {
                AccountName = exportDto.AccountName,
                Key = exportDto.Key,
                Login = exportDto.Login
            };


            var exporter = new XmlGenerator();

            log.Info("Export started");
            var exportedData = new FakturoidVATExportAdapter(connectConfig).GetExportForPeriod(setup);
            log.Info("Data exported");

            string xml = exporter.Generate(exportedData);
            log.Info("Report generated");

            return new OkObjectResult(xml);
        }
    }
}
