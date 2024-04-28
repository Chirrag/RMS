using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblPermission
{
    public int RecId { get; set; }

    public int UserId { get; set; }

    public int ApplicationId { get; set; }

    public int PermissionId { get; set; }

    public bool? Include { get; set; }

    public bool Exclude { get; set; }
}
