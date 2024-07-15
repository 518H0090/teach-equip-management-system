using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.Repositories
{
    public class QueryToolRepository : IQueryToolRepository
    {
        private readonly DataContext _context;

        public QueryToolRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Tool>> GetAllToolIncludeInvoices()
        {
            return await _context.Tools.Include(x => x.Invoices).ToListAsync();
        }

        public async Task<List<Tool>> GetAllToolIncludeSuppliers()
        {
            return await _context.Tools.Include(x => x.Supplier).ToListAsync();
        }
    }
}
