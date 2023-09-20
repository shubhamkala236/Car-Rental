using SharedLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Interface
{
    public interface IRentalAgreementRepository
    {
        //add agreement
        Task<bool> AddUserAgreement(RentalAgreement agreement);
        //get my rentals
        Task<IEnumerable<RentalAgreement>> GetMyAgreements(int userId);

        //edit agreement duration
        Task<RentalAgreement> EditAgreement(int agreementId,int newDuration,int userId);

        //edit Agreement -- Admin
        Task<RentalAgreement> EditAgreementAdmin(int agreementId,int newDuration);

        //delete Agreement
        Task<RentalAgreement> DeleteAgreement(int agreementId, int userId);
        //delete agreement Admin
        Task<RentalAgreement> DeleteAgreementAdmin(int agreementId);
        Task<RentalAgreement> GetAgreementById(int agreementId, int userId);
        //Admin get by Id
        Task<RentalAgreement> GetAgreementByIdAdmin(int agreementId);
        //accept agreement
        Task<bool> AcceptAgreement(int agreementId, int userId);
        //Request Return
        Task<bool> RequestReturn(int agreementId, int userId);
        //approve return
        Task<bool> ApproveReturn(int agreementId);
        //approve return ---Admin
        //Task<bool> ApproveRequestAdmin(int agreementId);

        //get all agreements -- admin
        Task<IEnumerable<RentalAgreement>> GetAllAgreements();

    }
}
