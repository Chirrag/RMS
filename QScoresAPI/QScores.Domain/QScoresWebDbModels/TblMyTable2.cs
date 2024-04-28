using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblMyTable2
{
    public int RecId { get; set; }

    public string Name { get; set; } = null!;

    public int StudyCode { get; set; }

    public string StudyName { get; set; } = null!;

    public string? Type { get; set; }
}
