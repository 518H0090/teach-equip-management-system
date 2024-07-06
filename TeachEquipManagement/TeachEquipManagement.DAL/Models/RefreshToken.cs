using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; } 

        public string Token { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public User User { get; set; } = new User();
    }
}
