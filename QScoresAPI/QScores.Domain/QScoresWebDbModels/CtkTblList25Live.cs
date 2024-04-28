using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class CtkTblList25Live
{
    public int ItemId { get; set; }

    public string? Header { get; set; }

    public DateTime? PublishDate { get; set; }

    public string? Description { get; set; }

    public string? Body { get; set; }

    public string? PubDisplay { get; set; }

    public string? Link { get; set; }

    public int? Status { get; set; }
}
