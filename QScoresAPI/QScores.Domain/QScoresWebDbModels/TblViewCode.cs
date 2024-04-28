using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblViewCode
{
    public int Vcid { get; set; }

    public int ViewId { get; set; }

    public string ColumnName { get; set; } = null!;

    public string Operator { get; set; } = null!;

    public string ValueFrom { get; set; } = null!;

    public string Value { get; set; } = null!;
}
