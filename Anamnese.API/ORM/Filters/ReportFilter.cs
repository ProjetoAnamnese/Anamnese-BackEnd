namespace Anamnese.API.ORM.Filters
{
    public class ReportFilter
    {
        public int? PacientId { get; set; }
        public bool? CardiovascularIssues { get; set; }
        public bool? Diabates { get; set; }
        public bool? Smoker { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
