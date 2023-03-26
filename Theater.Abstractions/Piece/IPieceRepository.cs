﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theater.Abstractions.Piece.Models;
using Theater.Entities.Theater;

namespace Theater.Abstractions.Piece
{
    public interface IPieceRepository : ICrudRepository<PieceEntity>
    {
        /// <summary>
        /// Получить краткую информацию об актуальных пьесах
        /// </summary>
        Task<IReadOnlyCollection<PieceShortInformationDto>> GetPiecesShortInformation();

        /// <summary>
        /// Получить полную информацию о пьесе по идентификатору
        /// </summary>
        /// <returns>Полная информация о пьесе</returns>
        Task<PieceDto> GetPieceDtoById(Guid pieceId);
    }
}
