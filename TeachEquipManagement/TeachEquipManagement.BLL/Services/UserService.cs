using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Serilog;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.UnitOfWorks;
using TeachEquipManagement.Utilities.CustomAttribute;
using TeachEquipManagement.Utilities.Helper;

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

        [ServiceFilter(typeof(LogFilterAttribute))]
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
