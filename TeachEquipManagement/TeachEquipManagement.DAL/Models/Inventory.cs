using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }

        public int TotalQuantity { get; set; }

        public int AmountBorrow { get; set; }

        public Tool Tool { get; set; } = new();

        public int ToolId {set; get;}

        public List<ApprovalRequest> ApprovalRequests { get; set; } = new();

        public List<InventoryHistory> InventoryHistories { set; get; } = new();
    }
}
