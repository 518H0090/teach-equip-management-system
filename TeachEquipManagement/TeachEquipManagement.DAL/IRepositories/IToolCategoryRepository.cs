using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.Models;
using TeachEquipManagement.DAL.Repositories;

namespace TeachEquipManagement.DAL.IRepositories
{
    public interface IToolCategoryRepository : IGenericRepository<ToolCategory>
    {
    }
}
