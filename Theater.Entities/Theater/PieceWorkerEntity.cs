﻿using System;

namespace Theater.Entities.Theater
{
    public class PieceWorkerEntity
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор работника театра 
        /// </summary>
        public Guid TheaterWorkerId { get; set; }

        /// <summary>
        /// Ссылка на работника театра
        /// </summary>
        public TheaterWorkerEntity TheaterWorker { get; set; }

        /// <summary>
        /// Идентификатор репертуара
        /// </summary>
        public Guid PieceId { get; set; }

        /// <summary>
        /// Ссылка на репертуар
        /// </summary>
        public PieceDateEntity PieceDate { get; set; }
    }
}