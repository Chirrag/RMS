using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblWorkFlowAssignment
{
    public int AssignmentId { get; set; }

    public int Wflid { get; set; }

    public string Type { get; set; } = null!;

    public int ItemId { get; set; }
}
