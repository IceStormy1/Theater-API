﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Entities.Theater;

namespace Theater.Sql.Configurations.Pieces;

internal sealed class PieceDateEntityConfiguration : IEntityTypeConfiguration<PieceDateEntity>
{
    public void Configure(EntityTypeBuilder<PieceDateEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(s => s.PiecesTickets)
            .WithOne(x => x.PieceDate)
            .HasForeignKey(x => x.PieceDateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}