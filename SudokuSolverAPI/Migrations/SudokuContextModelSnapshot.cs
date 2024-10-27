﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SudokuSolverAPI.Data;

#nullable disable

namespace SudokuSolverAPI.Migrations
{
    [DbContext(typeof(SudokuContext))]
    partial class SudokuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SudokuSolverAPI.Models.SudokuSolution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Solution")
                        .IsRequired()
                        .HasMaxLength(81)
                        .HasColumnType("nvarchar(81)");

                    b.Property<DateTime>("SolvedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SudokuSolutions");
                });
#pragma warning restore 612, 618
        }
    }
}