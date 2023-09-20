using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer
{
    public interface IUserRepository
    {
        //login
        Task<User> Login(string email, string password);
        Task<User> GetMyData(int myId);
        Task<User> GetUserAdmin(int userId);
    }
}
