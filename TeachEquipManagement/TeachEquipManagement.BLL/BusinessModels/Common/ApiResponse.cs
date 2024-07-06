using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Common
{
    public class ApiResponse<TEntity>
    {
        [JsonProperty("data_response")]
        public TEntity? Data { get; set; }

        [JsonProperty("message_response")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("status_response")]
        public int StatusCode { get; set; }
    }
}
