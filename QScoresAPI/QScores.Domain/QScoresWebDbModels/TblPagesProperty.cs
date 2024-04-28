using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPagesProperty
{
    public int RecId { get; set; }

    public int PageId { get; set; }

    public bool IsSecured { get; set; }

    public bool? IsSearchable { get; set; }

    public string? PageTitle { get; set; }

    public string? Description { get; set; }

    public string? Keywords { get; set; }

    public string? MenuLabel { get; set; }

    public bool InNav { get; set; }

    public string? Url { get; set; }

    public int? Inx { get; set; }
}
