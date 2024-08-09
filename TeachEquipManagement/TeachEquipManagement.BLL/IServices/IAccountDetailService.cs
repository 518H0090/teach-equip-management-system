using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IAccountDetailService
    {
        Task<ApiResponse<bool>> Create(AccountDetailRequest request, ValidationResult validation);

        Task<ApiResponse<bool>> Update(AccountDetailUpdateRequest request, ValidationResult validation);

        Task<ApiResponse<bool>> Remove(Guid id);

        Task<ApiResponse<List<AccountDetailResponse>>> GetAll();

        Task<ApiResponse<AccountDetailResponse>> GetById(Guid id);

        Task<string> GetAccessGraphToken();
    }
}
