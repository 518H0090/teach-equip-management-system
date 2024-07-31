using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Services.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.CommonModels;
using TeachEquipManagement.Utilities.Helper;

namespace TeachEquipManagement.BLL.Services
{
    public class ApprovalRequestService : IApprovalRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ApprovalRequestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        #region Process Approval

        public async Task<ApiResponse<bool>> Create(ApprovalProcessRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var existUser = await _unitOfWork.AccountRepository.GetByIdAsync(request.AccountId);

                    var existInventory = await _unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId);

                    if (existUser == null || existInventory == null)
                    {
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Missing Object to create request";

                        return response;
                    }

                    var listRequestType = GetListRequestTypeEnum();

                    if (!listRequestType.Contains(request.RequestType))
                    {
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Invalid RequestType";

                        return response;
                    }

                    var approval = _mapper.Map<ApprovalRequest>(request);

                    approval.RequestDate = DateTime.Now;
                    approval.Status = ApprovalStatusEnum.ApprovalPending.GetDescription();

                    var entity = await _unitOfWork.ApprovalRequestRepository.InsertAsync(approval);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new ApprovalRequest successfully";
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

        public async Task<ApiResponse<List<ApprovalProcessResponse>>> GetAll()
        {
            ApiResponse<List<ApprovalProcessResponse>> response = new();

            var approvalRequests = await _unitOfWork.ApprovalRequestRepository.GetAllAsync();

            if (!approvalRequests.Any())
            {
                _logger.Warning("Warning: Not Found Any Approval Request");
                response.Data = null;
                response.Message = "Not Found Any Approval Request";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<ApprovalProcessResponse>>(approvalRequests);
                response.Data = dataResponses;
                response.Message = "List Approval Requests";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<ApprovalProcessResponse>> GetApprovalProcess(int id)
        {
            ApiResponse<ApprovalProcessResponse> response = new();

            var approvalRequest = await _unitOfWork.ApprovalRequestRepository.GetByIdAsync(id);

            if (approvalRequest != null)
            {
                var dataResponse = _mapper.Map<ApprovalProcessResponse>(approvalRequest);
                response.Data = dataResponse;
                response.Message = "Found ApprovalRequest";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found ApprovalRequest");
                response.Data = null;
                response.Message = "Not Found ApprovalRequest";
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


                var approvalRequest = await _unitOfWork.ApprovalRequestRepository.GetByIdAsync(id);

                if (approvalRequest != null)
                {
                    _unitOfWork.ApprovalRequestRepository.Delete(approvalRequest!);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.Message = "Remove Approval Request";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Approval Request");
                    response.Data = false;
                    response.Message = "Not Found Approval Request";
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error with : {e.Message}");
                response.Data = false;
                response.Message = $"{e.InnerException}";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                _unitOfWork.Rollback();
            }

            return response;
        }


        public async Task<ApiResponse<bool>> Update(ApprovalProcessUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var listRequestType = GetListApprovalStatusEnum();

                    if (!listRequestType.Contains(request.Status))
                    {
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status404NotFound;
                        response.Message = "Invalid Approval Type";

                        return response;
                    }

                    var account = await _unitOfWork.AccountRepository.GetByIdAsync(request.AccountId);
                    var inventory = await _unitOfWork.InventoryRepository.GetByIdAsync(request.InventoryId);

                    if (account == null || inventory == null)
                    {
                        response.Data = false;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.Message = "Your reference object is not found";
                        return response;
                    }

                    QueryModel<ApprovalRequest> query = new QueryModel<ApprovalRequest>
                    {
                        QueryCondition = approvalRequest => approvalRequest.Id == request.Id
                    };

                    var approvalRequest = _unitOfWork.ApprovalRequestRepository.GetQueryable(query).FirstOrDefault();

                    bool isApproved = false;

                    if (request.Status == ApprovalStatusEnum.ApprovalAccept.GetDescription())
                    {
                        int itemQuantity = inventory!.TotalQuantity - request.Quantity;

                        if ((approvalRequest.RequestType == RequestTypeEnum.RequestBorrowType.GetDescription()
                            || approvalRequest.RequestType == RequestTypeEnum.RequestSellType.GetDescription())
                            && itemQuantity < 0)
                        {
                            response.Data = false;
                            response.StatusCode = StatusCodes.Status400BadRequest;
                            response.Message = "Quantity of item in inventory is less than your borrow";
                            return response;
                        }

                        else if (approvalRequest.RequestType == RequestTypeEnum.RequestReturnType.GetDescription())
                        {
                            inventory.AmountBorrow -= request.Quantity;
                            inventory.TotalQuantity += request.Quantity;
                        }

                        else if (approvalRequest.RequestType == RequestTypeEnum.RequestBuyType.GetDescription())
                        {
                            inventory.TotalQuantity += request.Quantity;
                        }

                        else if (approvalRequest.RequestType == RequestTypeEnum.RequestBorrowType.GetDescription() && itemQuantity >= 0)
                        {
                            inventory.AmountBorrow += request.Quantity;
                            inventory.TotalQuantity -= request.Quantity;
                        }

                        else if (approvalRequest.RequestType == RequestTypeEnum.RequestSellType.GetDescription() && itemQuantity >= 0)
                        {
                            inventory.TotalQuantity -= request.Quantity;
                        }

                        isApproved = true;
                    }

                    if (approvalRequest != null)
                    {
                        if (isApproved)
                        {
                            var updateItem = _mapper.Map(request, approvalRequest);
                            updateItem.IsApproved = isApproved;
                            updateItem.ApproveDate = DateTime.Now;

                            var inventoryHistory = new InventoryHistory
                            {
                                AccountId = updateItem.AccountId,
                                InventoryId = updateItem.InventoryId,
                                Quantity = updateItem.Quantity,
                                ActionDate = DateTime.Now,
                                ActionType = updateItem.RequestType
                            };

                            await _unitOfWork.InventoryHistoryRepository.InsertAsync(inventoryHistory);
                            _unitOfWork.InventoryRepository.Update(inventory);
                        }

                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Approval Request";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Approval Request");
                        response.Message = "Not Found Approval Request";
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

        #region Process Enum
        public IEnumerable<string> GetListApprovalStatusEnum()
        {

            IEnumerable<string> approvalEnums = Enum.GetValues(typeof(ApprovalStatusEnum)).Cast<ApprovalStatusEnum>().Select(value => value.GetDescription()).ToList();

            return approvalEnums;
        }

        public IEnumerable<string> GetListRequestTypeEnum()
        {
            IEnumerable<string> requestEnums = Enum.GetValues(typeof(RequestTypeEnum)).Cast<RequestTypeEnum>().Select(value => value.GetDescription()).ToList();

            return requestEnums;
        }

        #endregion

    }
}
