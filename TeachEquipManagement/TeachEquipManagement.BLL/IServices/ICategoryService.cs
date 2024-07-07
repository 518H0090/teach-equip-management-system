
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ICategoryService
    {
        Task<ApiResponse<bool>> Create(CategoryRequest request);
        Task<ApiResponse<bool>> Update(CategoryUpdateRequest request);
        Task<ApiResponse<bool>> Remove(int id);
        Task<ApiResponse<List<CategoryResponse>>> GetAll();
        Task<ApiResponse<CategoryResponse>> GetById(int id);
    }
}
