using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class UserPermission
    {
        public Account User { get; set; } = new();
        public Guid UserId { get; set; }

        public Permission Permission { get; set; } = new();
        public int PermissionId { get; set; }
    }
}
