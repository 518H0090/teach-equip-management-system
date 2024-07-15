using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Polly;
using Polly.Retry;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _retryPolicy = Policy
                          .Handle<Exception>()
                          .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(3),
                            (exception, timeSpan, retryCount, context) =>
                            {
                                _logger.Error(exception, $"Retry attempt {retryCount} failed due to {exception.Message}.");
                            });
        }

        public async Task<ApiResponse<bool>> Create(InventoryRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var isExistTool = await _unitOfWork.ToolRepository.GetByIdAsync(request.ToolId);

                    if (isExistTool == null)
                    {
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Not Found Any Tool";

                        return response;
                    }

                    var invetory = _mapper.Map<Inventory>(request);

                    Guid InventoryId = await _retryPolicy.ExecuteAsync(async () =>
                    {
                        InventoryId = Guid.NewGuid();

                        var existUser = await _unitOfWork.InventoryRepository.GetByIdAsync(InventoryId);

                        if (existUser != null)
                        {
                            throw new Exception("InventoryId Exist Please Try Again.");
                        }

                        return InventoryId;
                    });

                    invetory.Id = InventoryId;

                    var entity = await _unitOfWork.InventoryRepository.InsertAsync(invetory);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new Inventory successfully";
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

        public async Task<ApiResponse<List<InventoryResponse>>> GetAll()
        {
            ApiResponse<List<InventoryResponse>> response = new();

            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();

            if (!inventories.Any())
            {
                _logger.Warning("Warning: Not Found Any Inventory");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<InventoryResponse>>(inventories);
                response.Data = dataResponses;
                response.Message = "List Inventories";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<InventoryResponse>> GetById(Guid id)
        {
            ApiResponse<InventoryResponse> response = new();

            var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(id);

            if (inventory != null)
            {
                var dataResponse = _mapper.Map<InventoryResponse>(inventory);
                response.Data = dataResponse;
                response.Message = "Found Inventory";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Inventory");
                response.Data = null;
                response.Message = "Not Found Inventory";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Remove(Guid id)
        {
            ApiResponse<bool> response = new();

            try
            {
                _unitOfWork.CreateTransaction();

                var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(id);

                if (inventory != null)
                {
                    _unitOfWork.InventoryRepository.Delete(inventory!);
                    var isSuccess = await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = isSuccess;
                    response.Message = "Remove Inventory";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Inventory");
                    response.Message = "Not Found Inventory";
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

        public async Task<ApiResponse<bool>> Update(InventoryUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(request.Id);

                    if (inventory != null)
                    {
                        var updateItem = _mapper.Map(request, inventory);
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Inventory";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Inventory");
                        response.Message = "Not Found Inventory";
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
