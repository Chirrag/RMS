using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblContentsVersioning
{
    public int VersiontId { get; set; }

    public int? ContentId { get; set; }

    public int ControlId { get; set; }

    public string Content { get; set; } = null!;

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public string? StrippedContent { get; set; }

    public string Status { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public bool IsScheduled { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public bool IsSecured { get; set; }

    public bool? Inherits { get; set; }

    public bool IsPublished { get; set; }

    public int Wflstate { get; set; }

    public bool ForApproval { get; set; }

    public string? DocType { get; set; }

    public byte[]? BinaryContent { get; set; }
}
