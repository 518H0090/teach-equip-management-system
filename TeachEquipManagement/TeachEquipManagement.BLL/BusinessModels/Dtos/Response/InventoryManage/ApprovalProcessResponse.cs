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
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("inventory_id")]
        public Guid InventoryId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("request_type")]
        public string RequestType { get; set; } = string.Empty;

        [JsonProperty("request_date")]
        public DateTime RequestDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        [JsonProperty("manager_approve")]
        public string? ManagerApprove { get; set; } = string.Empty;

        [JsonProperty("approve_date")]
        public DateTime? ApproveDate { get; set; }

        [JsonProperty("is_approved")]
        public bool IsApproved { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
