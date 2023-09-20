using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using SharedLayer.Interface;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RentalAgreementRepository : IRentalAgreementRepository
    {
        private readonly CarRentDbContext context;
        public RentalAgreementRepository(CarRentDbContext context)
        {
            this.context = context;
        }

        //accept agreement --- user accepts
        public async Task<bool> AcceptAgreement(int agreementId,int userId)
        {
            //check if not already accepted
            //check validity of user
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return false;
            }
            //if currentUser Id != agreement creator userId
            if (agreement.UserId != userId)
            {
                // The user can edit or delete their own agreements.
                return false;
            }
            //valid user
            //check if availibility true
            var car = await context.Cars.FindAsync(agreement.CarId);
            if (car.AvailibilityStatus == "unavailable")
            {
                return false;
            }

            //available car
            //make agreement
            //availibliliy false and accepted true;
            car.AvailibilityStatus = "unavailable";
            agreement.IsAccepted = true;


            return true;

        }

        //add create rental agreement ---- user
        public async Task<bool> AddUserAgreement(RentalAgreement agreement)
        {
            //check if valid Car or not
            var car = await context.Cars.FindAsync(agreement.CarId);
            if(car==null)
            {
                return false;
            }
            //Now check the availibilty status of that car
            var status = car.AvailibilityStatus;
            if (status=="unavailable")
            {
                //if not available
                return false;
            }

            //now we can rent this car
            var totalCost = car.RentPrice * agreement.RentalDuration;
            //modify total cost
            agreement.TotalCost = totalCost;

            //adding agreement
            await context.RentalAgreements.AddAsync(agreement);

            return true;

        }

        //Aproove Request Admin
        //public async Task<bool> ApproveRequestAdmin(int agreementId)
        //{
        //    //check if valid user asking for return
        //    var agreement = await context.RentalAgreements.FindAsync(agreementId);
        //    if (agreement == null)
        //    {
        //        return false;
        //    }
            
        //    //valid User
        //    //if is already true or not
        //    if (agreement.IsAccepted == true && agreement.IsReturnRequested == true)
        //    {
        //        //return request
        //        agreement.IsReturned = true;

        //        return true;
        //    }

        // return false;
        //}

        //Approve Return
        public async Task<bool> ApproveReturn(int agreementId)
        {
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if(agreement==null)
            {
                return false;
            }
            //check if isRequested true;
            if(agreement.IsReturnRequested == false || agreement.IsReturned == true)
            {
                return false;
            }

            //mark as returned and set car to available again
            var car = await context.Cars.FindAsync(agreement.CarId);
            if (car == null)
            {
                return false;
            }
            car.AvailibilityStatus = "available";
            agreement.IsReturned = true;

            return true;

        }

        //Delete agreement
        public async Task<RentalAgreement> DeleteAgreement(int agreementId,int userId)
        {
            //var agreement = await context.RentalAgreements.FindAsync(agreementId);
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return null;
            }
            //if currentUser Id == agreement creator userId
            if (agreement.UserId != userId)
            {
                // The user can edit or delete their own agreements only
                return null;
            }
            //valid user
            //check Accepted or not
            if(agreement.IsAccepted==true)
            {
                return null;
            }

            //check if current user is the one to Delete the agreement
            //var user = await context.Users.FindAsync(userId);
            //if (user == null)
            //{
            //    return null;
            //}

            context.RentalAgreements.Remove(agreement);

            return agreement;
        }

        //Delete Agreement -------ADMIN

        public async Task<RentalAgreement> DeleteAgreementAdmin(int agreementId)
        {
            //var agreement = await context.RentalAgreements.FindAsync(agreementId);
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return null;
            }
            //simply delete agreement if Found
            context.RentalAgreements.Remove(agreement);

            return agreement;
        }

        //edit agreement --- user
        public async Task<RentalAgreement> EditAgreement(int agreementId, int newDuration, int userId)
        {
            //get check if agreement creator is the editor agreement
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return null;
            }
            //if currentUser Id == agreement creator userId
            if (agreement.UserId != userId)
            {
                // The user can edit or delete their own agreements.
                return null;
            }

            //check if accepted or not
            if (agreement.IsAccepted==true)
            {
                return null;
            }

           

            //find car cost
            var car = await context.Cars.FindAsync(agreement.CarId);
            if(car==null)
            {
                return null;
            }

            //update agreement duration and total cost
            agreement.RentalDuration = newDuration;
            agreement.TotalCost = newDuration * car.RentPrice;
            agreement.FromDate = DateTime.Now;
            agreement.ToDate = DateTime.Now.AddDays(agreement.RentalDuration);

            return agreement;
        }

        //Edit Agreement --- admin
        public async Task<RentalAgreement> EditAgreementAdmin(int agreementId, int newDuration)
        {
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return null;
            }

            //find car cost
            var car = await context.Cars.FindAsync(agreement.CarId);
            if (car == null)
            {
                return null;
            }

            //update agreement duration and total cost
            agreement.RentalDuration = newDuration;
            agreement.TotalCost = newDuration * car.RentPrice;
            agreement.FromDate = DateTime.Now;
            agreement.ToDate = DateTime.Now.AddDays(agreement.RentalDuration);

            return agreement;
        }

        //get agreement by Id -- user
        public async Task<RentalAgreement> GetAgreementById(int agreementId, int userId)
        {
            //get check if agreement creator is the editor agreement
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return null;
            }
            //if currentUser Id == agreement creator userId
            if (agreement.UserId != userId)
            {
                // The user can edit or delete their own agreements.
                return null;
            }

            //valid user
            return agreement;
        }

        public async Task<RentalAgreement> GetAgreementByIdAdmin(int agreementId)
        {
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return null;
            }
            //admin
            //valid user
            return agreement;
        }

        //Get all agreements --- admin
        public async Task<IEnumerable<RentalAgreement>> GetAllAgreements()
        {
            var myRentalAgreements = await context.RentalAgreements.ToListAsync();
            return myRentalAgreements;
        }

        public async Task<IEnumerable<RentalAgreement>> GetMyAgreements(int userId)
        {
            var myRentalAgreements = await context.RentalAgreements.Where(a => a.UserId == userId).ToListAsync();
            return myRentalAgreements;
        }

        

        public async Task<bool> RequestReturn(int agreementId, int userId)
        {
            //check if valid user asking for return
            var agreement = await context.RentalAgreements.FindAsync(agreementId);
            if (agreement == null)
            {
                return false;
            }
            //if currentUser Id == agreement creator userId
            if (agreement.UserId != userId)
            {
                // The user can edit or delete their own agreements.
                return false;
            }
            //valid User
            //if isaccepted already true or not
            if (agreement.IsAccepted==false || agreement.IsReturnRequested==true)
            {
                return false;
            }

            //return request
            agreement.IsReturnRequested = true;

            return true;

        }

        
    }
}
