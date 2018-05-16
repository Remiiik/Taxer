namespace Taxer.Functions
{
    internal class ExportDto
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