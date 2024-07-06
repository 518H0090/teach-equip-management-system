using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Tool
    {
        public int Id { get; set; }

        public string ToolName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string SubjectRelative { get; set; } = string.Empty;

        public Category Category { get; set; } = new();
        public int CategoryId { get; set; }

        public Supplier Supplier { get; set; } = new();

        public int SupplierId { get; set; }

        public Inventory Inventory { get; set; } = new();

        public List<Invoice> Invoices { get; set; } = new();
    }
}
