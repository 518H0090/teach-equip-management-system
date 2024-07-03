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

        public string SupplierName { get; set; }

        public string ContactName { get; set; } = string.Empty;

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
