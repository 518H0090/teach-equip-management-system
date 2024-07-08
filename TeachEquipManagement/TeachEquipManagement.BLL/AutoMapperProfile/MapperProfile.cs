using AutoMapper;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SupplierRequest, Supplier>();
            CreateMap<Supplier, SupplierResponse>();
            CreateMap<SupplierUpdateRequest, Supplier>();
            CreateMap<Supplier, SupplierIncludeToolResponse>();

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
            CreateMap<ToolUpdateRequest, Tool>();

            CreateMap<ToolCategoryRequest, ToolCategory>();
            CreateMap<ToolCategory, ToolCategoryResponse>();

            CreateMap<InvoceRequest, Invoice>();
            CreateMap<Invoice, InvoiceResponse>();
            CreateMap<InvoceUpdateRequest, Invoice>();
            CreateMap<Invoice, InvoiceIncludeToolResponse>();

            CreateMap<User, UserResponse>();
            CreateMap<UserUpdateRequest, User>();
        }
    }
}
