using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dto;
using SharedLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //only for logged in users
    public class AgreementController : ControllerBase
    {
        private readonly IRentAgreementBLL rentAgreementBLL;
        public AgreementController(IRentAgreementBLL rentAgreementBLL)
        {
            this.rentAgreementBLL = rentAgreementBLL;
        }

        //Add a rental Ageement -- user
        [HttpPost("rentalAgreement")]
        [Authorize(Policy = "UserOnly")] //users only 
        public async Task<IActionResult> AddUserAgreement([FromForm] AddUserAgreementDTO agreement)
        {
            try
            {
                var userId = GetCurrentUser();
                if(userId==0)
                {
                    return Unauthorized();
                }

                var result = await rentAgreementBLL.AddUserAgreement(agreement, userId);
                if (result)
                {
                    return Ok("Created Rental Successfully");
                }

                return StatusCode(400,"Unable to create rental agreement:");
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to create rental agreement: {e.Message}");
            }
        }

        //get My Agreements
        [HttpGet("myAgreement")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetMyAgreements()
        {
            try
            {
                var userId = GetCurrentUser();
                if(userId==0)
                {
                    return Unauthorized();
                }

                var result = await rentAgreementBLL.GetMyAgreements(userId);
                if(result==null)
                {
                    return NotFound("Not found any agreements");
                }
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to create rental agreement: {e.Message}");
            }
        }

        //Get Agreement By Id
        [HttpGet("agreementById/{agreementId}")]
        public async Task<IActionResult> GetAgreementById(int agreementId)
        {
            try
            {
                var userId = GetCurrentUser();
                if (userId == 0)
                {
                    return Unauthorized();
                }
                var result = await rentAgreementBLL.GetAgreementById(agreementId, userId);
                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(400, $"Unable to Find rental agreement");
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Find rental agreement: {e.Message}");
            }
        }

        //Admin GetAnyId----- Admin
        [HttpGet("agreementByIdAdmin/{agreementId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAgreementByIdAdmin(int agreementId)
        {
            try
            {
                var result = await rentAgreementBLL.GetAgreementByIdAdmin(agreementId);
                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(400, $"Unable to Find rental agreement");
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Find rental agreement: {e.Message}");
            }
        }

        //Edit agreement -- user
        [HttpPut("editAgreement/{agreementId}")]
        public async Task<IActionResult> EditAgreement(int agreementId,[FromForm] int updatedRentDuration)
        {
            try
            {
                var userId = GetCurrentUser();
                if (userId == 0)
                {
                    return Unauthorized();
                }
                var result = await rentAgreementBLL.EditAgreement(agreementId,updatedRentDuration,userId);
                if(result!=null)
                {
                    return Ok(result);
                }
                return StatusCode(400, $"Unable to Edit rental agreement");
            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to Edit rental agreement: {e.Message}");
            }
        }

        //Edit agreement ALL -- ADMIN
        [HttpPut("editAgreementAdmin/{agreementId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditAgreementAdmin(int agreementId, [FromForm] int updatedRentDuration)
        {
            try
            {
                var result = await rentAgreementBLL.EditAgreementAdmin(agreementId, updatedRentDuration);
                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(400, $"Unable to Edit rental agreement");
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Edit rental agreement: {e.Message}");
            }
        }

        //delete Agreement --user
        [HttpDelete("delete/{agreementId}")]
        public async Task<IActionResult> DeleteAgreement(int agreementId)
        {
            try
            {
                var userId = GetCurrentUser();
                if (userId == 0)
                {
                    return Unauthorized();
                }
                var result = await rentAgreementBLL.DeleteAgreement(agreementId,userId);
                if(result!=null)
                {
                    return Ok(result);
                }
                return StatusCode(400, $"Unable to Delete rental agreement Check Details Again");

            }
            catch(Exception e)
            {
                return StatusCode(400, $"Unable to Delete rental agreement: {e.Message}");
            }
        }

        //delete Agreement ANY ----ADMIN
        [HttpDelete("deleteAgreementAdmin/{agreementId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAgreementAdmin(int agreementId)
        {
            try
            {
                var result = await rentAgreementBLL.DeleteAgreementAdmin(agreementId);
                if (result != null)
                {
                    return Ok(result);
                }
                return StatusCode(400, $"Unable to Delete rental agreement Check Details Again");

            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Delete rental agreement: {e.Message}");
            }
        }

        //Accept agreement -- user
        [HttpGet("accept/{agreementId}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> AcceptAgreement(int agreementId)
        {
            try
            {
                var userId = GetCurrentUser();
                if (userId == 0)
                {
                    return Unauthorized();
                }
                var result = await rentAgreementBLL.AcceptAgreement(agreementId,userId);
                if (result)
                {
                    return Ok("Accepted Agreement Successfully");
                }

                return StatusCode(400, "Unable to Accept agreement:");


            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Accept rental agreement Error: {e.Message}");
            }
        }

        //Request Return--- user
        [HttpGet("returnRequest/{agreementId}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> RequestReturn(int agreementId)
        {
            try
            {
                var userId = GetCurrentUser();
                if (userId == 0)
                {
                    return Unauthorized();
                }
                var result = await rentAgreementBLL.RequestReturn(agreementId, userId);
                if (result)
                {
                    return Ok("Return Request Successfully");
                }

                return StatusCode(400, "Unable to Request a Return");


            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Request a Return Error: {e.Message}");
            }
        }

       
        //Approve Return ---- admin
        [HttpGet("requestApprove/{agreementId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ApproveReturn(int agreementId)
        {
            try
            {
                var result = await rentAgreementBLL.ApproveReturn(agreementId);
                if (result)
                {
                    return Ok("Approve Return Successfully");
                }

                return StatusCode(400, "Unable to Approve Return");


            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to Approve Return Error: {e.Message}");
            }
        }

        //Get All Agreements -- ADMIN
        [HttpGet("allAgreementsAdmin")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<IActionResult> GetAllAgreements()
        {
            try
            {  
                var result = await rentAgreementBLL.GetAllAgreements();
                if (result == null)
                {
                    return NotFound("Not found any agreements");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(400, $"Unable to create rental agreement: {e.Message}");
            }
        }


        //get current logged-In user data
        private int GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim!=null && int.TryParse(userIdClaim.Value,out int userId))
            {
                return userId;
            }

            return 0;
        }

    }
}
