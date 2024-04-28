using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblVisitorTracking
{
    public int VisitId { get; set; }

    public string UserAgent { get; set; } = null!;

    public string Browser { get; set; } = null!;

    public bool IsCrawler { get; set; }

    public int VisitCount { get; set; }

    public string Url { get; set; } = null!;

    public string RefererUrl { get; set; } = null!;

    public string? UserHostAddress { get; set; }

    public DateTime? TimeOfVisit { get; set; }
}
