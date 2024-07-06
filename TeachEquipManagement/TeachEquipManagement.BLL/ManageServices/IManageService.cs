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
        IToolManageService ToolManageService { get; }

        IInventoryManageService InventoryManageService { get; }

        IAuthenService AuthenService { get; }
    }
}
