using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService
{
    public class CategoryRequest
    {
        [JsonProperty("category_type")]
        public string Type { get; set; } = string.Empty;
    }
}
