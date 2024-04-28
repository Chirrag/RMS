using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblItemComment
{
    public int CommentId { get; set; }

    public string Comment { get; set; } = null!;

    public int PostedBy { get; set; }

    public DateTime PostedOn { get; set; }

    public int ItemId { get; set; }

    public string ItemType { get; set; } = null!;
}
