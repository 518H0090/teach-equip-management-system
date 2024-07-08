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
    public class UserUpdateRequest
    {
        [JsonProperty("user_id")]
        public Guid Id { get; set; }

        [JsonProperty("user_name")]
        public string Username { set; get; } = string.Empty;

        [JsonProperty("password_hash")]
        public byte[] PasswordHash { set; get; } = new byte[32];

        [JsonProperty("password_salt")]
        public byte[] PasswordSalt { set; get; } = new byte[32];

        [JsonProperty("email")]
        public string Email { set; get; } = string.Empty;
    }
}
