using AutoMapper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.Services;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.ManageServices
{
    public class ToolManageService : IToolManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ToolManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public IToolService ToolService => new ToolService(_unitOfWork, _mapper, _logger);

        public ISupplierService SupplierService => new SupplierService(_unitOfWork, _mapper, _logger);

        public IInvoiceService InvoiceService => new InvoiceService(_unitOfWork, _mapper, _logger);

        public ICategoryService CategoryService => new CategoryService(_unitOfWork, _mapper, _logger);

        public IToolCategoryService ToolCategoryService => new ToolCategoryService(_unitOfWork, _mapper, _logger);
    }
}
