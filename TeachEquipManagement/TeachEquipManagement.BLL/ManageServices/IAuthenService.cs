using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;

namespace TeachEquipManagement.BLL.ManageServices
{
    public interface IAuthenService
    {
        IUserService UserService { get; }

        IUserPermissionService UserPermissionService { get; }

        IUserDetailService UserDetailService { get; }

        IRefreshTokenService RefreshTokenService { get; }

        IPermissionService PermissionService { get; }
    }
}
