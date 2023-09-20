using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Interface
{
    public interface ICarRepository
    {
        //get all cars
        Task<IEnumerable<Car>> GetAllCar();
        //get car details
        Task<Car> CarDetails(int carId);

        //add car
        Task<bool> AddCar(Car car);

        //delete car
        Task<Car> DeleteCar(int carId);

        //edit
        Task<Car> EditCar(int carId,Car updatedCar);
    }
}
