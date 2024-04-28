using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblZipFile
{
    public int RecId { get; set; }

    public string ZipFile { get; set; } = null!;
}
