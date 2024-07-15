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
        IAccountService AccountService { get; }

        ITokenService TokenService { get; }

        IRoleService RoleService { get; }

        IAccountDetailService AccountDetailService { get; }

    }
}
