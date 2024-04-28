using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblClientPermission
{
    public int RecId { get; set; }

    public int GroupId { get; set; }

    public int ItemId { get; set; }

    public string Type { get; set; } = null!;
}
