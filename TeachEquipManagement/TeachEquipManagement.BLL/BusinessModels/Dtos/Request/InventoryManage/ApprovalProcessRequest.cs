﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage
{
    public class ApprovalProcessRequest
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("inventory_id")]
        public Guid InventoryId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("request_type")]
        public string RequestType { get; set; } = string.Empty;

        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;
    }
}
