using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblThread
{
    public int ThreadId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int ForumId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string TableName { get; set; } = null!;

    public bool? IsOpen { get; set; }

    public string Status { get; set; } = null!;
}
