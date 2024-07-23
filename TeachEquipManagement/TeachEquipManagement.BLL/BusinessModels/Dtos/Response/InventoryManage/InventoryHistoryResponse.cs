﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage
{
    public class InventoryHistoryResponse
    {
        [JsonProperty("user_id")]
        public Guid UserId { get; set; }

        [JsonProperty("inventory_id")]
        public Guid InventoryId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("action_date")]
        public DateTime ActionDate { get; set; }

        [JsonProperty("action_type")]
        public string ActionType { get; set; } = string.Empty;
    }
}
