using FluentValidation.Results;
using System.Security.Claims;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ITokenService
    {
        Task<ApiResponse<AuthenticatedResponse>> Login(AuthenticatedRequest request, ValidationResult validation);

        Task<ApiResponse<AuthenticatedResponse>> Refresh(AuthenticatedRefreshRequest request, ValidationResult validation);

        Task<ApiResponse<bool>> Revoke(Guid userId);
    }
}
