using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public string ContactName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public virtual List<Tool>? Tools { get; set; }
    }
}
