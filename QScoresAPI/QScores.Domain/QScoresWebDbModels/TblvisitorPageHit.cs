using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblvisitorPageHit
{
    public int RecId { get; set; }

    public int VisitId { get; set; }

    public string PageUrl { get; set; } = null!;

    public int PageId { get; set; }

    public string ElapsedTime { get; set; } = null!;
}
