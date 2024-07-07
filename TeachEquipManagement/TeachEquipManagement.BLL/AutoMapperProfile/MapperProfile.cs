using AutoMapper;
using Microsoft.Graph.Models;
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

            CreateMap<ToolRequest, Tool>()
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
            .ForMember(dest => dest.ToolName, opt => opt.MapFrom(src => src.ToolName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForAllMembers(opt => opt.Ignore());
            CreateMap<Tool, ToolResponse>();
            CreateMap<Tool, ToolIncludeSupplierResponse>();
        }
    }
}
