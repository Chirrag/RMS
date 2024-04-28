using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblList25Live
{
    public int ItemId { get; set; }

    public string? Header { get; set; }

    public int FkitemId { get; set; }

    public DateTime? PublishDate { get; set; }

    public string Description { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? PubDisplay { get; set; }
}
