using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.Repositories
{
    public class QueryInvoiceRepository : IQueryInvoiceRepository
    {
        private readonly DataContext _context;

        public QueryInvoiceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAllInvoiceIncludeTools()
        {
            return await _context.Invoices.Include(x => x.Tool).ToListAsync();
        }
    }
}
