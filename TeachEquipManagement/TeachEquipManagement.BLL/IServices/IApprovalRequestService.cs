using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IApprovalRequestService
    {
        Task<ApiResponse<bool>> Create(ApprovalProcessRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Update(ApprovalProcessUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(ProcessRequest request, ValidationResult validation);
        Task<ApiResponse<List<ApprovalProcessResponse>>> GetAll();
        ApiResponse<ApprovalProcessResponse> GetApprovalProcess(ProcessRequest request, ValidationResult validation);

        IEnumerable<string> GetListApprovalStatusEnum();

        IEnumerable<string> GetListRequestTypeEnum();
    }
}
