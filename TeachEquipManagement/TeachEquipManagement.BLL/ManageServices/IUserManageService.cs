﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;

namespace TeachEquipManagement.BLL.ManageServices
{
    public interface IUserManageService
    {
        IUserService UserService { get; }

        IUserPermissionService UserPermissionService { get; }

        IUserDetailService UserDetailService { get; }

        IPermissionService PermissionService { get; }
    }
}
