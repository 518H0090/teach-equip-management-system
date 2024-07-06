using AutoMapper;
using Microsoft.TeamFoundation.Work.WebApi;
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
    public class ManageService : IManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IUserService UserService =>  new UserService(_unitOfWork, _mapper, _logger);

        public IUserPermissionService UserPermissionService => new UserPermissionService(_unitOfWork, _mapper, _logger);

        public IUserDetailService UserDetailService =>  new UserDetailService(_unitOfWork, _mapper, _logger);

        public IToolService ToolService =>  new ToolService(_unitOfWork, _mapper, _logger);

        public ISupplierService SupplierService =>  new SupplierService(_unitOfWork, _mapper, _logger);

        public IRefreshTokenService RefreshTokenService =>  new RefreshTokenService(_unitOfWork, _mapper, _logger);

        public IPermissionService PermissionService =>  new PermissionService(_unitOfWork, _mapper, _logger);

        public IInvoiceService InvoiceService =>  new InvoiceService(_unitOfWork, _mapper, _logger);

        public IInventoryService InventoryService =>  new InventoryService(_unitOfWork, _mapper, _logger);

        public IInventoryHistoryService InventoryHistoryService =>  new InventoryHistoryService(_unitOfWork, _mapper, _logger);

        public ICategoryService CategoryService =>  new CategoryService(_unitOfWork, _mapper, _logger);

        public IApprovalRequestService ApprovalRequestService =>  new ApprovalRequestService(_unitOfWork, _mapper, _logger);
    }
}
