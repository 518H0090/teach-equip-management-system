
using AutoMapper;
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

        public async Task<ApiResponse<bool>> Create(SupplierRequest request)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                _unitOfWork.CreateTransaction();

                var supplier = _mapper.Map<Supplier>(request);

                await _unitOfWork.SupplierRepository.InsertAsync(supplier);
                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.Commit();
                response.StatusCode = StatusCodes.Status201Created;
                response.Message = "Create new supplier successfully";
            } 
            
            catch(Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status400BadRequest;
                _unitOfWork.Rollback();
            };

            return response;
        }

        public Task<ApiResponse<SupplierResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<SupplierResponse>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> Update(SupplierRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
