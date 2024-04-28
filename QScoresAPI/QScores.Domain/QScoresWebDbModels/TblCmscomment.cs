using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblCmscomment
{
    public int CommentId { get; set; }

    public int ItemId { get; set; }

    public string Notes { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }
}
