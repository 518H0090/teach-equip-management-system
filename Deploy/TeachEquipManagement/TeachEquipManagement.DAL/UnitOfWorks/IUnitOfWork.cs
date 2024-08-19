using TeachEquipManagement.DAL.IRepositories;
using static TeachEquipManagement.DAL.IRepositories.IRepositoryImplement;

namespace TeachEquipManagement.DAL.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }

        IAccountDetailRepository AccountDetailRepository { get; }

        IToolRepository ToolRepository { get; }

        ISupplierRepository SupplierRepository { get; }

        IInvoiceRepository InvoiceRepository { get; }

        IInventoryRepository InventoryRepository { get; }

        IInventoryHistoryRepository InventoryHistoryRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        IApprovalRequestRepository ApprovalRequestRepository { get; }

        IToolCategoryRepository ToolCategoryRepository { get; }

        IQueryToolRepository QueryToolRepository { get; }

        IQuerySupplierRepository QuerySupplierRepository { get; }

        IQueryToolCategoryRepository QueryToolCategoryRepository { get; }

        IQueryInvoiceRepository QueryInvoiceRepository { get; }

        IRoleRepository RoleRepository { get; }

        Task<bool> SaveChangesAsync();

        void CreateTransaction();

        void Commit();

        void Rollback();
    }
}
