using Microsoft.EntityFrameworkCore;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class CarRentDbContext : DbContext
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options):base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<RentalAgreement> RentalAgreements { get; set; }
        public DbSet<Car> Cars { get; set; }
        //public DbSet<AcceptedAgreement> AcceptedAgreements { get; set; }

        //seeding 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //DbInitializer.InitializeData(this);
            modelBuilder.Entity<User>().HasData(
                    new User { UserId =1,Name = "Rohan", Email = "rohan@gmail.com", Password = "rohan123", IsAdmin = false },
                    new User { UserId =2, Name = "Shubham", Email = "shubham@gmail.com", Password = "shubham123", IsAdmin = true },
                    new User { UserId =3, Name = "Raj", Email = "raj@gmail.com", Password = "raj123", IsAdmin = false },
                    new User { UserId =4, Name = "Ramesh", Email = "ramesh@gmail.com", Password = "ramesh123", IsAdmin = false },
                    new User { UserId =5, Name = "Admin", Email = "admin@gmail.com", Password = "admin123", IsAdmin = true }
               );
            modelBuilder.Entity<Car>().HasData(
                    new Car { CarId=1,Maker = "Honda", Model = "ASHD6SJ2HSK", RentPrice = 3000, AvailibilityStatus = "available" },
                    new Car { CarId = 2, Maker = "Maruti Suzuki", Model = "HASDJJ2JJSS", RentPrice = 2000, AvailibilityStatus = "available" },
                    new Car { CarId = 3,Maker = "BMW", Model = "SKO29JSJKFSS", RentPrice = 8000, AvailibilityStatus = "available" },
                    new Car { CarId = 4,Maker = "Mercedes", Model = "QOSJSFMKDLSS", RentPrice = 9000, AvailibilityStatus = "available" },
                    new Car { CarId = 5,Maker = "Porche", Model = "ZAKSKDSOSSLD", RentPrice = 15000, AvailibilityStatus = "available" }
                );
        }

    }
}
