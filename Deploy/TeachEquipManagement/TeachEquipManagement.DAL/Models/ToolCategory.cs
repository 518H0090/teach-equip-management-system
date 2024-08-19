using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class ToolCategory
    {
        public Tool Tool { get; set; }
        public int ToolId { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
