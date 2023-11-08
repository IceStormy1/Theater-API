﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Theater.Abstractions.Authorization.Models;
using Theater.Common.Enums;
using Theater.Entities.Users;

namespace Theater.Sql.Configurations.Users;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.Phone).HasMaxLength(11).IsFixedLength();
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.MiddleName).HasMaxLength(128);

        builder.Navigation(x => x.UserRole).AutoInclude();

        builder.HasIndex(x => x.VkId);

        builder.HasMany(s => s.UserReviews)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.BookedTickets)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.PurchasedUserTickets)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Photo)
            .WithOne(x => x.User)
            .HasForeignKey<UserEntity>(x => x.PhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.UserRooms)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(GetPrimaryUsersData());
    }

    private static IEnumerable<UserEntity> GetPrimaryUsersData()
        => new List<UserEntity>
        {
            new()
            {
                CreatedAt = new DateTime(2023, 01, 05),
                Email = "icestormyy-admin@mail.ru",
                FirstName = "Mikhail",
                LastName = "Tolmachev",
                MiddleName = "Evgenievich",
                Gender = GenderType.Male,
                Id = Guid.Parse("f2343d16-e610-4a73-a0f0-b9f63df511e6"),
                Phone = "81094316687",
                Password = "E10ADC3949BA59ABBE56E057F20F883E", // 123456
                RoleId = (int)UserRole.Admin,
                UserName = "IceStormy-admin",
                BirthDate = new DateTime(2001, 06, 06),
                Money = new decimal(1000.00)
            },
            new()
            {
                CreatedAt = new DateTime(2023, 01, 05),
                Email = "icestormyy-user@mail.ru",
                FirstName = "Mikhail",
                LastName = "Tolmachev",
                MiddleName = "Evgenievich",
                Gender = GenderType.Male,
                Id = Guid.Parse("e1f83d38-56a7-435b-94bd-fe891ed0f03a"),
                Phone = "81094316687",
                Password = "E10ADC3949BA59ABBE56E057F20F883E", // 123456
                RoleId = (int)UserRole.User,
                BirthDate = new DateTime(2001, 06, 06),
                UserName = "IceStormy-user",
                Money = new decimal(1000.00)
            },
            new()
            {
                CreatedAt = new DateTime(2023, 01, 05),
                Email = "shadow-theater@mail.ru",
                FirstName = "Администрация",
                LastName = "Театра",
                Gender = GenderType.Male,
                Phone = "81094316687",
                Id = Guid.Parse("cd448464-2ec0-4b21-b5fa-9a3cc8547489"),
                Password = "E10ADC3949BA59ABBE56E057F20F883E", // 123456
                RoleId = (int)UserRole.System,
                BirthDate = new DateTime(2001, 06, 06),
                UserName = "SystemUser",
            }
        };
}