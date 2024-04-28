using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblApplicationControl
{
    public int ControlId { get; set; }

    public string Description { get; set; } = null!;

    public string ControlName { get; set; } = null!;

    public bool IsCustom { get; set; }

    public string Type { get; set; } = null!;

    public string Path { get; set; } = null!;
}
