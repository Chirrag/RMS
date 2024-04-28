using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblForum
{
    public int ForumId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsSecured { get; set; }

    public bool IsMonitored { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool? IsOpen { get; set; }

    public string Status { get; set; } = null!;
}
