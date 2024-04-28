using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class Master
{
    public double? RecId { get; set; }

    public string? Name { get; set; }

    public double? StudyCode { get; set; }

    public string? StudyName { get; set; }

    public string? Type { get; set; }

    public string? F6 { get; set; }

    public string? F7 { get; set; }
}
