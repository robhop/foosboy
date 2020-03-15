﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend;
using backend.Models;

namespace backend.Migrations
{
    [DbContext(typeof(FoosBoyContex))]
    [Migration("20200315132347_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("backend.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("backend.Play", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Result")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("backend.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Avatar")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("backend.Play", b =>
                {
                    b.HasOne("backend.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId");

                    b.HasOne("backend.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });
#pragma warning restore 612, 618
        }
    }
}
