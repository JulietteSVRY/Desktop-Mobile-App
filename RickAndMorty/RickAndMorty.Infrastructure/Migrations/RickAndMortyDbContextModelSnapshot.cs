﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RickAndMorty.Infrastructure.Database;

#nullable disable

namespace RickAndMorty.Infrastructure.Migrations
{
    [DbContext(typeof(RickAndMortyDbContext))]
    partial class RickAndMortyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("RickAndMorty.Model.Entity.Liked", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ChangedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CheatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("ReferenceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id")
                        .HasName("PK_LikedId");

                    b.HasIndex("Type", "ReferenceId")
                        .IsUnique()
                        .HasDatabaseName("Index_Type_And_ReferenceId");

                    b.HasIndex("Type", "ReferenceId", "IsLiked")
                        .IsUnique()
                        .HasDatabaseName("Index_Type_And_ReferenceId_And_IsLiked");

                    b.ToTable("Likeds");
                });

            modelBuilder.Entity("RickAndMorty.Model.Entity.User", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("ChangedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CheatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLogin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id")
                        .HasName("PK_UserId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
