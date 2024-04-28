using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblBlog
{
    public int BlogId { get; set; }

    public string BlogName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string TableName { get; set; } = null!;

    public bool IsSecured { get; set; }

    public string? Xslt { get; set; }

    public bool? Inherits { get; set; }
}
