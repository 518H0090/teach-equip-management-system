using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Entity;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Repositories;

namespace TeachEquipManagement.DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private DbContextTransaction _transaction;

        public UnitOfWork(DataContext context, DbContextTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IUserRepository UserRepository => new UserRepository(_context);

        public void Commit()
        {
            _transaction.Commit();
        }

        public void CreateTransaction()
        {
            _transaction = (DbContextTransaction)_context.Database.BeginTransaction();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync() > 0;
        }
    }
}
