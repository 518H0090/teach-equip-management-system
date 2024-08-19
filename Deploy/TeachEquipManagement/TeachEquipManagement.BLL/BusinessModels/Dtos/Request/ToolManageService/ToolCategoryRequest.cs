using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService
{
    public class ToolCategoryRequest
    {
        [JsonProperty("tool_id")]
        public int ToolId { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
    }
}
