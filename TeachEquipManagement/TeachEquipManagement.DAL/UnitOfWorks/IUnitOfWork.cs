﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.IRepositories;

namespace TeachEquipManagement.DAL.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IUserPermissionRepository UserPermissionRepository { get; }

        IUserDetailRepository UserDetailRepository { get; }

        IToolRepository ToolRepository { get; }

        ISupplierRepository SupplierRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IPermissionRepository PermissionRepository { get; }

        IInvoiceRepository InvoiceRepository { get; }

        IInventoryRepository InventoryRepository { get; }

        IInventoryHistoryRepository InventoryHistoryRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IApprovalRequestRepository ApprovalRequestRepository { get; }

        IToolCategoryRepository ToolCategoryRepository { get; }

        IQueryToolRepository QueryToolRepository { get; }

        Task<bool> SaveChangesAsync();

        void CreateTransaction();

        void Commit();

        void Rollback();
    }
}
