using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class AccountDetailService : IAccountDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IGraphService _graphService;

        public AccountDetailService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IGraphService graphService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _graphService = graphService;
        }

        public async Task<ApiResponse<bool>> Create(AccountDetailRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var existUser = await _unitOfWork.AccountRepository.GetByIdAsync(request.UserId);

                    if (existUser == null) {

                        _logger.Warning("Warning: Not Found User To Mapping");
                        response.Data = false;
                        response.Message = "Not Found User To Mapping";
                        response.StatusCode = StatusCodes.Status404NotFound;

                        return response;

                    }

                    var existUserDetail = await _unitOfWork.AccountDetailRepository.GetByIdAsync(request.UserId);

                    if (existUserDetail != null)
                    {
                        _logger.Warning("Warning: UseDetail is already exist");
                        response.Data = false;
                        response.Message = "UseDetail is already exist";
                        response.StatusCode = StatusCodes.Status400BadRequest;

                        return response;
                    }

                    var accountDetail = _mapper.Map<AccountDetail>(request);

                    var entity = await _unitOfWork.AccountDetailRepository.InsertAsync(accountDetail);
                    await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    response.Data = true;
                    response.StatusCode = StatusCodes.Status201Created;
                    response.Message = "Create new AccountDetail successfully";
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

        public async Task<ApiResponse<List<AccountDetailResponse>>> GetAll()
        {
            ApiResponse<List<AccountDetailResponse>> response = new();

            var accountDetails = await _unitOfWork.AccountDetailRepository.GetAllAsync();

            if (!accountDetails.Any())
            {
                _logger.Warning("Warning: Not Found Any Account Detail");
                response.Data = null;
                response.Message = "Not Found Any Data";
                response.StatusCode = StatusCodes.Status404NotFound;
            }

            else
            {
                var dataResponses = _mapper.Map<List<AccountDetailResponse>>(accountDetails);
                response.Data = dataResponses;
                response.Message = "List Account Details";
                response.StatusCode = StatusCodes.Status200OK;
            }

            return response;
        }

        public async Task<ApiResponse<AccountDetailResponse>> GetById(Guid id)
        {
            ApiResponse<AccountDetailResponse> response = new();

            var accountDetail = await _unitOfWork.AccountDetailRepository.GetByIdAsync(id);

            if (accountDetail != null)
            {
                var dataResponse = _mapper.Map<AccountDetailResponse>(accountDetail);
                response.Data = dataResponse;
                response.Message = "Found Account Detail";
                response.StatusCode = StatusCodes.Status200OK;
            }

            else
            {
                _logger.Warning("Warning: Not Found Account Detail");
                response.Data = null;
                response.Message = "Not Found Account Detail";
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

                var accountDetail = await _unitOfWork.AccountDetailRepository.GetByIdAsync(id);

                if (accountDetail != null)
                {
                    _unitOfWork.AccountDetailRepository.Delete(accountDetail!);

                    var isSuccess = await _unitOfWork.SaveChangesAsync();

                    _unitOfWork.Commit();

                    if (isSuccess && accountDetail.SpoFileId != null)
                    {
                        await _graphService.DeleteDriveItemAsync(accountDetail.SpoFileId);
                    }

                    response.Data = isSuccess;
                    response.Message = "Remove Account Detail";
                    response.StatusCode = StatusCodes.Status202Accepted;
                }

                else
                {
                    _logger.Warning("Warning: Not Found Account Detail");
                    response.Message = "Not Found Account Detail";
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

        public async Task<ApiResponse<bool>> Update(AccountDetailUpdateRequest request, ValidationResult validation)
        {
            ApiResponse<bool> response = new();

            try
            {
                if (validation.IsValid)
                {
                    _unitOfWork.CreateTransaction();

                    var accountDetail = await _unitOfWork.AccountDetailRepository.GetByIdAsync(request.UserId);

                    if (accountDetail != null)
                    {
                        var updateItem = _mapper.Map(request, accountDetail);

                        if (request.FileUpload != null)
                        {
                            var spoFileId = await _graphService.UploadDriveItemAsync(request.FileUpload);
                            var urlShareFile = await _graphService.GetItemShareLink(spoFileId);

                            if (!string.IsNullOrEmpty(spoFileId) || !string.IsNullOrEmpty(urlShareFile))
                            {
                                updateItem.SpoFileId = spoFileId;
                                updateItem.Avatar = urlShareFile;
                            }
                        }

                        await _unitOfWork.SaveChangesAsync();

                        _unitOfWork.Commit();

                        response.Data = true;
                        response.Message = "Update Account Detail";
                        response.StatusCode = StatusCodes.Status202Accepted;
                    }

                    else
                    {
                        _logger.Warning("Warning: Not Found Account Detail");
                        response.Message = "Not Found Account Detail";
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
