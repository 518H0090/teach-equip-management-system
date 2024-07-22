using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.InventoryManage
{
    public class ProcessRequest
    {
        [JsonProperty("account_id")]
        public Guid AccountId { get; set; }

        [JsonProperty("inventory_id")]
        public Guid InventoryId { get; set; }
    }
}
