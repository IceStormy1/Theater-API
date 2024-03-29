﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Entities.Theater;

namespace Theater.Sql.Configurations.Tickets;

internal sealed class TicketPriceEventsEntityConfiguration : IEntityTypeConfiguration<TicketPriceEventsEntity>
{
    public void Configure(EntityTypeBuilder<TicketPriceEventsEntity> builder)
    {
        builder.HasKey(x => new { x.Version, x.PiecesTicketId });
        builder.Property(x => x.Model).HasColumnType("jsonb");
        builder.Property(x => x.Version).ValueGeneratedNever();
        builder.Property(x => x.PiecesTicketId).ValueGeneratedNever();

        builder.HasOne(s => s.PurchasedUserTicket)
            .WithOne(x => x.TicketPriceEvents)
            .HasForeignKey<PurchasedUserTicketEntity>(x => new { x.TicketPriceEventsVersion, x.TicketPriceEventsId })
            .OnDelete(DeleteBehavior.Cascade);
    }
}