using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPageTemplate
{
    public int TemplateId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string PageUrl { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool IsDefault { get; set; }
}
