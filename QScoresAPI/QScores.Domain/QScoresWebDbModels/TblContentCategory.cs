using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblContentCategory
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Parent { get; set; }

    public bool? Inherits { get; set; }

    public bool ViewAllFiles { get; set; }

    public int? FolderType { get; set; }

    public int? Inx { get; set; }

    public bool? IsInternalLink { get; set; }

    public string? Url { get; set; }
}
