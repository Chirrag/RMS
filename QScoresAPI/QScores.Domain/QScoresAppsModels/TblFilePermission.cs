using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblFilePermission
{
    public int RecId { get; set; }

    public int UserId { get; set; }

    public int FileId { get; set; }

    public bool? Include { get; set; }

    public bool Exclude { get; set; }
}
