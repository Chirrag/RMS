﻿using System;
using System.Collections.Generic;

namespace QScores.Domain.QScoresWebDbModels;

public partial class CodeKey
{
    public double? StudyCode { get; set; }

    public string? StudyName { get; set; }

    public string? Type { get; set; }
}
