using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLayer;
using SharedLayer.Dto;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserBLL userBLL;
        public UserController(IUserBLL userBLL)
        {
            this.userBLL = userBLL;
        }

        //Login User ---- any
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromForm]LoginDTO user)
        {
            try
            {
                // user bll me data bhejo
                var response = await userBLL.LoginUser(user);
                if(response!=null)
                {
                    return Ok(response);
                }
                return Unauthorized();
            }
            catch(Exception e)
            {
                return StatusCode(500, $"An error occured while login:{e.Message}");
            }
        }
        
        //get my data ----USER
        [HttpGet("myData")]
        public async Task<IActionResult> GetMyData()
        {
            try
            {
                var userId = GetCurrentUser();
                if (userId == 0)
                {
                    return Unauthorized();
                }
                // user bll me data bhejo
                var response = await userBLL.GetMyData(userId);
                if (response != null)
                {
                    return Ok(response);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occured while login:{e.Message}");
            }
        }

        //get any user's Data ----- ADMIN
        [HttpGet("getUserAdmin/{userId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetUserAdmin(int userId)
        {
            try
            {   // user bll me data bhejo
                var response = await userBLL.GetUserAdmin(userId);
                if (response != null)
                {
                    return Ok(response);
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occured while login:{e.Message}");
            }
        }

        private int GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return 0;
        }
    }
}
