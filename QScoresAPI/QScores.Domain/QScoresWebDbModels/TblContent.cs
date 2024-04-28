using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblContent
{
    public int ContentId { get; set; }

    public int ControlId { get; set; }

    public string Content { get; set; } = null!;

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public string? StrippedContent { get; set; }

    public int? VersionId { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public bool IsSecured { get; set; }
}
