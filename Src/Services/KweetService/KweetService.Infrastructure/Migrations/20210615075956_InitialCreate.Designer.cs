﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShareRecipe.Services.KweetService.Infrastructure;

namespace ShareRecipe.Services.KweetService.Infrastructure.Migrations
{
    [DbContext(typeof(KweetContext))]
    [Migration("20210615075956_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Kweet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Kweets");
                });

            modelBuilder.Entity("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.ProfileAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Kweet", b =>
                {
                    b.HasOne("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.ProfileAggregate", null)
                        .WithMany("Kweets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsMany("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Direction", "Directions", b1 =>
                        {
                            b1.Property<Guid>("KweetId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id1")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Message")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<int>("Order")
                                .HasMaxLength(50)
                                .HasColumnType("integer");

                            b1.HasKey("KweetId", "Id1");

                            b1.ToTable("Direction");

                            b1.WithOwner()
                                .HasForeignKey("KweetId");
                        });

                    b.OwnsMany("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Ingredient", "Ingredients", b1 =>
                        {
                            b1.Property<Guid>("KweetId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id1")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Amount")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.HasKey("KweetId", "Id1");

                            b1.ToTable("Ingredient");

                            b1.WithOwner()
                                .HasForeignKey("KweetId");
                        });

                    b.Navigation("Directions");

                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.ProfileAggregate", b =>
                {
                    b.Navigation("Kweets");
                });
#pragma warning restore 612, 618
        }
    }
}
