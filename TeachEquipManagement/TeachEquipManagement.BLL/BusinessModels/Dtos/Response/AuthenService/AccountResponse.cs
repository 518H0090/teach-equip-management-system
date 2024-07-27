using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.AuthenService
{
    public class AccountResponse
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

        [JsonProperty("refresh-token")]
        public string? RefreshToken { get; set; } = string.Empty;

        [JsonProperty("token-expiry")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

        [JsonProperty("role_id")]
        public int RoleId { get; set; }
    }
}
