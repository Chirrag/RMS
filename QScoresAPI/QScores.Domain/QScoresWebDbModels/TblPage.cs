using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class TblPage
{
    public int PageId { get; set; }

    public string Name { get; set; } = null!;

    public int TemplateId { get; set; }

    public int? FolderId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? Createdon { get; set; }

    public int? VersionId { get; set; }

    public DateTime? ScheduledTime { get; set; }
}
