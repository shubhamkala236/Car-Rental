using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLayer.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICarRepository CarRepository { get; }
        IRentalAgreementRepository RentalAgreementRepository { get; }

        Task<bool> SaveAsync();
    }
}
