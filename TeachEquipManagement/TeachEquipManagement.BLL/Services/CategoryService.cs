using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.BusinessModels.Common;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<ApiResponse<bool>> Create(CategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<CategoryResponse>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<CategoryResponse>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> Update(SupplierUpdateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> Update(CategoryUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
