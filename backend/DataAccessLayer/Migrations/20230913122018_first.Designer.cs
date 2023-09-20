﻿// <auto-generated />
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(CarRentDbContext))]
    [Migration("20230913122018_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SharedLayer.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AvailibilityStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Maker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentPrice")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            CarId = 1,
                            AvailibilityStatus = true,
                            Maker = "Honda",
                            Model = "ASHD6SJ2HSK",
                            RentPrice = 3000
                        },
                        new
                        {
                            CarId = 2,
                            AvailibilityStatus = true,
                            Maker = "Maruti Suzuki",
                            Model = "HASDJJ2JJSS",
                            RentPrice = 2000
                        },
                        new
                        {
                            CarId = 3,
                            AvailibilityStatus = true,
                            Maker = "BMW",
                            Model = "SKO29JSJKFSS",
                            RentPrice = 8000
                        },
                        new
                        {
                            CarId = 4,
                            AvailibilityStatus = true,
                            Maker = "Mercedes",
                            Model = "QOSJSFMKDLSS",
                            RentPrice = 9000
                        },
                        new
                        {
                            CarId = 5,
                            AvailibilityStatus = true,
                            Maker = "Porche",
                            Model = "ZAKSKDSOSSLD",
                            RentPrice = 15000
                        });
                });

            modelBuilder.Entity("SharedLayer.Models.RentalAgreement", b =>
                {
                    b.Property<int>("AgreementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReturnRequested")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<int>("RentalDuration")
                        .HasColumnType("int");

                    b.Property<int>("TotalCost")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AgreementId");

                    b.ToTable("RentalAgreements");
                });

            modelBuilder.Entity("SharedLayer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "rohan@gmail.com",
                            IsAdmin = false,
                            Name = "Rohan",
                            Password = "rohan123"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "shubham@gmail.com",
                            IsAdmin = true,
                            Name = "Shubham",
                            Password = "shubham123"
                        },
                        new
                        {
                            UserId = 3,
                            Email = "raj@gmail.com",
                            IsAdmin = false,
                            Name = "Raj",
                            Password = "raj123"
                        },
                        new
                        {
                            UserId = 4,
                            Email = "ramesh@gmail.com",
                            IsAdmin = false,
                            Name = "Ramesh",
                            Password = "ramesh123"
                        },
                        new
                        {
                            UserId = 5,
                            Email = "admin@gmail.com",
                            IsAdmin = true,
                            Name = "Admin",
                            Password = "admin123"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
