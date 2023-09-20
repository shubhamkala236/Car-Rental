using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using SharedLayer;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentDbContext context;
        public UserRepository(CarRentDbContext context)
        {
            this.context = context;
        }

        //get my data
        public async Task<User> GetMyData(int myId)
        {
            var myData = await context.Users.FirstOrDefaultAsync(x => x.UserId == myId);
            if (myData == null)
            {
                return null;
            }

            return myData;
        }

        // get user ---- admin
        public async Task<User> GetUserAdmin(int userId)
        {
            //var myData = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            var userData = await context.Users.FindAsync(userId);
            if (userData == null)
            {
                return null;
            }

            return userData;
        }

        public async Task<User> Login(string email, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if(user==null)
            {
                return null;
            }
            if(user.Password!=password)
            {
                return null;
            }

            return user;
        }
    }
}
