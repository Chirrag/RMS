using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPagerelatedItemsVersioning
{
    public int RecId { get; set; }

    public int VersionId { get; set; }

    public string ItemType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Link { get; set; } = null!;
}
