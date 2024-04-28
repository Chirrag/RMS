using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblItemPropertiesLive
{
    public int RecId { get; set; }

    public int ItemId { get; set; }

    public string Tag { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Type { get; set; } = null!;
}
