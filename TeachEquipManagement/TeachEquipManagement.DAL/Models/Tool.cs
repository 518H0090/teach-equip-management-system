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

        public string SubjectRelative { get; set; } = string.Empty;
    }
}
