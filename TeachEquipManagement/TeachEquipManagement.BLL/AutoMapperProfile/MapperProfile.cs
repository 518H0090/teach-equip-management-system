using AutoMapper;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<User, UserDTOTest>();

            //CreateMap<UserDTOTest, User>();

            CreateMap<SupplierRequest, Supplier>();
            CreateMap<Supplier, SupplierResponse>();
            CreateMap<SupplierUpdateRequest, Supplier>();

            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();
            CreateMap<CategoryUpdateRequest, Category>();
        }
    }
}
