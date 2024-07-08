using System.Security.Claims;

namespace TeachEquipManagement.BLL.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(List<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
