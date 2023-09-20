using SharedLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Interface
{
    public interface IRentAgreementBLL
    {
        //create agreement
        Task<bool> AddUserAgreement(AddUserAgreementDTO agreement,int userId);
        //get my agreement
        Task<IEnumerable<DisplayAgreementDTO>> GetMyAgreements(int userId);

        //edit agreement duration
        Task<DisplayAgreementDTO> EditAgreement(int agreementId,int rentDuration,int userId);

        //EDit agreement -- admin
        Task<DisplayAgreementDTO> EditAgreementAdmin(int agreementId,int rentDuration);

        //Approve REquest -- admin
        //Task<bool> ApproveRequestAdmin(int agreementId);

        //delete agreement
        Task<DisplayAgreementDTO> DeleteAgreement(int agreementId,int userId); 
        //delete agreeement ANY ---- Admin
        Task<DisplayAgreementDTO> DeleteAgreementAdmin(int agreementId);
        //get agreement by id
        Task<DisplayAgreementDTO> GetAgreementById(int agreementId,int userId);
        //Admin Get By ID
        Task<DisplayAgreementDTO> GetAgreementByIdAdmin(int agreementId);

        //accept agreement
        Task<bool> AcceptAgreement(int agreementId,int userId);

        //REquest Return
        Task<bool> RequestReturn(int agreementId, int userId);
        //approve return
        Task<bool> ApproveReturn(int agreementId);

        //get all agreements -- admin
        Task<IEnumerable<DisplayAgreementDTO>> GetAllAgreements();

    }
}
