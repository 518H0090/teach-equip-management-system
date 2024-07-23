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

        public virtual Tool? Tool { get; set; }

        public int ToolId {set; get;}

        public virtual List<ApprovalRequest>? ApprovalRequests { get; set; }

        public virtual List<InventoryHistory>? InventoryHistories { set; get; }
    }
}
