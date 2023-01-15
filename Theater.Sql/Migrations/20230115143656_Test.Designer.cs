﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Theater.Entities.Theater;
using Theater.Sql;

namespace Theater.Sql.Migrations
{
    [DbContext(typeof(TheaterDbContext))]
    [Migration("20230115143656_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Theater.Entities.Authorization.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<decimal>("Money")
                        .HasColumnType("numeric");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character(11)")
                        .IsFixedLength(true);

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2343d16-e610-4a73-a0f0-b9f63df511e6"),
                            BirthDate = new DateTime(2001, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfCreate = new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "icestormyy-admin@mail.ru",
                            FirstName = "Mikhail",
                            Gender = 1,
                            LastName = "Tolmachev",
                            MiddleName = "Evgenievich",
                            Money = 1000m,
                            Password = "E10ADC3949BA59ABBE56E057F20F883E",
                            Phone = "81094316687",
                            RoleId = 2,
                            UserName = "IceStormy-admin"
                        },
                        new
                        {
                            Id = new Guid("e1f83d38-56a7-435b-94bd-fe891ed0f03a"),
                            BirthDate = new DateTime(2001, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfCreate = new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "icestormyy-user@mail.ru",
                            FirstName = "Mikhail",
                            Gender = 1,
                            LastName = "Tolmachev",
                            MiddleName = "Evgenievich",
                            Money = 1000m,
                            Password = "E10ADC3949BA59ABBE56E057F20F883E",
                            Phone = "81094316687",
                            RoleId = 1,
                            UserName = "IceStormy-user"
                        });
                });

            modelBuilder.Entity("Theater.Entities.Authorization.UserRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "User"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("Theater.Entities.FileStorage.FileStorageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BucketId")
                        .HasColumnType("integer");

                    b.Property<string>("ContentType")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("FileStorageName")
                        .HasColumnType("text");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric(20,0)");

                    b.Property<DateTime>("UploadAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Theater.Entities.Theater.BookedTicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PiecesTicketId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PiecesTicketId");

                    b.HasIndex("UserId", "Id")
                        .IsUnique();

                    b.ToTable("BookedTickets");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceDateEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PieceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PieceId");

                    b.ToTable("PieceDates");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<Guid[]>("PhotoIds")
                        .HasColumnType("uuid[]");

                    b.Property<string>("PieceName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.ToTable("Pieces");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceWorkerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PieceId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TheaterWorkerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PieceId");

                    b.HasIndex("TheaterWorkerId");

                    b.ToTable("PieceWorkers");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PiecesGenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("PiecesGenres");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PiecesTicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PieceDateId")
                        .HasColumnType("uuid");

                    b.Property<int>("TicketPlace")
                        .HasColumnType("integer");

                    b.Property<int>("TicketPrice")
                        .HasColumnType("integer");

                    b.Property<int>("TicketRow")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PieceDateId");

                    b.ToTable("PiecesTickets");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PurchasedUserTicketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("TicketPriceEventsId")
                        .HasColumnType("uuid");

                    b.Property<int>("TicketPriceEventsVersion")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TicketPriceEventsVersion", "TicketPriceEventsId");

                    b.ToTable("PurchasedUserTickets");
                });

            modelBuilder.Entity("Theater.Entities.Theater.TheaterWorkerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid?>("PhotoId")
                        .HasColumnType("uuid");

                    b.Property<int>("PositionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.ToTable("TheaterWorkers");
                });

            modelBuilder.Entity("Theater.Entities.Theater.TicketPriceEventsEntity", b =>
                {
                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("PiecesTicketId")
                        .HasColumnType("uuid");

                    b.Property<PiecesTicketEntity>("Model")
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Version", "PiecesTicketId");

                    b.HasIndex("PiecesTicketId");

                    b.ToTable("TicketPriceEvents");
                });

            modelBuilder.Entity("Theater.Entities.Theater.UserReviewEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<Guid>("PieceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PieceId");

                    b.HasIndex("UserId");

                    b.ToTable("UserReviews");
                });

            modelBuilder.Entity("Theater.Entities.Theater.WorkersPositionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("PositionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("WorkersPositions");
                });

            modelBuilder.Entity("Theater.Entities.Authorization.UserEntity", b =>
                {
                    b.HasOne("Theater.Entities.Authorization.UserRoleEntity", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Theater.Entities.Theater.BookedTicketEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PiecesTicketEntity", "PiecesTicket")
                        .WithMany("BookedTickets")
                        .HasForeignKey("PiecesTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Theater.Entities.Authorization.UserEntity", "User")
                        .WithMany("BookedTickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PiecesTicket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceDateEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PieceEntity", "Piece")
                        .WithMany("PieceDates")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Piece");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PiecesGenreEntity", "Genre")
                        .WithMany("Pieces")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceWorkerEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PieceEntity", "Piece")
                        .WithMany("PieceWorkers")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Theater.Entities.Theater.TheaterWorkerEntity", "TheaterWorker")
                        .WithMany("PieceWorkers")
                        .HasForeignKey("TheaterWorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Piece");

                    b.Navigation("TheaterWorker");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PiecesTicketEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PieceDateEntity", "PieceDate")
                        .WithMany("PiecesTickets")
                        .HasForeignKey("PieceDateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PieceDate");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PurchasedUserTicketEntity", b =>
                {
                    b.HasOne("Theater.Entities.Authorization.UserEntity", "User")
                        .WithMany("PurchasedUserTickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Theater.Entities.Theater.TicketPriceEventsEntity", "TicketPriceEvents")
                        .WithMany("PurchasedUserTicket")
                        .HasForeignKey("TicketPriceEventsVersion", "TicketPriceEventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TicketPriceEvents");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Theater.Entities.Theater.TheaterWorkerEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.WorkersPositionEntity", "Position")
                        .WithMany("TheaterWorker")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Theater.Entities.Theater.TicketPriceEventsEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PiecesTicketEntity", "PiecesTicket")
                        .WithMany("TicketPriceEvents")
                        .HasForeignKey("PiecesTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PiecesTicket");
                });

            modelBuilder.Entity("Theater.Entities.Theater.UserReviewEntity", b =>
                {
                    b.HasOne("Theater.Entities.Theater.PieceEntity", "Piece")
                        .WithMany("Reviews")
                        .HasForeignKey("PieceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Theater.Entities.Authorization.UserEntity", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Piece");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Theater.Entities.Authorization.UserEntity", b =>
                {
                    b.Navigation("BookedTickets");

                    b.Navigation("PurchasedUserTickets");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Theater.Entities.Authorization.UserRoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceDateEntity", b =>
                {
                    b.Navigation("PiecesTickets");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PieceEntity", b =>
                {
                    b.Navigation("PieceDates");

                    b.Navigation("PieceWorkers");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PiecesGenreEntity", b =>
                {
                    b.Navigation("Pieces");
                });

            modelBuilder.Entity("Theater.Entities.Theater.PiecesTicketEntity", b =>
                {
                    b.Navigation("BookedTickets");

                    b.Navigation("TicketPriceEvents");
                });

            modelBuilder.Entity("Theater.Entities.Theater.TheaterWorkerEntity", b =>
                {
                    b.Navigation("PieceWorkers");
                });

            modelBuilder.Entity("Theater.Entities.Theater.TicketPriceEventsEntity", b =>
                {
                    b.Navigation("PurchasedUserTicket");
                });

            modelBuilder.Entity("Theater.Entities.Theater.WorkersPositionEntity", b =>
                {
                    b.Navigation("TheaterWorker");
                });
#pragma warning restore 612, 618
        }
    }
}
