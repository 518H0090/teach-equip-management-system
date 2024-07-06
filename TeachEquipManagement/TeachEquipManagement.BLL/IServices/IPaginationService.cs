using TeachEquipManagement.BLL.BusinessModels.Dtos;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response;

namespace TeachEquipManagement.BLL.IServices
{
    public interface IPaginationService
    {
        Task<PaginationResponse<UserDTOTest>> GetUserPaginationFilter(int page = 1, int pageSize = 10, string filter = "");
    }
}
