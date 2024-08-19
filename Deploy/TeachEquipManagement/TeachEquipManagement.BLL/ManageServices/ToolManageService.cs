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
        private readonly IGraphService _graphService;

        public ToolManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IGraphService graphService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _graphService = graphService;
        }
        public IToolService ToolService => new ToolService(_unitOfWork, _mapper, _logger, _graphService);

        public ISupplierService SupplierService => new SupplierService(_unitOfWork, _mapper, _logger);

        public IInvoiceService InvoiceService => new InvoiceService(_unitOfWork, _mapper, _logger);

        public ICategoryService CategoryService => new CategoryService(_unitOfWork, _mapper, _logger);

        public IToolCategoryService ToolCategoryService => new ToolCategoryService(_unitOfWork, _mapper, _logger);
    }
}
