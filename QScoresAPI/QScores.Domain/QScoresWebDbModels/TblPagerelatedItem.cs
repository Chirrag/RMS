using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPagerelatedItem
{
    public int RecId { get; set; }

    public int PageId { get; set; }

    public string ItemType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Link { get; set; } = null!;
}
