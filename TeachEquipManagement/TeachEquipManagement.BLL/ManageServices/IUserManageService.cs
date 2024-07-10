using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;

namespace TeachEquipManagement.BLL.ManageServices
{
    public interface IUserManageService
    {
        IAccountService UserService { get; }

        ITokenService TokenService { get; }

        IRoleService RoleService { get; }

        IUserDetailService UserDetailService { get; }

    }
}
