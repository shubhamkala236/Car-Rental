using SharedLayer.Dto;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer
{
    public interface IUserBLL
    {
        Task<LoginResponseDTO> LoginUser(LoginDTO user);
        Task<MyDataResponseDTO> GetMyData(int myId);
        Task<MyDataResponseDTO> GetUserAdmin(int userId);
    }
}
