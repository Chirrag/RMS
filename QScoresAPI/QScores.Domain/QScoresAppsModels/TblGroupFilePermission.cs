using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblGroupFilePermission
{
    public int RecId { get; set; }

    public int GroupId { get; set; }

    public int FileId { get; set; }
}
