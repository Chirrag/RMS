using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblApplication
{
    public int RecId { get; set; }

    public string? Description { get; set; }

    public string? AppLocation { get; set; }

    public string? DbaseName { get; set; }

    public string? Application { get; set; }

    public string? SubApplication { get; set; }

    public bool IsTvq { get; set; }

    public bool? IsActive { get; set; }

    public bool? ShowInApp { get; set; }

    public int? ParentApplicationId { get; set; }
}
