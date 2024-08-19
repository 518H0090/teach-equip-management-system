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
        [JsonProperty("id")]
        public int Id { get; set; } 

        [JsonProperty("category_type")]
        public string Type { get; set; } = string.Empty;
    }
}
