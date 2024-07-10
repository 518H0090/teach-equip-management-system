using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class ToolService : IToolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ToolService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Create(ToolRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var existSupplier = await _unitOfWork.SupplierRepository.GetByIdAsync(request.SupplierId);

                    if (existSupplier == null)
                    {
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Message = "Not Found Supplier";

                        return response;
                    }

                    var tool = new Tool
                    {
                        Supplier = existSupplier,
                        Description = request.Description,
                        ToolName = request.ToolName
                    };

                    var entity = await _unitOfWork.ToolRepository.InsertAsync(tool);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new Tool successfully";
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

        public async Task<ApiResponse<List<ToolResponse>>> GetAll()
        {
            ApiResponse<List<ToolResponse>> response = new();

            var tools = await _unitOfWork.ToolRepository.GetAllAsync();

            if (!tools.Any())
            {
                _logger.Warning("Warning: Not Found Any Tool");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<ToolResponse>>(tools);
                response.Data = dataResponses;
                response.Message = "List tools";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<List<ToolIncludeSupplierResponse>>> GetAllIncludeSupplier()
        {
            ApiResponse<List<ToolIncludeSupplierResponse>> response = new();

            var tools = await _unitOfWork.QueryToolRepository.GetAllToolIncludeSuppliers();

            if (!tools.Any())
            {
                _logger.Warning("Warning: Not Found Any Tool");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<ToolIncludeSupplierResponse>>(tools);
                response.Data = dataResponses;
                response.Message = "List tools";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<ToolResponse>> GetById(int id)
        {
            ApiResponse<ToolResponse> response = new();

            var tool = await _unitOfWork.ToolRepository.GetByIdAsync(id);

            if (tool != null)
            {
                var dataResponses = _mapper.Map<ToolResponse>(tool);
                response.Data = dataResponses;
                response.Message = "Found Tool";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Tool");
                response.Data = null;
                response.Message = "Not Found Tool";
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

                var tool = await _unitOfWork.ToolRepository.GetByIdAsync(id);

                if (tool != null)
                {
                    _unitOfWork.ToolRepository.Delete(tool!);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.Message = "Remove Tool";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Tool");
                    response.Message = "Not Found Tool";
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

        public async Task<ApiResponse<bool>> Update(ToolUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var tool = await _unitOfWork.ToolRepository.GetByIdAsync(request.Id);

                    if (tool != null)
                    {
                        var updateSupplier = _mapper.Map(request, tool);
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Tool";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Supplier");
                        response.Message = "Not Found Supplier";
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
