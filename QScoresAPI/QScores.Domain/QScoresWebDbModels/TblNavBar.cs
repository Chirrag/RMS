using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblNavBar
{
    public int NavId { get; set; }

    public string Name { get; set; } = null!;

    public int Level { get; set; }

    public int Position { get; set; }

    public int LinkId { get; set; }

    public int PageId { get; set; }

    public string Pgname { get; set; } = null!;

    public string Hdr { get; set; } = null!;

    public int? TemplatepageId { get; set; }

    public int? SiteId { get; set; }

    public bool? IsExternal { get; set; }

    public string? ExternalUrl { get; set; }
}
