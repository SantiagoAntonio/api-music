﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_music;

#nullable disable

namespace api_music.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230729235935_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api_music.Entities.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });
#pragma warning restore 612, 618
        }
    }
}