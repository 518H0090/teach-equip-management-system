using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class InventoryHistory
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public Guid InventoryId { get; set; }   

        public int Quantity { get; set; }

        public DateTime ActionDate { get; set; }   

        public string ActionType { get; set; } = string.Empty;

        public Account User { get; set; }

        public Inventory Inventory { get; set; }
    }
}
