using FluentValidation.Results;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IUserService
    {
        Task<ApiResponse<bool>> CreateUser(UserRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> UpdateUser(UserUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> RemoveUser(int id);
        Task<ApiResponse<List<UserResponse>>> GetAllUser();
        Task<ApiResponse<UserResponse>> GetUserById(int id);
    }
}
