using SharedLayer.Dto;
using SharedLayer.Interface;
using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class RentAgreementBLL : IRentAgreementBLL
    {
        private readonly IUnitOfWork uow;
        public RentAgreementBLL(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<bool> AcceptAgreement(int agreementId,int userId)
        {
            var result = await uow.RentalAgreementRepository.AcceptAgreement(agreementId,userId);
            await uow.SaveAsync();
            return result;
        }

        //Add new rental agreement
        public async Task<bool> AddUserAgreement(AddUserAgreementDTO agreement, int userId)
        {
            //convert into Rental DB entity
            var newAgreement = new RentalAgreement
            {
                UserId = userId,
                CarId = agreement.CarId,
                RentalDuration = agreement.RentalDuration,
                TotalCost = 0,
                IsAccepted = false,
                IsReturned = false,
                IsReturnRequested = false,
                FromDate = DateTime.Now,
                ToDate= DateTime.Now.AddDays(agreement.RentalDuration),
            };

            var result = await uow.RentalAgreementRepository.AddUserAgreement(newAgreement);
            await uow.SaveAsync();
            return result;

        }

        //public async Task<bool> ApproveRequestAdmin(int agreementId)
        //{
        //    var result = await uow.RentalAgreementRepository.ApproveRequestAdmin(agreementId);
        //    await uow.SaveAsync();
        //    return result;
        //}

        //Approve return 
        public async Task<bool> ApproveReturn(int agreementId)
        {
            var result = await uow.RentalAgreementRepository.ApproveReturn(agreementId);
            await uow.SaveAsync();
            return result;
        }

        //Delete agreement
        public async Task<DisplayAgreementDTO> DeleteAgreement(int agreementId, int userId)
        {
            var result = await uow.RentalAgreementRepository.DeleteAgreement(agreementId,userId);
            await uow.SaveAsync();
            if(result==null)
            {
                return null;
            }
            //create dto and send back to controller
            var displayAgreementDto = new DisplayAgreementDTO
            {
                AgreementId = result.AgreementId,
                UserId = result.UserId,
                CarId = result.CarId,
                RentalDuration = result.RentalDuration,
                TotalCost = result.TotalCost,
                IsAccepted = result.IsAccepted,
                IsReturnRequested = result.IsReturnRequested,
                IsReturned = result.IsReturned,
                ToDate=result.ToDate,
                FromDate=result.FromDate,
            };
            return displayAgreementDto;
        }

        //delete agreement -----admin

        public async Task<DisplayAgreementDTO> DeleteAgreementAdmin(int agreementId)
        {
            var result = await uow.RentalAgreementRepository.DeleteAgreementAdmin(agreementId);
            await uow.SaveAsync();
            if (result == null)
            {
                return null;
            }
            //create dto and send back to controller
            var displayAgreementDto = new DisplayAgreementDTO
            {
                AgreementId = result.AgreementId,
                UserId = result.UserId,
                CarId = result.CarId,
                RentalDuration = result.RentalDuration,
                TotalCost = result.TotalCost,
                IsAccepted = result.IsAccepted,
                IsReturnRequested = result.IsReturnRequested,
                IsReturned = result.IsReturned,
                ToDate=result.ToDate,
                FromDate=result.FromDate,
            };
            return displayAgreementDto;
        }

        //edit agreement
        public async Task<DisplayAgreementDTO> EditAgreement(int agreementId, int rentDuration,int userId)
        {
            var result = await uow.RentalAgreementRepository.EditAgreement(agreementId, rentDuration,userId);
            await uow.SaveAsync();
            if(result==null)
            {
                return null;
            }
            //convert to DTO
            var agreementDto = new DisplayAgreementDTO
            {
                AgreementId = result.AgreementId,
                UserId = result.UserId,
                CarId = result.CarId,
                RentalDuration = result.RentalDuration,//update
                TotalCost = result.TotalCost, //update
                IsAccepted = result.IsAccepted,
                IsReturnRequested = result.IsReturnRequested,
                IsReturned = result.IsReturned,
                ToDate=result.ToDate,
                FromDate=result.FromDate,
            };

            return agreementDto;
        }

        public async Task<DisplayAgreementDTO> EditAgreementAdmin(int agreementId, int rentDuration)
        {
            var result = await uow.RentalAgreementRepository.EditAgreementAdmin(agreementId, rentDuration);
            await uow.SaveAsync();
            //convert to DTO
            var agreementDto = new DisplayAgreementDTO
            {
                AgreementId = result.AgreementId,
                UserId = result.UserId,
                CarId = result.CarId,
                RentalDuration = result.RentalDuration,//update
                TotalCost = result.TotalCost, //update
                IsAccepted = result.IsAccepted,
                IsReturnRequested = result.IsReturnRequested,
                IsReturned = result.IsReturned,
                ToDate=result.ToDate, //update
                FromDate=result.FromDate, //update
            };

            return agreementDto;
        }

        //agreement by Id
        public async Task<DisplayAgreementDTO> GetAgreementById(int agreementId, int userId)
        {
            var result = await uow.RentalAgreementRepository.GetAgreementById(agreementId, userId);
            if(result==null)
            {
                return null;
            }

            //map to DTO and send Back
            var displayAgreementDto = new DisplayAgreementDTO
            {
                AgreementId = result.AgreementId,
                UserId = result.UserId,
                CarId = result.CarId,
                RentalDuration = result.RentalDuration,
                TotalCost = result.TotalCost,
                IsAccepted = result.IsAccepted,
                IsReturnRequested = result.IsReturnRequested,
                IsReturned = result.IsReturned,
                ToDate = result.ToDate,
                FromDate = result.FromDate,
            };

            return displayAgreementDto;
        }

        //By Id ---- Admin
        public async Task<DisplayAgreementDTO> GetAgreementByIdAdmin(int agreementId)
        {
            var result = await uow.RentalAgreementRepository.GetAgreementByIdAdmin(agreementId);
            if (result == null)
            {
                return null;
            }

            //map to DTO and send Back
            var displayAgreementDto = new DisplayAgreementDTO
            {
                AgreementId = result.AgreementId,
                UserId = result.UserId,
                CarId = result.CarId,
                RentalDuration = result.RentalDuration,
                TotalCost = result.TotalCost,
                IsAccepted = result.IsAccepted,
                IsReturnRequested = result.IsReturnRequested,
                IsReturned = result.IsReturned,
                ToDate = result.ToDate,
                FromDate = result.FromDate,
            };

            return displayAgreementDto;
        }

        //Get All Agreements ----admin
        public async Task<IEnumerable<DisplayAgreementDTO>> GetAllAgreements()
        {
            var result = await uow.RentalAgreementRepository.GetAllAgreements();

            //convert to dto and send to frontend
            var agreementDtoList = result.Select(a => new DisplayAgreementDTO
            {
                AgreementId = a.AgreementId,
                UserId = a.UserId,
                CarId = a.CarId,
                RentalDuration = a.RentalDuration,
                TotalCost = a.TotalCost,
                IsAccepted = a.IsAccepted,
                IsReturnRequested = a.IsReturnRequested,
                IsReturned = a.IsReturned,
                ToDate = a.ToDate,
                FromDate = a.FromDate,
            }).ToList();

            return agreementDtoList;
        }

        public async Task<IEnumerable<DisplayAgreementDTO>> GetMyAgreements(int userId)
        {
            var result = await uow.RentalAgreementRepository.GetMyAgreements(userId);

            //convert to dto and send to frontend
            var agreementDtoList = result.Select(a => new DisplayAgreementDTO
            {
                AgreementId=a.AgreementId,
                UserId=a.UserId,
                CarId = a.CarId,
                RentalDuration=a.RentalDuration,
                TotalCost=a.TotalCost,
                IsAccepted=a.IsAccepted,
                IsReturnRequested=a.IsReturnRequested,
                IsReturned=a.IsReturned,
                ToDate = a.ToDate,
                FromDate = a.FromDate,

            }).ToList();

            return agreementDtoList;
        }

        public async Task<bool> RequestReturn(int agreementId, int userId)
        {
            var result = await uow.RentalAgreementRepository.RequestReturn(agreementId, userId);
            await uow.SaveAsync();
            return result;
        }
    }
}
