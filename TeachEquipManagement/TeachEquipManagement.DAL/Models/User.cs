using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();

        public string Username { set; get; } = string.Empty;

        public byte[] PasswordHash { set; get; } = new byte[32];

        public byte[] PasswordSalt { set; get; } = new byte[32];

        public string Email { set; get; } = string.Empty;

        public List<UserPermission> UserPermissions { get; set; } = new();

        public RefreshToken RefreshToken { set; get; } = new();

        public Guid? RefreshTokenId { set; get; } = null;

        public List<ApprovalRequest> ApprovalRequests { get; set; } = new();

        public List<InventoryHistory> InventoryHistories { set; get; } = new();
    }
}
