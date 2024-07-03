﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class UserPermission
    {
        public User User { get; set; }
        public Guid UserId { get; set; }

        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
    }
}
