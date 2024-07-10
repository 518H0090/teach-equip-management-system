﻿using Microsoft.EntityFrameworkCore.Storage;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Repositories;

namespace TeachEquipManagement.DAL.UnitOfWorks
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IDbContextTransaction _transaction;

#pragma warning disable CS8618

        public UnitOfWork(DataContext context) => _context = context;

        #pragma warning restore CS8618

        public IAccountRepository AccountRepository => new AccountRepository(_context);

        public IAccountDetailRepository AccountDetailRepository => new AccountDetailRepository(_context);

        public IToolRepository ToolRepository =>  new ToolRepository(_context);

        public ISupplierRepository SupplierRepository => new SupplierRepository(_context);

        public IInvoiceRepository InvoiceRepository =>  new InvoiceRepository(_context);

        public IInventoryRepository InventoryRepository =>  new InventoryRepository(_context);

        public IInventoryHistoryRepository InventoryHistoryRepository =>  new InventoryHistoryRepository(_context);

        public ICategoryRepository CategoryRepository =>  new CategoryRepository(_context);

        public IApprovalRequestRepository ApprovalRequestRepository => new ApprovalRequestRepository(_context);

        public IToolCategoryRepository ToolCategoryRepository => new ToolCategoryRepository(_context);

        public IQueryToolRepository QueryToolRepository => new QueryToolRepository(_context);

        public IQuerySupplierRepository QuerySupplierRepository => new QuerySupplierRepository(_context);

        public IQueryToolCategoryRepository QueryToolCategoryRepository => new QueryToolCategoryRepository(_context);

        public IQueryInvoiceRepository QueryInvoiceRepository => new QueryInvoiceRepository(_context);

        public IRoleRepository RoleRepository => new RoleRepository(_context);

        public void Commit()
        {
            _transaction.Commit();
        }

        public void CreateTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
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
