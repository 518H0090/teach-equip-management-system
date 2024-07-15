﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService
{
    public class ToolResponse
    {
        [JsonProperty("tool_id")]
        public int Id { get; set; }

        [JsonProperty("tool_name")]
        public string ToolName { get; set; } = string.Empty;

        [JsonProperty("tool_description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("tool_supplierid")]
        public int SupplierId { get; set; }
    }
}
