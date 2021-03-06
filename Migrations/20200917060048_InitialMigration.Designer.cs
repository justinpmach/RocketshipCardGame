﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RocketShip.Models;

namespace RocketShip.Migrations
{
    [DbContext(typeof(RSContext))]
    [Migration("20200917060048_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RocketShip.Models.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DeckId");

                    b.Property<string>("StringVal");

                    b.Property<string>("Suit");

                    b.Property<int>("Val");

                    b.HasKey("CardId");

                    b.HasIndex("DeckId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("RocketShip.Models.Deck", b =>
                {
                    b.Property<int>("DeckId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("DeckId");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("RocketShip.Models.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username");

                    b.Property<int?>("rdFourCardCardId");

                    b.Property<int?>("rdOneCardCardId");

                    b.Property<int?>("rdThreeCardCardId");

                    b.Property<int?>("rdTwoCardCardId");

                    b.HasKey("PlayerId");

                    b.HasIndex("rdFourCardCardId");

                    b.HasIndex("rdOneCardCardId");

                    b.HasIndex("rdThreeCardCardId");

                    b.HasIndex("rdTwoCardCardId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("RocketShip.Models.Card", b =>
                {
                    b.HasOne("RocketShip.Models.Deck")
                        .WithMany("cards")
                        .HasForeignKey("DeckId");
                });

            modelBuilder.Entity("RocketShip.Models.Player", b =>
                {
                    b.HasOne("RocketShip.Models.Card", "rdFourCard")
                        .WithMany()
                        .HasForeignKey("rdFourCardCardId");

                    b.HasOne("RocketShip.Models.Card", "rdOneCard")
                        .WithMany()
                        .HasForeignKey("rdOneCardCardId");

                    b.HasOne("RocketShip.Models.Card", "rdThreeCard")
                        .WithMany()
                        .HasForeignKey("rdThreeCardCardId");

                    b.HasOne("RocketShip.Models.Card", "rdTwoCard")
                        .WithMany()
                        .HasForeignKey("rdTwoCardCardId");
                });
#pragma warning restore 612, 618
        }
    }
}
