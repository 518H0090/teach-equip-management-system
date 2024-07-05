using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        Task<bool> SaveChangesAsync();

        void CreateTransaction();

        void Commit();

        void Rollback();
    }
}
