using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblList24
{
    public int ItemId { get; set; }

    public bool IsPublished { get; set; }

    public int Wflstate { get; set; }

    public bool ForApproval { get; set; }

    public string Status { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedOn { get; set; }

    public string EventTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? EventDate { get; set; }

    public string? Link { get; set; }
}
