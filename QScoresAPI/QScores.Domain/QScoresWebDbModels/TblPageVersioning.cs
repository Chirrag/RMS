using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPageVersioning
{
    public int VersionId { get; set; }

    public int? PageId { get; set; }

    public string Name { get; set; } = null!;

    public int TemplateId { get; set; }

    public int FolderId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime Createdon { get; set; }

    public string Status { get; set; } = null!;

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public bool? Inherits { get; set; }

    public bool IsPublished { get; set; }

    public int Wflstate { get; set; }

    public bool ForApproval { get; set; }
}
