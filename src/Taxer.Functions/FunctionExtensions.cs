using System;
using Microsoft.AspNetCore.Mvc;
using Taxer.Core.Export;

namespace Taxer.Functions
{
    internal static class FunctionExtensions
    {
        internal static IActionResult Validate(this ExportDto dto)
        {
            return null
                   ?? CheckParam(dto.AccountName, nameof(ExportDto.AccountName))
                   ?? CheckParam(dto.Login, nameof(ExportDto.Login))
                   ?? CheckParam(dto.Key, nameof(ExportDto.Key))
                   ?? CheckParam(dto.Month, nameof(ExportDto.Month), i => dto.Quarter.HasValue || (i.HasValue && i >= 1 && i <= 12))
                   ?? CheckParam(dto.Quarter, nameof(ExportDto.Quarter), i => dto.Month.HasValue || (i.HasValue && i >= 1 && i <= 4))
                   ?? CheckParam(dto.Year, nameof(ExportDto.Year), i => i >= 2000 && i <= 2030);
        }

        private static IActionResult CheckParam(string paramValue, string paramName)
        {
            if (string.IsNullOrEmpty(paramValue))
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