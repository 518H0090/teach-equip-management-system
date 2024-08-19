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

        public string ToolName { get; set; } 

        public string Description { get; set; }

        public virtual Supplier Supplier { get; set; } 

        public int SupplierId { get; set; }

        public string? Avatar { get; set; } = string.Empty;

        public string? SpoFileId { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;

        public virtual Inventory Inventory { get; set; } 

        public virtual List<Invoice> Invoices { get; set; } 

        public virtual List<ToolCategory> ToolCategories { get; set; } 
    }
}
