using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;

namespace TeachEquipManagement.BLL.ManageServices
{
    public interface IInventoryManageService
    {
        IInventoryService InventoryService { get; }

        IInventoryHistoryService InventoryHistoryService { get; }

        IApprovalRequestService ApprovalRequestService { get; }
    }
}
