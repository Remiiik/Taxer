using System;
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
    public static class KHReports
    {
        [FunctionName("KHMonthReportPost")]
        public static async Task<IActionResult> KHMonthReportPost([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "KHMonthReportPost")]HttpRequest req, TraceWriter log)
        {
            var bodyString = await req.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<KHExportDto>(bodyString);

            return GetMonthReport(dto, log);
        }


        [FunctionName("KHMonthReportGet")]
        public static IActionResult KHMonthReportGet(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route =
                "KHMonthReportGet/key/{key}/accountname/{accountname}/login/{login}/year/{year}/month/{month}")]
            HttpRequest req,
            string key,
            string accountName,
            string login,
            int year,
            int month,
            TraceWriter log)
        {
            return GetMonthReport(new KHExportDto
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

        private static IActionResult GetMonthReport(KHExportDto exportDto, TraceWriter log)
        {

            log.Info("Function started");
            IActionResult check = ValidateDto(exportDto);

            if (check != null)
                return check;

            var setup = new ExportSetup()
            {
                Year = exportDto.Year,
                Month = exportDto.Month,
                OfficeDepartmentNo = exportDto.OfficeDepartmentId.ToString(),
                OfficeNo = exportDto.OfficeNo.ToString()
            };

            var connectConfig = new FakturoidConfiguration()
            {
                AccountName = exportDto.AccountName,
                Key = exportDto.Key,
                Login = exportDto.Login
            };


            var exporter = new XmlGenerator();

            log.Info("Export started");
            var exportedData = new FakturoidExportSourceClient(connectConfig).GetExportForPeriod(setup);
            log.Info("Data exported");

            string xml = exporter.Generate(exportedData);
            log.Info("Report generated");

            return new OkObjectResult(xml);
        }

        private static IActionResult ValidateDto(KHExportDto dto)
        {
            return null
                  ?? CheckParam(dto.AccountName, nameof(KHExportDto.AccountName))
                  ?? CheckParam(dto.Login, nameof(KHExportDto.Login))
                  ?? CheckParam(dto.Key, nameof(KHExportDto.Key))
                  ?? CheckParam(dto.Month, nameof(KHExportDto.Month), i => i >= 1 && i <= 12)
                  ?? CheckParam(dto.Year, nameof(KHExportDto.Year), i => i >= 2000 && i <= 2030);
        }

        private static IActionResult CheckParam(string paramValue, string paramName)
        {
            if(string.IsNullOrEmpty(paramValue))
                return new BadRequestObjectResult($"Invalid parameter {paramName}");

            return null;
        }

        private static IActionResult CheckParam<T>(T paramValue, string paramName, Func<T, bool> predicate)
        {
            if (!predicate(paramValue))
                return new BadRequestObjectResult($"Invalid parameter {paramName}");

            return null;
        }
    }

    internal class KHExportDto
    {
        public string Login { get; set; }
        public string Key { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string AccountName { get; set; }
        public int OfficeDepartmentId  { get; set; }
        public int OfficeNo { get; set; }
    }
}
