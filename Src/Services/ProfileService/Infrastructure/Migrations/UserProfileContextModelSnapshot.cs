﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShareRecipe.Services.ProfileService.Infrastructure;

namespace ShareRecipe.Services.ProfileService.Infrastructure.Migrations
{
    [DbContext(typeof(UserProfileContext))]
    partial class UserProfileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.UserProfileAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.UserProfileAggregate", b =>
                {
                    b.OwnsOne("ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.UserProfile", "UserProfile", b1 =>
                        {
                            b1.Property<Guid>("UserProfileAggregateId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(300)
                                .HasColumnType("character varying(300)");

                            b1.Property<string>("Image")
                                .IsRequired()
                                .HasMaxLength(512)
                                .HasColumnType("character varying(512)");

                            b1.HasKey("UserProfileAggregateId");

                            b1.ToTable("UserProfiles");

                            b1.WithOwner()
                                .HasForeignKey("UserProfileAggregateId");
                        });

                    b.Navigation("UserProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
