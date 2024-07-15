using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Create(RoleRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var role = _mapper.Map<Role>(request);

                    var entity = await _unitOfWork.RoleRepository.InsertAsync(role);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new Role successfully";
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

        public async Task<ApiResponse<List<RoleResponse>>> GetAll()
        {
            ApiResponse<List<RoleResponse>> response = new();

            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            if (!roles.Any())
            {
                _logger.Warning("Warning: Not Found Any Roles");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<RoleResponse>>(roles);
                response.Data = dataResponses;
                response.Message = "List Roles";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<RoleResponse>> GetById(int id)
        {
            ApiResponse<RoleResponse> response = new();

            var category = await _unitOfWork.RoleRepository.GetByIdAsync(id);

            if (category != null)
            {
                var dataResponse = _mapper.Map<RoleResponse>(category);
                response.Data = dataResponse;
                response.Message = "Found Role";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Role");
                response.Data = null;
                response.Message = "Not Found Role";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Remove(int id)
        {
            ApiResponse<bool> response = new();

            try
            {
                _unitOfWork.CreateTransaction();

                var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);

                if (role != null)
                {
                    _unitOfWork.RoleRepository.Delete(role!);
                    var isSuccess = await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = isSuccess;
                    response.Message = "Remove Role";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Role");
                    response.Message = "Not Found Role";
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

        public async Task<ApiResponse<bool>> Update(RoleUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var category = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id);

                    if (category != null)
                    {
                        var updateItem = _mapper.Map(request, category);
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Role";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Role");
                        response.Message = "Not Found Role";
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
    }
}
