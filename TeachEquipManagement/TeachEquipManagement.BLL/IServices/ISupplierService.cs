using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ISupplierService
    {
        Task<ApiResponse<bool>> Create(SupplierRequest request);
        Task<ApiResponse<bool>> Update(SupplierRequest request);
        Task<ApiResponse<bool>> Remove(int id);
        Task<ApiResponse<SupplierResponse>> GetAll();
        Task<ApiResponse<SupplierResponse>> GetById(int id);
    }
}
