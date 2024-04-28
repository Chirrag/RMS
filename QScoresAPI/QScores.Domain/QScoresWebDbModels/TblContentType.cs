using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblContentType
{
    public int ContentTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}
