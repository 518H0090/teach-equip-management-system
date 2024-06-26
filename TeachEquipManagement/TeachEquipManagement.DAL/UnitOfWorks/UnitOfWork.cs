using Microsoft.EntityFrameworkCore.Storage;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Repositories;

namespace TeachEquipManagement.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DataContext context, IDbContextTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IUserRepository UserRepository => new UserRepository(_context);

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public void CreateTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}
