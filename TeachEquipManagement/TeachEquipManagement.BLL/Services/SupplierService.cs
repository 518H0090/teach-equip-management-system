
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SupplierService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Create(SupplierRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var supplier = _mapper.Map<Supplier>(request);

                    var entity = await _unitOfWork.SupplierRepository.InsertAsync(supplier);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new supplier successfully";
                } 

                else
                {
                    response.Data = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = validation.ToString();
                }
            } 
            
            catch(Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            };

            return response;
        }

        public async Task<ApiResponse<List<SupplierResponse>>> GetAll()
        {
            ApiResponse<List<SupplierResponse>> response = new();

            var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();

            if (!suppliers.Any())
            {
                _logger.Warning("Warning: Not Found Any Supplier");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<SupplierResponse>>(suppliers);
                response.Data = dataResponses;
                response.Message = "List Suppliers";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<List<SupplierIncludeToolResponse>>> GetAllIncludeTools()
        {
            ApiResponse<List<SupplierIncludeToolResponse>> response = new();

            var suppliers = await _unitOfWork.SupplierRepository.GetAllAsync();
            

            if (!suppliers.Any())
            {
                _logger.Warning("Warning: Not Found Any Supplier");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var tools = await _unitOfWork.ToolRepository.GetAllAsync();

                foreach (var supplier in suppliers)
                {
                    supplier.Tools = tools.Where(x => x.SupplierId == supplier.Id).ToList();
                }

                var dataResponses = _mapper.Map<List<SupplierIncludeToolResponse>>(suppliers);
                response.Data = dataResponses;
                response.Message = "List Suppliers";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<SupplierResponse>> GetById(int id)
        {
            ApiResponse<SupplierResponse> response = new();

            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);

            if (supplier != null)
            {
                var dataResponses = _mapper.Map<SupplierResponse>(supplier);
                response.Data = dataResponses;
                response.Message = "Found Supplier";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Supplier");
                response.Data = null;
                response.Message = "Not Found Supplier";
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

                var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(id);

                if (supplier != null)
                {
                    _unitOfWork.SupplierRepository.Delete(supplier!);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.Message = "Remove Supplier";
                    response.StatusCode = StatusCodes.Status202Accepted;
                } 

                else
                {
                    _logger.Warning("Warning: Not Found Supplier");
                    response.Message = "Not Found Supplier";
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception e) {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }

        public async Task<ApiResponse<bool>> Update(SupplierUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(request.Id);

                    if (supplier != null)
                    {
                        var updateSupplier = _mapper.Map(request, supplier);
                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Supplier";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Supplier");
                        response.Message = "";
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
