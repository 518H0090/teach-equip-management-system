using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Account
    {
        public Guid Id { get; set; } = new Guid();

        public string Username { set; get; } = string.Empty;

        public byte[] PasswordHash { set; get; } = new byte[32];

        public byte[] PasswordSalt { set; get; } = new byte[32];

        public string Email { set; get; } = string.Empty;

        public string? RefreshToken { get; set; } = string.Empty;

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public virtual List<ApprovalRequest>? ApprovalRequests { get; set; } 

        public virtual List<InventoryHistory>? InventoryHistories { set; get; }

        public Role? Role { get; set; }
        public int? RoleId { get; set; }

        public AccountDetail AccountDetail { set; get; }
    }
}
