using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService
{
    public class SupplierIncludeToolResponse
    {
        [JsonProperty("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;

        [JsonProperty("contact_name")]
        public string ContactName { get; set; } = string.Empty;

        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonProperty("list_tools")]
        public List<ToolResponse>? Tools { get; set; }
    }
}
