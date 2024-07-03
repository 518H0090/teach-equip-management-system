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
    }
}
