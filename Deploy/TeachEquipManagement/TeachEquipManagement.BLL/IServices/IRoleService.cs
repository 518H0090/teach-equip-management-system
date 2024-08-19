﻿using FluentValidation.Results;
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
    public interface IRoleService
    {
        Task<ApiResponse<bool>> Create(RoleRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Update(RoleUpdateRequest request, ValidationResult validation);
        Task<ApiResponse<bool>> Remove(int id);
        Task<ApiResponse<List<RoleResponse>>> GetAll();
        Task<ApiResponse<RoleResponse>> GetById(int id);
    }
}
