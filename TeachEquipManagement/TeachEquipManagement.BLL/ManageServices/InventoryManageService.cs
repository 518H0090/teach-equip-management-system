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
    public class InventoryManageService : IInventoryManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public InventoryManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IInventoryService InventoryService => new InventoryService(_unitOfWork, _mapper, _logger);

        public IInventoryHistoryService InventoryHistoryService => new InventoryHistoryService(_unitOfWork, _mapper, _logger);

        public IApprovalRequestService ApprovalRequestService => new ApprovalRequestService(_unitOfWork, _mapper, _logger);
    }
}
