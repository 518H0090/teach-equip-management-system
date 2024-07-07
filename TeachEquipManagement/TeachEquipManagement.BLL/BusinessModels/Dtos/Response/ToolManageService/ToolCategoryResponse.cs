using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService
{
    public class ToolCategoryResponse
    {
        [JsonProperty("tool")]
        public ToolResponse? Tool { get; set; }

        [JsonProperty("category")]
        public CategoryResponse? Category { get; set; } 

    }
}
