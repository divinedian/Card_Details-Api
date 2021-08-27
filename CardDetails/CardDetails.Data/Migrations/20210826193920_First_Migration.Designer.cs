﻿// <auto-generated />
using System;
using CardDetails.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CardDetails.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210826193920_First_Migration")]
    partial class First_Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("CardDetails.Data.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("CardDetails.Data.Models.CardDetail", b =>
                {
                    b.Property<int>("Bin")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BankId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Brand")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NumberId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Prepaid")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Scheme")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Bin");

                    b.HasIndex("BankId");

                    b.HasIndex("CountryId");

                    b.HasIndex("NumberId");

                    b.ToTable("CardDetails");
                });

            modelBuilder.Entity("CardDetails.Data.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Alpha2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<string>("Emoji")
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Numeric")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CardDetails.Data.Models.Number", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Luhn")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Numbers");
                });

            modelBuilder.Entity("CardDetails.Data.Models.CardDetail", b =>
                {
                    b.HasOne("CardDetails.Data.Models.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankId");

                    b.HasOne("CardDetails.Data.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("CardDetails.Data.Models.Number", "Number")
                        .WithMany()
                        .HasForeignKey("NumberId");

                    b.Navigation("Bank");

                    b.Navigation("Country");

                    b.Navigation("Number");
                });
#pragma warning restore 612, 618
        }
    }
}
