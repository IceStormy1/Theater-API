﻿using System;

namespace Theater.Contracts.Theater.PieceWorker;

public class PieceWorkerParameters
{
    /// <summary>
    /// Идентификатор работника театра 
    /// </summary>
    public Guid TheaterWorkerId { get; set; }

    /// <summary>
    /// Идентификатор репертуара
    /// </summary>
    public Guid PieceId { get; set; }
}