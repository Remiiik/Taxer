using System;
using System.ComponentModel.DataAnnotations;

namespace Taxer.Core.Export
{
    public class ExportSetup
    {
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month;
        public string OfficeNo { get; set; }
        public string OfficeDepartmentNo { get; set; }
    }
}