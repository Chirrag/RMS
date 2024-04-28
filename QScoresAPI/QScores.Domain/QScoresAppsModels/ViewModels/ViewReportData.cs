namespace QScores.Domain.QScoresAppsModels.ViewModels
{
    public class ViewReportData
    {
        public int UserID { get; set; }

        public string? ReportName { get; set; }

        public string? RptType { get; set; }
        public List<string>? Studydates { get; set; }

        public List<string>? ScalePoints { get; set; }

        public List<string>? Targets { get; set; }

        public List<string>? DemoCodes { get; set; }

        public string? Miscellaneous { get; set; }

        public string? Application { get; set; }

        public DateTime Datetime { get; set; }

        public string? DBaseName { get; set; }
    }
}
