﻿// <auto-generated />
using System;
using BrosMed_Insurance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrosMed_Insurance.Migrations.ReservationDb
{
    [DbContext(typeof(ReservationDbContext))]
    [Migration("20240507152451_wizyta")]
    partial class wizyta
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Finalizacja", b =>
                {
                    b.Property<int>("FinalizacjaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FinalizacjaId"));

                    b.Property<int>("TerminyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FinalizacjaId");

                    b.HasIndex("TerminyId");

                    b.ToTable("Finalizacja");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Godzina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GodzinaId")
                        .HasColumnType("int");

                    b.Property<string>("godzinaVM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GodzinaId");

                    b.ToTable("Godziny");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Terminy", b =>
                {
                    b.Property<int>("TerminyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TerminyId"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("UslugaId")
                        .HasColumnType("int");

                    b.Property<int>("godzinaId")
                        .HasColumnType("int");

                    b.HasKey("TerminyId");

                    b.HasIndex("UslugaId");

                    b.HasIndex("godzinaId");

                    b.ToTable("Terminy");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Usluga", b =>
                {
                    b.Property<int>("UslugaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UslugaId"));

                    b.Property<string>("Cena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UslugaId");

                    b.ToTable("Usluga");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Finalizacja", b =>
                {
                    b.HasOne("BrosMed_Insurance.Models.Reservation.Terminy", "Terminy")
                        .WithMany()
                        .HasForeignKey("TerminyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Terminy");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Godzina", b =>
                {
                    b.HasOne("BrosMed_Insurance.Models.Reservation.Godzina", null)
                        .WithMany("Godzinki")
                        .HasForeignKey("GodzinaId");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Terminy", b =>
                {
                    b.HasOne("BrosMed_Insurance.Models.Reservation.Usluga", "Usluga")
                        .WithMany()
                        .HasForeignKey("UslugaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrosMed_Insurance.Models.Reservation.Godzina", "godzina")
                        .WithMany()
                        .HasForeignKey("godzinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usluga");

                    b.Navigation("godzina");
                });

            modelBuilder.Entity("BrosMed_Insurance.Models.Reservation.Godzina", b =>
                {
                    b.Navigation("Godzinki");
                });
#pragma warning restore 612, 618
        }
    }
}
