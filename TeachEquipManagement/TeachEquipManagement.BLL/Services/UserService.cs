using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.CommonModels;
using TeachEquipManagement.Utilities.Helper;
using TeachEquipManagement.Utilities.OptionPattern;

namespace TeachEquipManagement.BLL.Services
{
    public class UserService : IUserService, ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly JwtSecretKeyConfiguration _jwtSecret;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger,
            IOptionsSnapshot<JwtSecretKeyConfiguration> jwtSecret)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _jwtSecret = jwtSecret.Value;
        }

        #region User

        public async Task<ApiResponse<bool>> CreateUser(UserRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                int maxRetries = 3;

                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    Guid userId = Guid.NewGuid();

                    for (int i = 0; i < maxRetries; i++)
                    {
                        var existUser = await _unitOfWork.UserRepository.GetByIdAsync(userId);

                        if (existUser == null)
                        {
                            break;
                        }

                        userId = Guid.NewGuid();
                    }

                    FunctionHelper.CreatePasswordHash(request.Password, out byte[] passwordSalt, out byte[] passwordHash);

                    User newUser = new User
                    {
                        Id = userId,
                        Email = request.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Username = request.Username,
                    };

                    var entity = await _unitOfWork.UserRepository.InsertAsync(newUser);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new User successfully";
                }

                else
                {
                    response.Data = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = validation.ToString();
                }
            }

            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            };

            return response;
        }

        

        public async Task<ApiResponse<List<UserResponse>>> GetAllUser()
        {
            ApiResponse<List<UserResponse>> response = new();

            var users = await _unitOfWork.UserRepository.GetAllAsync();

            if (!users.Any())
            {
                _logger.Warning("Warning: Not Found Any User");
                response.Data = null;
                response.Message = "Not Found Any User";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<UserResponse>>(users);
                response.Data = dataResponses;
                response.Message = "List Users";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<UserResponse>> GetUserById(Guid id)
        {
            ApiResponse<UserResponse> response = new();

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user != null)
            {
                var dataResponse = _mapper.Map<UserResponse>(user);
                response.Data = dataResponse;
                response.Message = "Found User";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found User");
                response.Data = null;
                response.Message = "Not Found User";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> RemoveUser(Guid id)
        {
            ApiResponse<bool> response = new();

            try
            {
                _unitOfWork.CreateTransaction();

                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

                if (user != null)
                {
                    _unitOfWork.UserRepository.Delete(user!);
                    var isDeleted = await _unitOfWork.SaveChangesAsync();

                     _unitOfWork.Commit();

                    response.Data = isDeleted;
                    response.Message = "Remove User";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found User");
                    response.Message = "Not Found User";
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }

        public async Task<ApiResponse<bool>> UpdateUser(UserUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);

                    if (user != null)
                    {
                        var updateUser = _mapper.Map(request, user);

                        if (!string.IsNullOrEmpty(request.Password))
                        {
                            FunctionHelper.CreatePasswordHash(request.Password, out byte[] passwordSalt, out byte[] passwordHash);
                            updateUser.PasswordSalt = passwordSalt;
                            updateUser.PasswordHash = passwordHash;
                        }

                        if (!string.IsNullOrEmpty(request.Email))
                        {
                            updateUser.Email = request.Email;
                        }
                        
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update User";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found User");
                        response.Message = "Not Found User";
                        response.StatusCode = StatusCodes.Status404NotFound;
                    }
                }

                else
                {
                    response.Data = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = validation.ToString();
                }

            }
            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }

        #endregion

        #region Token

        private string GenerateAccessToken(List<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret.SecretKey));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret.SecretKey)),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public async Task<ApiResponse<AuthenticatedResponse>> Login(AuthenticatedRequest request, 
            ValidationResult validation)
        {
            ApiResponse<AuthenticatedResponse> response = new();

            if (!validation.IsValid)
            {
                response.Data = null;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = validation.ToString();

                return response;
            }

            QueryModel<User> query = new QueryModel<User>
            {
                QueryCondition = x => x.Username == request.Username
            };

            var findUser = _unitOfWork.UserRepository.GetQueryable(query).FirstOrDefault();

            if (findUser == null)
            {
                response.Data = null;
                response.Message = "Not Found User";
                response.StatusCode = StatusCodes.Status404NotFound;

                return response;
            }

            var isValidPassword = FunctionHelper.VerifyPasswordHash(request.Password, 
                            findUser.PasswordHash, findUser.PasswordSalt);

            if (!isValidPassword)
            {
                response.Data = null;
                response.Message = "Wrong Password";
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            var accessToken = GenerateAccessToken(claims);
            var refreshToken = GenerateRefreshToken();

            findUser.RefreshToken = refreshToken;
            findUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _unitOfWork.SaveChangesAsync();

            response.Data = new AuthenticatedResponse
                            {
                                AccessToken = accessToken,
                                RefreshToken = refreshToken,
                            };
            response.Message = "Login Successfully";
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }

        public async Task<ApiResponse<AuthenticatedResponse>> Refresh(AuthenticatedRefreshRequest request, ValidationResult validation)
        {
            ApiResponse<AuthenticatedResponse> response = new();

            try
            {
                if (!validation.IsValid)
                {
                    response.Data = null;
                    response.Message = "Invalid client request";
                    response.StatusCode = StatusCodes.Status400BadRequest;

                    return response;
                }

                string accessToken = request.AccessToken;
                string refreshToken = request.RefreshToken;

                var principal = GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Identity!.Name;

                QueryModel<User> query = new QueryModel<User>
                {
                    QueryCondition = x => x.Username == username
                };

                var user = _unitOfWork.UserRepository.GetQueryable(query).FirstOrDefault();

                if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    response.Data = null;
                    response.Message = "Invalid client request";
                    response.StatusCode = StatusCodes.Status400BadRequest;

                    return response;
                }

                var newAccessToken = GenerateAccessToken(principal.Claims.ToList());
                var newRefreshToken = GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                var isSuccess = await _unitOfWork.SaveChangesAsync();

                var authenResponse = new AuthenticatedResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                };

                response.Data = authenResponse;
                response.Message = "Generate new access token";
                response.StatusCode = StatusCodes.Status200OK;
            }

            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Revoke(string accessToken)
        {
            ApiResponse<bool> response = new();

            try
            {
                var principal = GetPrincipalFromExpiredToken(accessToken);

                string username = principal.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.Name).Value.ToString();

                QueryModel<User> query = new QueryModel<User>
                {
                    QueryCondition = x => x.Username == username
                };

                var user = _unitOfWork.UserRepository.GetQueryable(query).FirstOrDefault();

                if (user == null)
                {
                    response.Data = false;
                    response.Message = "Invalid client request";
                    response.StatusCode = StatusCodes.Status400BadRequest;

                    return response;
                }

                user.RefreshToken = null;
                var isSuccess = await _unitOfWork.SaveChangesAsync();

                response.Data = isSuccess;
                response.Message = "Revoke Successfully";
                response.StatusCode = StatusCodes.Status200OK;
            }

            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

           
            return response;
        }

        #endregion
    }
}
