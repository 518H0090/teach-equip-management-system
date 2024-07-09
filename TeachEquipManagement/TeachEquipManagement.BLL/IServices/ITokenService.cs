using FluentValidation.Results;
using System.Security.Claims;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(List<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        Task<ApiResponse<AuthenticatedResponse>> Login(AuthenticatedRequest request, ValidationResult validation);
    }
}
