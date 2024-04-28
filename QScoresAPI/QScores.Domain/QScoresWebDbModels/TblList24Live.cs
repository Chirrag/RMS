using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblList24Live
{
    public int ItemId { get; set; }

    public int FkitemId { get; set; }

    public string EventTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? EventDate { get; set; }

    public string? Link { get; set; }
}
