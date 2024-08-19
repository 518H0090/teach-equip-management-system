using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService
{
    public class AccountRequest
    {
        [JsonProperty("user_name")]
        public string Username { set; get; } = string.Empty;

        [JsonProperty("password")]
        public string Password { set; get; } = string.Empty;

        [JsonProperty("email")]
        public string Email { set; get; } = string.Empty;

        [JsonProperty("role_id")]
        public int RoleId { set; get; } 
    }
}
