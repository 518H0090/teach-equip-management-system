using AutoMapper;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTOTest>> ToiDayD()
        {
            var listData = await _unitOfWork.UserRepository.GetAllAsync();

            var returnList = _mapper.Map<IEnumerable<UserDTOTest>>(listData);

            return returnList;
        }
    }
}
