using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblFooter
{
    public int FooterId { get; set; }

    public string? RightFooterHeading { get; set; }

    public string? Body { get; set; }

    public string? MonthYear { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
