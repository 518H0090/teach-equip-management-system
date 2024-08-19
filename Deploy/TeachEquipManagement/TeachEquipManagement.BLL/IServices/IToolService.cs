using FluentValidation.Results;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IToolService
    {
        Task<ApiResponse<int>> Create(ToolRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Update(ToolUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(int id);
        Task<ApiResponse<List<ToolResponse>>> GetAll();
        Task<ApiResponse<ToolResponse>> GetById(int id);
        Task<ApiResponse<List<ToolIncludeSupplierResponse>>> GetAllIncludeSupplier();
    }
}
