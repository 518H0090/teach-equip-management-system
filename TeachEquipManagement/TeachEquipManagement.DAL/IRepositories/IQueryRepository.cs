using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.IRepositories
{
    public interface IQueryToolRepository
    {
        Task<List<Tool>> GetAllToolIncludeSuppliers();

        Task<List<Tool>> GetAllToolIncludeInvoices();
    }

    public interface IQuerySupplierRepository
    {
        Task<List<Supplier>> GetAllSupplierIncludeTools();
    }

    public interface IQueryToolCategoryRepository
    {
        Task<List<ToolCategory>> GetAllToolCategoryIncludeRelationship();

        Task<ToolCategory> GetToolCategoryIncludeRelationship(int toolId, int categoryId);
    }

    public interface IQueryInvoiceRepository
    {
        Task<List<Invoice>> GetAllInvoiceIncludeTools();
    }
}