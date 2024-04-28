using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class VwMediaResource
{
    public int ItemId { get; set; }

    public string? Header { get; set; }

    public string? Body { get; set; }
}
