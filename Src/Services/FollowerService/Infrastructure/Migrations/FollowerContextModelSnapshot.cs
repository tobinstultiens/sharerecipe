﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShareRecipe.Services.FollowerService.Infrastructure;

namespace ShareRecipe.Services.FollowerService.Infrastructure.Migrations
{
    [DbContext(typeof(FollowerContext))]
    partial class FollowerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ShareRecipe.Services.FollowerService.Domain.Follower", b =>
                {
                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowingId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("FollowedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("FollowerId", "FollowingId");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follower");
                });

            modelBuilder.Entity("ShareRecipe.Services.FollowerService.Domain.ProfileAggregate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("ShareRecipe.Services.FollowerService.Domain.Follower", b =>
                {
                    b.HasOne("ShareRecipe.Services.FollowerService.Domain.ProfileAggregate", null)
                        .WithMany("Followers")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ShareRecipe.Services.FollowerService.Domain.ProfileAggregate", null)
                        .WithMany("Following")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("ShareRecipe.Services.FollowerService.Domain.ProfileAggregate", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Following");
                });
#pragma warning restore 612, 618
        }
    }
}