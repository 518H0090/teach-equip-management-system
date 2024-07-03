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

        public DateTime TransactionDate { get; set; }   

        public string ActionType { get; set; }
    }
}
