using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblList23Live
{
    public int ItemId { get; set; }

    public string? Header { get; set; }

    public string? Description { get; set; }

    public int FkitemId { get; set; }

    public string Product { get; set; } = null!;

    public DateTime? PostedDate { get; set; }
}
