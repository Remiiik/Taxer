using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Taxer.Core.Export;
using Taxer.Core.Infrastructure;
using Taxer.FakturoidAdapter;

namespace Taxer.Functions
{
    public static class KHReports
    {
        [FunctionName("GetMonthReport")]
        public static IActionResult GetMonthReport([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetMonthReport/key/{key}/accountname/{accountname}/login/{login}/year/{year}/month/{month}")]HttpRequest req, string key, string accountName, string login, int year, int month, TraceWriter log)
        {
            log.Info("Function started");

            IActionResult check = null
                                  ?? CheckParam(accountName, nameof(accountName))
                                  ?? CheckParam(login, nameof(login))
                                  ?? CheckParam(key, nameof(key))
                                  ?? CheckParam(month, nameof(month), i => i >= 1 && i <= 12)
                                  ?? CheckParam(year, nameof(year), i => i >= 2000 && i <= 2030);

            if (check != null)
                return check;

            var setup = new ExportSetup()
            {
                Year = year,
                Month = month,
                AccountName = accountName,
                AppKey = key,
                Login = login
            };


            var exporter = new XmlGenerator();

            log.Info("Export started");
            var exportedData = new FakturoidExportSourceClient().GetExportForPeriod(setup);
            log.Info("Data exported");

            string xml = exporter.Generate(exportedData);
            log.Info("Report generated");

            return new OkObjectResult(xml);
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
}
