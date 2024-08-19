using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService
{
    public class RoleUpdateRequest
    {
        [JsonProperty("role_id")]
        public int Id { get; set; }

        [JsonProperty("role_name")]
        public string RoleName { get; set; } = string.Empty;

        [JsonProperty("role_description")]
        public string RoleDescription { get; set; } = string.Empty;
    }
}
