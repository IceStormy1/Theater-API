﻿using System;
using System.Collections.Generic;

namespace Theater.Entities.Theater
{
    public class PiecesTicketEntity
    {
        /// <summary>
        /// Идентификатор билета
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ряд
        /// </summary>
        public ushort TicketRow { get; set; }

        /// <summary>
        /// Место
        /// </summary>
        public ushort TicketPlace { get; set; }

        /// <summary>
        /// Идентификатор даты начала пьесы
        /// </summary>
        public Guid PieceDateId { get; set; }

        /// <summary>
        /// Ссылка на дату начала пьесы
        /// </summary>
        public PieceDateEntity PieceDate { get; set; }

        public List<BookedTicketEntity> BookedTickets { get; set; }
        public List<TicketPriceEventsEntity> TicketPriceEvents { get; set; }
    }
}
