﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public string RoleDescription { get; set; } = string.Empty;

        public virtual List<Account>? Accounts { set; get; }
    }
}
