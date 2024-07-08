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
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Create(InvoceRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var tool = await _unitOfWork.ToolRepository.GetByIdAsync(request.ToolId);

                    if (tool == null)
                    {
                        _logger.Warning($"Warning with : Not Found Tool");
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Message = "Not Found Tool";

                        return response;
                    }

                    var invoice = _mapper.Map<Invoice>(request);

                    var entity = await _unitOfWork.InvoiceRepository.InsertAsync(invoice);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new Invoice successfully";
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

        public async Task<ApiResponse<List<InvoiceResponse>>> GetAll()
        {
            ApiResponse<List<InvoiceResponse>> response = new();

            var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();

            if (!invoices.Any())
            {
                _logger.Warning("Warning: Not Found Any Invoices");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<InvoiceResponse>>(invoices);
                response.Data = dataResponses;
                response.Message = "List Invoices";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<List<InvoiceIncludeToolResponse>>> GetAllIncludeTools()
        {
            ApiResponse<List<InvoiceIncludeToolResponse>> response = new();

            var invoices = await _unitOfWork.QueryInvoiceRepository.GetAllInvoiceIncludeTools();

            if (!invoices.Any())
            {
                _logger.Warning("Warning: Not Found Any Invoices");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<InvoiceIncludeToolResponse>>(invoices);
                response.Data = dataResponses;
                response.Message = "List Invoices";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<InvoiceResponse>> GetById(int id)
        {
            ApiResponse<InvoiceResponse> response = new();

            var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);

            if (invoice != null)
            {
                var dataResponse = _mapper.Map<InvoiceResponse>(invoice);
                response.Data = dataResponse;
                response.Message = "Found Invoice";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Invoice");
                response.Data = null;
                response.Message = "Not Found Invoice";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<InvoiceIncludeToolResponse>> GetLatestInvoiceWithTool(int toolId)
        {
            ApiResponse<InvoiceIncludeToolResponse> response = new();

            var invoices = await _unitOfWork.QueryInvoiceRepository.GetAllInvoiceIncludeTools();
            var lastPrice = invoices.LastOrDefault(x => x.ToolId == toolId);

            if (lastPrice != null)
            {
                var dataResponse = _mapper.Map<InvoiceIncludeToolResponse>(lastPrice);
                response.Data = dataResponse;
                response.Message = "Found Invoice";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Invoice with ToolId");
                response.Data = null;
                response.Message = "Not Found Invoice with ToolId";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            return response;
        }

        public async Task<ApiResponse<InvoiceResponse>> GetLatestInvoiceWithToolId(int toolId)
        {
            ApiResponse<InvoiceResponse> response = new();

            var invoices = await _unitOfWork.InvoiceRepository.GetAllAsync();
            var lastPrice = invoices.LastOrDefault(x => x.ToolId == toolId);

            if (lastPrice != null)
            {
                var dataResponse = _mapper.Map<InvoiceResponse>(lastPrice);
                response.Data = dataResponse;
                response.Message = "Found Invoice";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Invoice with ToolId");
                response.Data = null;
                response.Message = "Not Found Invoice with ToolId";
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

                var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(id);

                if (invoice != null)
                {
                    _unitOfWork.InvoiceRepository.Delete(invoice!);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.Message = "Remove Invoice";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Invoice");
                    response.Message = "Not Found Invoice";
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

        public async Task<ApiResponse<bool>> Update(InvoceUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var invoice = await _unitOfWork.InvoiceRepository.GetByIdAsync(request.Id);

                    if (invoice != null)
                    {
                        var updateItem = _mapper.Map(request, invoice);
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Invoice";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Invoice");
                        response.Message = "Not Found Invoice";
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
