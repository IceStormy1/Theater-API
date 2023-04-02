﻿using System;

namespace Theater.Contracts.Theater
{
    public class PieceDateParameters
    {
        /// <summary>
        /// Дата и время пьесы
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор пьесы
        /// </summary>
        public Guid PieceId { get; set; }
    }
}