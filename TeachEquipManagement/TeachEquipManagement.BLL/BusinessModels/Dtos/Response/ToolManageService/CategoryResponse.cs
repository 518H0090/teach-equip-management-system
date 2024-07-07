using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService
{
    public class CategoryResponse
    {
        [JsonProperty("category_type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("category_unit")]
        public string Unit { get; set; } = string.Empty;
    }
}
