using System;
using Microsoft.AspNetCore.Mvc;

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
                   ?? CheckParam(dto.Month, nameof(ExportDto.Month), i => i >= 1 && i <= 12)
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