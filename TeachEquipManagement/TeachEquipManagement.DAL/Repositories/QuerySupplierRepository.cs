using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.Repositories
{
    public class QuerySupplierRepository : IQuerySupplierRepository
    {
        private readonly DataContext _context;

        public QuerySupplierRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Supplier>> GetAllSupplierIncludeTools()
        {
            return await _context.Set<Supplier>().Include(x => x.Tools).ToListAsync();
        }
    }
}
