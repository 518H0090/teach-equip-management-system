using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public class QueryToolCategoryRepository : IQueryToolCategoryRepository
    {
        private readonly DataContext _context;

        public QueryToolCategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ToolCategory>> GetAllToolCategoryIncludeRelationship()
        {
            return await _context.Set<ToolCategory>().Include(x => x.Tool)
                .Include(x => x.Category).ToListAsync();
        }

        public async Task<ToolCategory> GetToolCategoryIncludeRelationship(int toolId, int categoryId)
        {
            return await _context.Set<ToolCategory>().Include(x => x.Tool)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ToolId == toolId && x.CategoryId == categoryId);
        }
    }

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
