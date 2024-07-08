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
    public class UserManageService : IUserManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public IUserService UserService => new UserService(_unitOfWork, _mapper, _logger);

        public IUserPermissionService UserPermissionService => new UserPermissionService(_unitOfWork, _mapper, _logger);

        public IUserDetailService UserDetailService => new UserDetailService(_unitOfWork, _mapper, _logger);
        
        public IPermissionService PermissionService => new PermissionService(_unitOfWork, _mapper, _logger);
    }
}
