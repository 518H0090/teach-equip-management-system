
using FluentValidation.Results;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ICategoryService
    {
        Task<ApiResponse<bool>> Create(CategoryRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Update(CategoryUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(int id);
        Task<ApiResponse<List<CategoryResponse>>> GetAll();
        Task<ApiResponse<CategoryResponse>> GetById(int id);
    }
}
