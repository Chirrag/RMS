using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblWorkFlowItem
{
    public int Wflid { get; set; }

    public string Description { get; set; } = null!;

    public int AssignedTo { get; set; }

    public bool IsPublish { get; set; }

    public bool LockContent { get; set; }
}
