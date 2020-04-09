﻿// <auto-generated />
using System;
using Betto.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Betto.DataAccessLayer.Migrations
{
    [DbContext(typeof(BettoDbContext))]
    [Migration("20200402112811_AddUsersTable")]
    partial class AddUsersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Betto.Model.Entities.CoverageEntity", b =>
                {
                    b.Property<int>("CoverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<bool>("Odds")
                        .HasColumnType("bit");

                    b.Property<bool>("Players")
                        .HasColumnType("bit");

                    b.Property<bool>("Predictions")
                        .HasColumnType("bit");

                    b.Property<bool>("Standings")
                        .HasColumnType("bit");

                    b.Property<bool>("TopScorers")
                        .HasColumnType("bit");

                    b.HasKey("CoverageId");

                    b.HasIndex("LeagueId")
                        .IsUnique();

                    b.ToTable("Coverages");
                });

            modelBuilder.Entity("Betto.Model.Entities.FixturesEntity", b =>
                {
                    b.Property<int>("FixturesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CoverageId")
                        .HasColumnType("int");

                    b.Property<bool>("Events")
                        .HasColumnType("bit");

                    b.Property<bool>("Lineups")
                        .HasColumnType("bit");

                    b.Property<bool>("PlayerStatistics")
                        .HasColumnType("bit");

                    b.Property<bool>("Statistics")
                        .HasColumnType("bit");

                    b.HasKey("FixturesId");

                    b.HasIndex("CoverageId")
                        .IsUnique();

                    b.ToTable("Fixtures");
                });

            modelBuilder.Entity("Betto.Model.Entities.LeagueEntity", b =>
                {
                    b.Property<int>("LeagueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CountryCode")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Flag")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<bool>("IsCurrent")
                        .HasColumnType("bit");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Season")
                        .HasColumnType("int");

                    b.Property<DateTime>("SeasonEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SeasonStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Standings")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("LeagueId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("Betto.Model.Entities.TeamEntity", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Founded")
                        .HasColumnType("int");

                    b.Property<string>("IsNational")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("TeamId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Betto.Model.Entities.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Betto.Model.Entities.VenueEntity", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Surface")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("VenueId");

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("Betto.Model.Entities.CoverageEntity", b =>
                {
                    b.HasOne("Betto.Model.Entities.LeagueEntity", "League")
                        .WithOne("Coverage")
                        .HasForeignKey("Betto.Model.Entities.CoverageEntity", "LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Betto.Model.Entities.FixturesEntity", b =>
                {
                    b.HasOne("Betto.Model.Entities.CoverageEntity", "Coverage")
                        .WithOne("Fixtures")
                        .HasForeignKey("Betto.Model.Entities.FixturesEntity", "CoverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Betto.Model.Entities.TeamEntity", b =>
                {
                    b.HasOne("Betto.Model.Entities.LeagueEntity", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Betto.Model.Entities.VenueEntity", b =>
                {
                    b.HasOne("Betto.Model.Entities.TeamEntity", "Team")
                        .WithOne("Venue")
                        .HasForeignKey("Betto.Model.Entities.VenueEntity", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
