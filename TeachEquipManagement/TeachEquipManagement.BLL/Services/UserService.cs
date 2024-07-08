using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Serilog;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.CustomAttribute;
using TeachEquipManagement.Utilities.Helper;

namespace TeachEquipManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> CreateUser(UserRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    Guid userId = Guid.NewGuid();

                    var existUser = await _unitOfWork.UserRepository.GetByIdAsync(userId);

                    if (existUser != null)
                    {
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
                    response.Message = "Create new Category successfully";
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

        public Task<ApiResponse<List<UserResponse>>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserResponse>> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> RemoveUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> UpdateUser(UserUpdateRequest request, ValidationResult validation)
        {
            throw new NotImplementedException();
        }
    }
}
