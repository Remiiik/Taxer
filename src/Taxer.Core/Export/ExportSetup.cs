using System;
using System.ComponentModel.DataAnnotations;

namespace Taxer.Core.Export
{
    public class ExportSetup
    {
        [Display(Name = "Account")]
        public string AccountName { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name="App Key")]
        public string AppKey { get; set; }
        public int Year { get; set; } = DateTime.Now.Year;
        public int Month { get; set; } = DateTime.Now.Month;
        public string OfficeNo { get; set; } = "461";
        public string OfficeDepartment { get; set; } = "3002";
    }
}