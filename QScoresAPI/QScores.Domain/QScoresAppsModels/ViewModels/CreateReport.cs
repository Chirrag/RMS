namespace QScores.Domain.QScoresAppsModels.ViewModels
{
    public class CreateReport
    {
        public string? reportName { get; set; }
        public string? rptType { get; set; }
        public string? studydates { get; set; } = "";
        public string? scalePoints { get; set; } = "";
        public string? targets { get; set; } = "";
        public string? demoCodes { get; set; } = "";
        public string? miscellaneous { get; set; } = "";

        public string? ranking { get; set; } = "";
        public string? application { get; set; }

    }
}
