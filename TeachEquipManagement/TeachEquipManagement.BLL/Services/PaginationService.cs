using AutoMapper;
using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.DAL.UnitOfWorks;

namespace TeachEquipManagement.BLL.Services
{
    public class PaginationService : IPaginationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<UserDTOTest>> GetUserPaginationFilter(int page = 1, int pageSize = 10, string filter = "")
        {
            var query = await _unitOfWork.UserRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Username.Contains(filter));
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var queryResponse = _mapper.Map<List<UserDTOTest>>(query);

            var result = new PaginationResponse<UserDTOTest>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Data = queryResponse.ToList()
            };

            return result;
        }
    }
}
