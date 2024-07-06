using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService
{
    public class SupplierRequest
    {
        [JsonProperty("supplier_name")]
        public string SupplierName { get; set; } = string.Empty;

        [JsonProperty("contact_name")]
        public string ContactName { get; set; } = string.Empty;

        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
