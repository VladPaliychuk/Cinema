﻿// <auto-generated />
using System;
using Catalog.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.DAL.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20240427172525_Update2")]
    partial class Update2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Catalog.DAL.Entities.Actor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("ImageFile")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("text");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.ProductActor", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uuid");

                    b.HasKey("ProductId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("ProductActors");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.ProductGenre", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uuid");

                    b.HasKey("ProductId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("ProductGenres");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Screening", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StartTime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Screenings");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Seat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("boolean");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Row")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ScreeningId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ScreeningId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.ProductActor", b =>
                {
                    b.HasOne("Catalog.DAL.Entities.Actor", "Actor")
                        .WithMany("ProductActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.DAL.Entities.Product", "Product")
                        .WithMany("ProductActors")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.ProductGenre", b =>
                {
                    b.HasOne("Catalog.DAL.Entities.Genre", "Genre")
                        .WithMany("ProductsGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.DAL.Entities.Product", "Product")
                        .WithMany("ProductGenres")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Screening", b =>
                {
                    b.HasOne("Catalog.DAL.Entities.Product", "Product")
                        .WithMany("Screenings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Seat", b =>
                {
                    b.HasOne("Catalog.DAL.Entities.Screening", "Screening")
                        .WithMany("Seats")
                        .HasForeignKey("ScreeningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Screening");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Actor", b =>
                {
                    b.Navigation("ProductActors");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Genre", b =>
                {
                    b.Navigation("ProductsGenres");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Product", b =>
                {
                    b.Navigation("ProductActors");

                    b.Navigation("ProductGenres");

                    b.Navigation("Screenings");
                });

            modelBuilder.Entity("Catalog.DAL.Entities.Screening", b =>
                {
                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}
