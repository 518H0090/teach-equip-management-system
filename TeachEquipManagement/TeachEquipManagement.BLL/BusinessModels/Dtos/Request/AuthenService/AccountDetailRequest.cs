﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService
{
    public class AccountDetailRequest
    {
        [JsonProperty("user_id")]
        public Guid UserId { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonProperty("address")]
        public string Address { get; set; } = string.Empty;

        [JsonProperty("phone")]
        public string Phone { get; set; } = string.Empty;
    }
}
