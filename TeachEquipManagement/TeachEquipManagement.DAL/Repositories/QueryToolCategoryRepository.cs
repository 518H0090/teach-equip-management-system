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
}
