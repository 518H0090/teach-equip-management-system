using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService
{
    public class AccountUpdateRequest
    {
        [JsonProperty("user_id")]
        public Guid Id { get; set; } = Guid.Empty;

        [JsonProperty("password")]
        public string? Password { set; get; } = null;

        [JsonProperty("email")]
        public string? Email { set; get; } = null;

        [JsonProperty("role_id")]
        public int RoleId { set; get; }
    }
}
