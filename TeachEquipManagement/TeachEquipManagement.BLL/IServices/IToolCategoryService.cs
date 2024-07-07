using FluentValidation.Results;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.Services;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IToolCategoryService
    {
        Task<ApiResponse<bool>> Create(ToolCategoryRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(ToolCategoryRequest request);
        Task<ApiResponse<List<ToolCategoryResponse>>> GetAll();
        Task<ApiResponse<ToolCategoryResponse>> GetById(ToolCategoryRequest id);
    }
}
