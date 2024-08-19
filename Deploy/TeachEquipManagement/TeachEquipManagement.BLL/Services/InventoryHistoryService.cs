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
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.CommonModels;

namespace TeachEquipManagement.BLL.Services
{
    public class InventoryHistoryService : IInventoryHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public InventoryHistoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<List<InventoryHistoryResponse>>> GetAll()
        {
            ApiResponse<List<InventoryHistoryResponse>> response = new();

            var inventoryHistories = await _unitOfWork.InventoryHistoryRepository.GetAllAsync();

            if (!inventoryHistories.Any())
            {
                _logger.Warning("Warning: Not Found Any Inventory History");
                response.Data = null;
                response.Message = "Not Found Any Inventory History";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<InventoryHistoryResponse>>(inventoryHistories);
                response.Data = dataResponses;
                response.Message = "List Inventory Histories";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public ApiResponse<InventoryHistoryResponse> GetInventoryHistory(ProcessRequest request, ValidationResult validation)
        {
            ApiResponse<InventoryHistoryResponse> response = new();

            if (validation.IsValid)
            {
                QueryModel<InventoryHistory> query = new QueryModel<InventoryHistory>
                {
                    QueryCondition = approvalRequest => approvalRequest.InventoryId == request.InventoryId && approvalRequest.AccountId == request.AccountId
                };

                var approvalRequest = _unitOfWork.InventoryHistoryRepository.GetQueryable(query).FirstOrDefault();

                if (approvalRequest != null)
                {
                    var dataResponse = _mapper.Map<InventoryHistoryResponse>(approvalRequest);
                    response.Data = dataResponse;
                    response.Message = "Found Inventory History";
                    response.StatusCode = StatusCodes.Status200OK;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Inventory History");
                    response.Data = null;
                    response.Message = "Not Found Inventory History";
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
            }

            else
            {
                _logger.Warning("Warning: Invalid Request");
                response.Data = null;
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Message = validation.ToString();
            }

            return response;
        }
    }
}
