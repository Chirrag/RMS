using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblView
{
    public int ViewId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ListId { get; set; }

    public string Sqlcode { get; set; } = null!;

    public string OutputFormat { get; set; } = null!;

    public bool DisplayAll { get; set; }

    public int NoOfitems { get; set; }

    public string? AddinfoUrl { get; set; }

    public bool? ExposeRss { get; set; }

    public string ViewName { get; set; } = null!;

    public string? Rssxslt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }
}
