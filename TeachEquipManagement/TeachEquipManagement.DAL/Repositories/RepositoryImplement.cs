using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.Models;
using static TeachEquipManagement.DAL.IRepositories.IRepositoryImplement;

namespace TeachEquipManagement.DAL.Repositories
{
    public class RepositoryImplement
    {
        public class AccountDetailRepository : GenericRepository<AccountDetail>, IAccountDetailRepository
        {
            private readonly DataContext _context;

            public AccountDetailRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class AccountRepository : GenericRepository<Account>, IAccountRepository
        {
            private readonly DataContext _context;

            public AccountRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class ApprovalRequestRepository : GenericRepository<ApprovalRequest>, IApprovalRequestRepository
        {
            private readonly DataContext _context;

            public ApprovalRequestRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
        {
            private readonly DataContext _context;

            public CategoryRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class InventoryHistoryRepository : GenericRepository<InventoryHistory>, IInventoryHistoryRepository
        {
            private readonly DataContext _context;

            public InventoryHistoryRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
        {
            private readonly DataContext _context;

            public InventoryRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
        {
            private readonly DataContext _context;

            public InvoiceRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class RoleRepository : GenericRepository<Role>, IRoleRepository
        {
            private readonly DataContext _context;

            public RoleRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
        {
            private readonly DataContext _context;

            public SupplierRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class ToolCategoryRepository : GenericRepository<ToolCategory>, IToolCategoryRepository
        {
            private readonly DataContext _context;

            public ToolCategoryRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }

        public class ToolRepository : GenericRepository<Tool>, IToolRepository
        {
            private readonly DataContext _context;

            public ToolRepository(DataContext context) : base(context)
            {
                _context = context;
            }
        }
    }
}
