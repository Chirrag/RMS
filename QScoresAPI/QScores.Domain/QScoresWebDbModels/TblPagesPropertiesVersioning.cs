using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPagesPropertiesVersioning
{
    public int RecId { get; set; }

    public int VersionId { get; set; }

    public bool IsSecured { get; set; }

    public bool? IsSearchable { get; set; }

    public string PageTitle { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Keywords { get; set; } = null!;

    public string MenuLabel { get; set; } = null!;

    public bool InNav { get; set; }

    public string Url { get; set; } = null!;

    public int? Inx { get; set; }
}
