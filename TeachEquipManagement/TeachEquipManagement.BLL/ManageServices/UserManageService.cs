using AutoMapper;
using Serilog;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.BLL.Services;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.OptionPattern;
using Microsoft.Extensions.Options;

namespace TeachEquipManagement.BLL.ManageServices
{
    public class UserManageService : IUserManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IOptionsSnapshot<JwtSecretKeyConfiguration> _jwtSecret;

        public UserManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger,
            IOptionsSnapshot<JwtSecretKeyConfiguration> jwtSecret)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _jwtSecret = jwtSecret;
        }

        public IAccountService UserService => new AccountService(_unitOfWork, _mapper, _logger, _jwtSecret);

        public IUserDetailService UserDetailService => new UserDetailService(_unitOfWork, _mapper, _logger);

        public ITokenService TokenService => new AccountService(_unitOfWork, _mapper, _logger, _jwtSecret);

        public IRoleService RoleService => new RoleService(_unitOfWork, _mapper, _logger);
    }
}
