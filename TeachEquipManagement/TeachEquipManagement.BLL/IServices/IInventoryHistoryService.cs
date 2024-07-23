using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IInventoryHistoryService
    {
        Task<ApiResponse<List<InventoryHistoryResponse>>> GetAll();
        ApiResponse<InventoryHistoryResponse> GetInventoryHistory(ProcessRequest request, ValidationResult validation);
    }
}
