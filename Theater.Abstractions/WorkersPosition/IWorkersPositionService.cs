﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Theater.Common;
using Theater.Contracts.Theater;

namespace Theater.Abstractions.WorkersPosition;

public interface IWorkersPositionService : ICrudService<WorkersPositionParameters>
{
    /// <summary>
    /// Возвращает все должности работников театра
    /// </summary>
    /// <param name="positionType">
    /// Тип должности. Опциональный параметр
    /// </param>
    Task<IReadOnlyCollection<WorkersPositionModel>> GetWorkerPositions(PositionType? positionType);
}