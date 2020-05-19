﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SteelLakeGameListAPI.Domain;

namespace SteelLakeGameListAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200519220050_Test for foreign key changes")]
    partial class Testforforeignkeychanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SteelLakeGameListAPI.Domain.Expansion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Expansions");
                });

            modelBuilder.Entity("SteelLakeGameListAPI.Domain.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<string>("GameLength")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<int?>("MaxNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<int>("MinNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RecommendedNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("SteelLakeGameListAPI.Domain.Mod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1500)")
                        .HasMaxLength(1500);

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameLength")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<int?>("MinNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<int?>("RecommendedNumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Mods");
                });

            modelBuilder.Entity("SteelLakeGameListAPI.Domain.Expansion", b =>
                {
                    b.HasOne("SteelLakeGameListAPI.Domain.Game", null)
                        .WithMany("Expansions")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("SteelLakeGameListAPI.Domain.Mod", b =>
                {
                    b.HasOne("SteelLakeGameListAPI.Domain.Game", "Game")
                        .WithMany("Mods")
                        .HasForeignKey("GameId");
                });
#pragma warning restore 612, 618
        }
    }
}
