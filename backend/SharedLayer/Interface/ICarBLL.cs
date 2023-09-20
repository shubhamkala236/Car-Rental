using SharedLayer.Dto;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Interface
{
    public interface ICarBLL
    {
        //get all cars
        Task<IEnumerable<Car>> GetAllCars();
        //get Car Detail by Id
        Task<Car> CarDetails(int carId);

        //add car
        Task<bool> AddCar(AddCarDTO car);

        //delete car
        Task<Car> DeleteCar(int carId);

        //edit car
        Task<Car> EditCar(int carId,UpdateCarDTO updatedCar);
    }
}
