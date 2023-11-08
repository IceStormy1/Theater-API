﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Entities.Rooms;

namespace Theater.Sql.Configurations.Rooms;

internal sealed class UserRoomEntityConfiguration : IEntityTypeConfiguration<UserRoomEntity>
{
    public void Configure(EntityTypeBuilder<UserRoomEntity> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoomId });
        builder.Property(x => x.IsActive).HasDefaultValue(true);

        builder.ToTable(name: "UserRooms", schema: "chat");
    }
}