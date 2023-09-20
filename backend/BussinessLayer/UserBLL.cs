using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLayer;
using SharedLayer.Dto;
using SharedLayer.Interface;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class UserBLL : IUserBLL
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration config;
        public UserBLL(IUnitOfWork uow, IConfiguration config)
        {
            this.uow = uow;
            this.config = config;
        }

        //get my data
        public async Task<MyDataResponseDTO> GetMyData(int myId)
        {
            var response = await uow.UserRepository.GetMyData(myId);

            //convert to myDataDTO
            var myData = new MyDataResponseDTO
            {
                UserId=response.UserId,
                Name=response.Name,
                Email=response.Email,
            };

            return myData;

        }

        //get user ----- admin
        public async Task<MyDataResponseDTO> GetUserAdmin(int userId)
        {
            var response = await uow.UserRepository.GetUserAdmin(userId);

            //convert to myDataDTO
            var myData = new MyDataResponseDTO
            {
                UserId = response.UserId,
                Name = response.Name,
                Email = response.Email,
            };

            return myData;
        }

        //login user
        public async Task<LoginResponseDTO> LoginUser(LoginDTO user)
        {
            var result = await uow.UserRepository.Login(user.Email, user.Password);
            if(result!=null)
            {
                var loginResponse = new LoginResponseDTO();
                loginResponse.Email = user.Email;
                loginResponse.Token = CreateJWT(result);
                return loginResponse;
            }
            return null;
        }

        //JWT Token Create
        private string CreateJWT(User user)
        {
            var secretKey = config.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Name,user.IsAdmin.ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),

            };

            // Add role claims
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "user"));
            }

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
