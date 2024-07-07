using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ISupplierService
    {
        Task<ApiResponse<bool>> Create(SupplierRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Update(SupplierUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(int id);
        Task<ApiResponse<List<SupplierResponse>>> GetAll();
        Task<ApiResponse<SupplierResponse>> GetById(int id);
    }
}
