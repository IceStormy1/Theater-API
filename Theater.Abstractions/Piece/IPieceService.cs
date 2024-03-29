﻿using System;
using System.Threading.Tasks;
using Theater.Common;
using Theater.Contracts;
using Theater.Contracts.Theater.Piece;

namespace Theater.Abstractions.Piece;

public interface IPieceService : ICrudService<PieceParameters>
{
    /// <summary>
    /// Получить полную информацию о пьесе по идентификатору
    /// </summary>
    /// <returns>Полная информация о пьесе</returns>
    Task<Result<PieceModel>> GetPieceById(Guid pieceId);

    /// <summary>
    /// Обогатить модель
    /// </summary>
    Task EnrichPieceShortInformation(Page<PieceShortInformationModel> shortInformationModel);
}