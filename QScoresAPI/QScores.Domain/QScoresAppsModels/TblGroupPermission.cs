using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblGroupPermission
{
    public int RecId { get; set; }

    public int GroupId { get; set; }

    public int ApplicationId { get; set; }

    public int PermissionId { get; set; }
}
