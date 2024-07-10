using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.EFContext;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.IRepositories;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.Repositories
{
    public class UserPermissionRepository : GenericRepository<UserPermission>, IAccountPermissionRepository
    {
        private readonly DataContext _context;

        public UserPermissionRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
