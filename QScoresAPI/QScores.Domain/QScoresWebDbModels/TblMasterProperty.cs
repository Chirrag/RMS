using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblMasterProperty
{
    public int PropertyId { get; set; }

    public string Name { get; set; } = null!;

    public int BitwiseValue { get; set; }

    public string Scriptname { get; set; } = null!;

    public string? Type { get; set; }
}
