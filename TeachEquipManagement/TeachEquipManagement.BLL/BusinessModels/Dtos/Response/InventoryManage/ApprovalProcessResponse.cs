using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage
{
    public class ApprovalProcessResponse
    {
        [JsonProperty("")]
        public Guid AccountId { get; set; }

        [JsonProperty("")]
        public Guid InventoryId { get; set; }

        [JsonProperty("")]
        public int Quantity { get; set; }

        [JsonProperty("")]
        public string RequestType { get; set; } = string.Empty;

        [JsonProperty("")]
        public DateTime RequestDate { get; set; }

        [JsonProperty("")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("")]
        public string? ManagerApprove { get; set; } = string.Empty;

        [JsonProperty("")]
        public DateTime? ApproveDate { get; set; }

        [JsonProperty("")]
        public bool IsApproved { get; set; }
    }
}
