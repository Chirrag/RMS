using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPageContent
{
    public int RecId { get; set; }

    public int Pgid { get; set; }

    public int ContId { get; set; }

    public string Pgloc { get; set; } = null!;

    public int PlaceOrder { get; set; }

    public string? Type { get; set; }
}
