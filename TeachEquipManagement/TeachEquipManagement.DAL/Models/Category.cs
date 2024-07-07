using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;

        public List<ToolCategory> ToolCategories { get; set; } = new();
    }
}
