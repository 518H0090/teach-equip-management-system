using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService
{
    public class CategoryUpdateRequest
    {
        [JsonProperty("category_id")]
        public int Id { get; set; }

        [JsonProperty("category_type")]
        public string Type { get; set; } = string.Empty;

        [JsonProperty("category_unit")]
        public string Unit { get; set; } = string.Empty;
    }
}
