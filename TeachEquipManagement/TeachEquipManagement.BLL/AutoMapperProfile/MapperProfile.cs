using AutoMapper;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService;
using TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage;
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
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Tool, ToolResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Tool, ToolIncludeSupplierResponse>();
            CreateMap<ToolUpdateRequest, Tool>();

            CreateMap<ToolCategoryRequest, ToolCategory>();
            CreateMap<ToolCategory, ToolCategoryResponse>();

            CreateMap<InvoceRequest, Invoice>();
            CreateMap<Invoice, InvoiceResponse>();
            CreateMap<InvoceUpdateRequest, Invoice>();
            CreateMap<Invoice, InvoiceIncludeToolResponse>();

            CreateMap<Account, AccountResponse>();
            CreateMap<AccountUpdateRequest, Account>();

            CreateMap<RoleRequest, Role>();
            CreateMap<Role, RoleResponse>();
            CreateMap<RoleUpdateRequest, Role>();

            CreateMap<AccountDetailRequest, AccountDetail>();
            CreateMap<AccountDetail, AccountDetailResponse>();
            CreateMap<AccountDetailUpdateRequest, AccountDetail>();

            CreateMap<InventoryRequest, Inventory>();
            CreateMap<Inventory, InventoryResponse>();
            CreateMap<InventoryUpdateRequest, Inventory>();

            CreateMap<ApprovalProcessRequest, ApprovalRequest>();
            CreateMap<ApprovalRequest, ApprovalProcessResponse>();
            CreateMap<ApprovalProcessUpdateRequest, ApprovalRequest>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ManagerApprove, opt => opt.MapFrom(src => src.ManagerApprove))
             .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.IsApproved));
        }
    }
}
