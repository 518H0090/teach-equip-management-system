using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class ApprovalRequest
    {
        public Guid UserId { get; set; }

        public Guid InventoryId { get; set; }   

        public int Quantity {  get; set; }  

        public string RequestType { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public string ManagerApprove { get; set; } = string.Empty;

        public DateTime ApproveDate { get; set; }   

        public bool IsApproved { get; set; }

        public Account User { get; set; } = new();

        public Inventory Inventory { get; set; } = new();
    }
}
