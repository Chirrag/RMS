using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblMonitor
{
    public int RecId { get; set; }

    public DateTime? Executed { get; set; }

    public string? CommandText { get; set; }

    public string? Comments { get; set; }
}
