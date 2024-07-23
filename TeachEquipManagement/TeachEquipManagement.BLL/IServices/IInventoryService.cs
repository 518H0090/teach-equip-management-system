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
    public interface IInventoryService
    {
        Task<ApiResponse<bool>> Create(InventoryRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Update(InventoryUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(Guid id);
        Task<ApiResponse<List<InventoryResponse>>> GetAll();
        Task<ApiResponse<InventoryResponse>> GetById(Guid id);
    }
}
