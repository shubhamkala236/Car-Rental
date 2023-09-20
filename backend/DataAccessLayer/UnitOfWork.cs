using DataAccessLayer.Context;
using SharedLayer;
using SharedLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarRentDbContext context;

        public UnitOfWork(CarRentDbContext context)
        {
            this.context = context;
        }

        public IUserRepository UserRepository => new UserRepository(context);

        public ICarRepository CarRepository => new CarRepository(context);

        public IRentalAgreementRepository RentalAgreementRepository => new RentalAgreementRepository(context);

        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
