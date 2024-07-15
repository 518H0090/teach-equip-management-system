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
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
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

                    var approval = _mapper.Map<ApprovalRequest>(request);

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

        public Task<ApiResponse<List<ApprovalProcessResponse>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ApprovalProcessResponse>> GetById(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<ApiResponse<bool>> Remove(int id)
        {
            throw new NotImplementedException();
        }
     

        public Task<ApiResponse<bool>> Update(ApprovalProcessUpdateRequest request, ValidationResult validation)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Process Enum
        public IEnumerable<string> GetListApprovalEnum()
        {

            IEnumerable<string> approvalEnums = Enum.GetValues(typeof(ApprovalEnum)).Cast<ApprovalEnum>().Select(value => value.GetDescription()).ToList();

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
