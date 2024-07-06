using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.IRepositories;

namespace TeachEquipManagement.BLL.ManageServices
{
    public interface IManageService
    {
        IUserService UserService { get; }

        IUserPermissionService UserPermissionService { get; }

        IUserDetailService UserDetailService { get; }

        IToolService ToolService { get; }

        ISupplierService SupplierService { get; }

        IRefreshTokenService RefreshTokenService { get; }

        IPermissionService PermissionService { get; }

        IInvoiceService InvoiceService { get; }

        IInventoryService InventoryService { get; }

        IInventoryHistoryService InventoryHistoryService { get; }

        ICategoryService CategoryService { get; }

        IApprovalRequestService ApprovalRequestService { get; }
    }
}
