using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IInvoiceService
    {
        Task<ApiResponse<bool>> Create(InvoceRequest request, ValidationResult validation);

        Task<ApiResponse<bool>> Update(InvoceUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(int id);

        Task<ApiResponse<List<InvoiceResponse>>> GetAll();

        Task<ApiResponse<InvoiceResponse>> GetById(int id);

        Task<ApiResponse<InvoiceResponse>> GetLatestInvoiceWithToolId(int toolId);

        Task<ApiResponse<List<InvoiceIncludeToolResponse>>> GetAllIncludeTools();

        Task<ApiResponse<InvoiceIncludeToolResponse>> GetLatestInvoiceWithTool(int toolId);
    }
}
