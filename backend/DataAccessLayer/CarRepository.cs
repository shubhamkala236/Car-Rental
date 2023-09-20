using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Interface;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentDbContext context;
        public CarRepository(CarRentDbContext context)
        {
            this.context = context;
        }

        //add car
        public async Task<bool> AddCar(Car car)
        {
            bool alreadyPresent = await context.Cars.AnyAsync(x => x.Model == car.Model);
            if(alreadyPresent)
            {
                return false;
            }

            await context.Cars.AddAsync(car);
            return true;
        }

        //get car details
        public async Task<Car> CarDetails(int carId)
        {
            var car = await context.Cars.FindAsync(carId);
            return car;
        }

        //delete car along with its agreement
        public async Task<Car> DeleteCar(int carId)
        {
            var car = await context.Cars.FindAsync(carId);
            if(car==null)
            {
                return null;
            }
            //find related agreements with that car
            var agreements = await context.RentalAgreements.Where(a => a.CarId == carId).ToListAsync();

            context.Cars.Remove(car);
            context.RentalAgreements.RemoveRange(agreements);

            return car;
        }

      
        public async Task<Car> EditCar(int carId,Car updatedCar)
        {
            //find old car
            var car = await context.Cars.FindAsync(carId);
            if(car==null)
            {
                return null;
            }

            //find related agreements with that car to update agreeement total cost
            var agreements = await context.RentalAgreements.Where(a => a.CarId == carId).ToListAsync();

            //update car in Db
            car.Maker = updatedCar.Maker;
            car.Model = updatedCar.Model;
            car.RentPrice = updatedCar.RentPrice;
            car.AvailibilityStatus = updatedCar.AvailibilityStatus;

            //update agreement totalcost
            foreach(var agreement in agreements)
            {
                agreement.TotalCost = car.RentPrice * agreement.RentalDuration;
            }

            return updatedCar;
        }

        //get all cars
        public async Task<IEnumerable<Car>> GetAllCar()
        {
            var cars = await context.Cars.ToListAsync();
            return cars;
        }
    }
}
