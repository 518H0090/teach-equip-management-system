using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class InventoryHistory
    {
        public Guid UserId { get; set; }

        public Guid InventoryId { get; set; }   

        public int Quantity { get; set; }

        public DateTime ActionDate { get; set; }   

        public string ActionType { get; set; } = string.Empty;

        public User User { get; set; } = new();

        public Inventory Inventory { get; set; } = new();
    }
}
