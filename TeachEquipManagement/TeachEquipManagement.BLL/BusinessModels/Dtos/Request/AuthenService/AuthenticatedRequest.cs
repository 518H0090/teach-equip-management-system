using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.AuthenService
{
    public class AuthenticatedRequest
    {
        [JsonProperty("user_name")]
        public string Username { set; get; } = string.Empty;


        [JsonProperty("password")]
        public string Password { set; get; } = string.Empty;
    }
}
