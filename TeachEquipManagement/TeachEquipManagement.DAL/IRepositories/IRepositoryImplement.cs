using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.IRepositories
{
    public interface IRepositoryImplement
    {
        public interface IAccountDetailRepository : IGenericRepository<AccountDetail>
        {
        }

        public interface IAccountRepository : IGenericRepository<Account>
        {
        }

        public interface IApprovalRequestRepository : IGenericRepository<ApprovalRequest>
        {
        }

        public interface ICategoryRepository : IGenericRepository<Category>
        {
        }

        public interface IInventoryHistoryRepository : IGenericRepository<InventoryHistory>
        {
        }

        public interface IInventoryRepository : IGenericRepository<Inventory>
        {
        }

        public interface IInvoiceRepository : IGenericRepository<Invoice>
        {
        }

        public interface IRoleRepository : IGenericRepository<Role>
        {
        }

        public interface ISupplierRepository : IGenericRepository<Supplier>
        {
        }

        public interface IToolCategoryRepository : IGenericRepository<ToolCategory>
        {
        }

        public interface IToolRepository : IGenericRepository<Tool>
        {
        }
    }
}
