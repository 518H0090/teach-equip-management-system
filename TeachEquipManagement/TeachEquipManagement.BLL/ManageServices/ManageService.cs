using AutoMapper;
using Microsoft.Extensions.Options;
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
using TeachEquipManagement.Utilities.OptionPattern;

namespace TeachEquipManagement.BLL.ManageServices
{
    public class ManageService : IManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IOptionsSnapshot<JwtSecretKeyConfiguration> _jwtSecret;

        public ManageService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger,
            IOptionsSnapshot<JwtSecretKeyConfiguration> jwtSecret)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _jwtSecret = jwtSecret;
        }

        public IToolManageService ToolManageService =>  new ToolManageService(_unitOfWork, _mapper, _logger);

        public IInventoryManageService InventoryManageService =>  new InventoryManageService(_unitOfWork, _mapper, _logger);

        public IUserManageService AuthenService => new UserManageService(_unitOfWork, _mapper, _logger, _jwtSecret);
    }
}
