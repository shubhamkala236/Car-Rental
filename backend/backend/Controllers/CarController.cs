using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dto;
using SharedLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarController : ControllerBase
    {
        private readonly ICarBLL carBLL;
        public CarController(ICarBLL carBLL)
        {
            this.carBLL = carBLL;
        }

        //Get All Cars
        [HttpGet("allCars")]
        //[AllowAnonymous] any logged in user
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var response = await carBLL.GetAllCars();
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Error fetching Cars:{e.Message}");
            }
        }

        //Get Car Details
        [HttpGet("carDetails/{carId}")]
        //[AllowAnonymous] any logged in user
        public async Task<IActionResult> CarDetails(int carId)
        {
            try
            {
                var response = await carBLL.CarDetails(carId);
                if(response!=null)
                {
                    return Ok(response);
                }
                return StatusCode(404, $"No Car with detail found");
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to fetch Car Details:{e.Message}");
            }
        }

        //Add Cars ---admin
        [HttpPost("addCar")]
        [Authorize(Policy = "AdminOnly")] //admin only
        public async Task<IActionResult> AddCar([FromForm] AddCarDTO car)
        {
            try
            {
                var result = await carBLL.AddCar(car);
                if (result)
                {
                    return Ok("Car Added Successfully");
                }

                return StatusCode(409, $"Unable to Add Car");
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to Add Car:{e.Message}");
            }
            
        }

        //delete car ---admin --- also delete all agreements with that car
        [HttpDelete("delete/{carId}")]
        [Authorize(Policy = "AdminOnly")] //admin only
        public async Task<IActionResult> DeleteCar(int carId)
        {
            try
            {
                var response = await carBLL.DeleteCar(carId);
                if(response!=null)
                {
                    return Ok($"Product Deleted");
                }

                return NotFound("Product not found");
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to Delete Car:{e.Message}");
            }
        }
        //Edit Car Details ---admin
        [HttpPut("edit/{carId}")]
        [Authorize(Policy = "AdminOnly")] //admin only
        public async Task<IActionResult> EditCar(int carId,[FromForm]UpdateCarDTO car)
        {
            try
            {
                var result = await carBLL.EditCar(carId, car);
                if(result!=null)
                {
                    return Ok(result);
                }

                return StatusCode(404, $"Unable to Edit Car");
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to Edit Car:{e.Message}");
            }
        }
        

    }
}
