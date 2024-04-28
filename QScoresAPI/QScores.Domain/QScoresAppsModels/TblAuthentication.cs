using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresAppsModels;

public partial class TblAuthentication
{
    public int RecId { get; set; }

    public string? FullName { get; set; }

    public string? Password { get; set; }

    public string? EmailAddress { get; set; }

    public string? OfficePhone { get; set; }

    public string? CellPhone { get; set; }

    public int? GroupId { get; set; }

    public byte[]? Picture { get; set; }

    public DateTime? AuditDate { get; set; }
}
