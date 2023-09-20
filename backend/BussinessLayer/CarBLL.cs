using SharedLayer.Dto;
using SharedLayer.Interface;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class CarBLL : ICarBLL
    {
        private readonly IUnitOfWork uow;
        public CarBLL(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        //Add car
        public async Task<bool> AddCar(AddCarDTO car)
        {
            var newCar = new Car
            {
                Maker = car.Maker,
                Model = car.Model,
                RentPrice = car.RentPrice,
                AvailibilityStatus = car.AvailibilityStatus,
            };

            var result = await uow.CarRepository.AddCar(newCar);
            await uow.SaveAsync();
            return result;
        }

        //get car details
        public async Task<Car> CarDetails(int carId)
        {
            var result = await uow.CarRepository.CarDetails(carId);
            if(result==null)
            {
                return null;
            }

            return result;
        }


        // delete car with agreement --- admin
        public async Task<Car> DeleteCar(int carId)
        {
            var result = await uow.CarRepository.DeleteCar(carId);
            await uow.SaveAsync();
            return result;
        }

        //Edit car details
        public async Task<Car> EditCar(int carId, UpdateCarDTO updatedCar)
        {
            var newCar = new Car
            {
                CarId = carId,
                Maker = updatedCar.Maker,
                Model = updatedCar.Model,
                RentPrice = updatedCar.RentPrice,
                AvailibilityStatus = updatedCar.AvailibilityStatus,
            };

            var result = await uow.CarRepository.EditCar(carId, newCar);
            await uow.SaveAsync();

            return result;
        }

        //get all cars
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var result = await uow.CarRepository.GetAllCar();
            var list = result.ToList();
            return list;

        }
    }
}
