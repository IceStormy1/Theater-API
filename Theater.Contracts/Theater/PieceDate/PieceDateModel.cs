﻿using System;

namespace Theater.Contracts.Theater.PieceDate;

public class PieceDateModel : PieceDateParameters
{
    /// <summary>
    /// Идентификатор даты пьесы
    /// </summary>
    public Guid Id { get; set; }
}