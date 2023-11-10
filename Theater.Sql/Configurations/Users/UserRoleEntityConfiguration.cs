﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Entities.Users;

namespace Theater.Sql.Configurations.Users;

internal sealed class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RoleName).IsRequired().HasMaxLength(64);
       
        builder.HasMany(s => s.Users)
            .WithOne(x => x.UserRole)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(GetPrimaryRolesData());
    }

    private static IEnumerable<UserRoleEntity> GetPrimaryRolesData()
        => new List<UserRoleEntity>
        {
            new()
            {
                Id = 1,
                RoleName = "User"
            },
            new()
            {
                Id = 2,
                RoleName = "Admin"
            },
            new()
            {
                Id = 3,
                RoleName = "System"
            }
        };
}