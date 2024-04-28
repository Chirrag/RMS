using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblReport
{
    public int RecId { get; set; }

    public int? UserId { get; set; }

    public string? ReportName { get; set; }

    public string? RptType { get; set; }

    public string? Studydates { get; set; }

    public string? ScalePoints { get; set; }

    public string? Targets { get; set; }

    public string? DemoCodes { get; set; }

    public string? Miscellaneous { get; set; }

    public int? ApplicationId { get; set; }

    public DateTime Datetime { get; set; }
}
