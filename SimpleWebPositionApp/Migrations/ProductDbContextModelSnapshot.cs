﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleWebPositionApp.Data;

#nullable disable

namespace SimpleWebPositionApp.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("SimpleWebPositionApp.Models.CodeItem", b =>
                {
                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<string>("TopCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Barcode");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("SimpleWebPositionApp.Models.Dto.CensusItem", b =>
                {
                    b.Property<string>("TopCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Device")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Diff")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Logistics")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Scanned")
                        .HasColumnType("INTEGER");

                    b.HasKey("TopCode");

                    b.ToTable("Census");
                });

            modelBuilder.Entity("SimpleWebPositionApp.Models.Login", b =>
                {
                    b.Property<int>("Login_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login_Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("mode")
                        .HasColumnType("INTEGER");

                    b.HasKey("Login_id");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("SimpleWebPositionApp.Models.Product64", b =>
                {
                    b.Property<string>("TopCode")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance68")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("BalanceCentral")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CapacityCentral")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Monthly")
                        .HasColumnType("TEXT");

                    b.Property<string>("Position68")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PositionCentral")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Reserved68")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionLine")
                        .HasColumnType("INTEGER");

                    b.HasKey("TopCode");

                    b.ToTable("Products64");
                });

            modelBuilder.Entity("SimpleWebPositionApp.Models.Product68", b =>
                {
                    b.Property<string>("TopCode")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Balance68")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("BalanceCentral")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("CapacityCentral")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Monthly")
                        .HasColumnType("TEXT");

                    b.Property<string>("Position68")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PositionCentral")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Reserved68")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionLine")
                        .HasColumnType("INTEGER");

                    b.HasKey("TopCode");

                    b.ToTable("Products68");
                });

            modelBuilder.Entity("SimpleWebPositionApp.Models.SearchBar", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("TEXT");

                    b.HasKey("Code");

                    b.ToTable("SearchBar");
                });
#pragma warning restore 612, 618
        }
    }
}
