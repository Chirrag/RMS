using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblBlogIvrSystem
{
    public int ItemId { get; set; }

    public string? Header { get; set; }

    public string? Description { get; set; }

    public bool IsPublished { get; set; }

    public int Wflstate { get; set; }

    public bool ForApproval { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }
}
