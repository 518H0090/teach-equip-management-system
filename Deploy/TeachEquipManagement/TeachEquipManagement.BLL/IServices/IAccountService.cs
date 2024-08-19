using FluentValidation.Results;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IAccountService
    {
        Task<ApiResponse<Guid>> CreateUser(AccountRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> UpdateUser(AccountUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> RemoveUser(Guid id);
        Task<ApiResponse<List<AccountResponse>>> GetAllUser();
        Task<ApiResponse<AccountResponse>> GetUserById(Guid id);
    }
}
