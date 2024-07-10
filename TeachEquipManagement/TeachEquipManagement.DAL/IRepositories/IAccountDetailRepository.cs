using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.GenericRepository;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.IRepositories
{
    public interface IAccountDetailRepository : IGenericRepository<UserDetail>
    {
    }
}
