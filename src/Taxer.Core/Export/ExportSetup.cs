using System;
using System.ComponentModel.DataAnnotations;

namespace Taxer.Core.Export
{
    public class ExportSetup
    {
        public int Year { get; set; } = DateTime.Now.Year;
        public int Period { get; set; }
        public string OfficeNo { get; set; }
        public string OfficeDepartmentNo { get; set; }

        public ExportMode ExportMode { get; set; } = ExportMode.Month;


        public DateTime GetReferenceDate()
        {
            if (ExportMode == ExportMode.Quarter)
                return new DateTime(Year, Period * 3 - 2, 1);
            else
                return new DateTime(Year, Period, 1);
        }

        public DateTime GetEndDate()
        {
            if (ExportMode == ExportMode.Quarter)
                return new DateTime(Year, Period * 3, 1).AddMonths(1);
            else
                return new DateTime(Year, Period, 1).AddMonths(1);
        }
    }
}