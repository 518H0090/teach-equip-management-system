using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class AccountDetail
    {
        public Account User { get; set; } = new();
            
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;    

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Avatar { get; set; } = string.Empty;

        public string? SpoFileId { get; set; } = string.Empty;

    }
}
