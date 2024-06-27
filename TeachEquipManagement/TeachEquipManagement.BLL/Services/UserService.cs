using AutoMapper;
using Serilog;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.Specifications;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDTOTest>> ToiDayD()
        {
            var listData = await _unitOfWork.UserRepository.GetAllAsync();

            var NewList = new List<string>
            {
                "hehe",
                "haha",
                "FOOFOF",
                "Safasfsa"
            };

            _logger.Information("Test {@listData}", NewList);

            var returnList = _mapper.Map<IEnumerable<UserDTOTest>>(listData);

            return returnList;
        }
    }
}
