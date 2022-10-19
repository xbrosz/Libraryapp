﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(LibraryappDbContext))]
    [Migration("20221017120756_TestMigration")]
    partial class TestMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2022, 10, 17, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8269),
                            FirstName = "Štefan",
                            LastName = "Hemingway",
                            MiddleName = ""
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2022, 10, 17, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8312),
                            FirstName = "Adrian",
                            LastName = "McKinty",
                            MiddleName = "Alfonz"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Release")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Release = new DateTime(2022, 10, 17, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8317),
                            Title = "Space"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Release = new DateTime(2022, 10, 17, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8319),
                            Title = "The chain"
                        });
                });

            modelBuilder.Entity("DAL.Entities.BookGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("GenreId");

                    b.ToTable("BookGenre");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            GenreId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            GenreId = 3
                        });
                });

            modelBuilder.Entity("DAL.Entities.BookPrint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("BranchId");

                    b.ToTable("BookPrint");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            BranchId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            BranchId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Branch");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "botanicka 68A",
                            Name = "Pobocka"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sci-fi"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Thriller"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RatingNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Rating");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            Comment = "Super",
                            RatingNumber = 4
                        });
                });

            modelBuilder.Entity("DAL.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookPrintId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookPrintId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservation");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookPrintId = 1,
                            EndDate = new DateTime(2022, 10, 21, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8322),
                            StartDate = new DateTime(2022, 10, 17, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8321),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            BookPrintId = 2,
                            EndDate = new DateTime(2022, 10, 19, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8326),
                            StartDate = new DateTime(2022, 10, 17, 14, 7, 56, 658, DateTimeKind.Local).AddTicks(8325),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Bratislavská 25",
                            Email = "peter@gmail.com",
                            FirstName = "Peter",
                            LastName = "Pavol",
                            Password = "123",
                            PhoneNumber = "4567890",
                            RoleId = 2,
                            UserName = "pp"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Bratislavská 21",
                            Email = "karlic@gmail.com",
                            FirstName = "Marek",
                            LastName = "Karlicky",
                            Password = "marekk",
                            PhoneNumber = "4567890",
                            RoleId = 1,
                            UserName = "mk"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Book", b =>
                {
                    b.HasOne("DAL.Entities.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("DAL.Entities.BookGenre", b =>
                {
                    b.HasOne("DAL.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("DAL.Entities.BookPrint", b =>
                {
                    b.HasOne("DAL.Entities.Book", "Book")
                        .WithMany("BookPrints")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("DAL.Entities.Rating", b =>
                {
                    b.HasOne("DAL.Entities.Book", null)
                        .WithMany("Ratings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.Reservation", b =>
                {
                    b.HasOne("DAL.Entities.BookPrint", "BookPrint")
                        .WithMany()
                        .HasForeignKey("BookPrintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookPrint");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.HasOne("DAL.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DAL.Entities.Book", b =>
                {
                    b.Navigation("BookPrints");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
