using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.InventoryManage
{
    public class InventoryResponse
    {
        [JsonProperty("inventory_id")]
        public Guid Id { get; set; }

        [JsonProperty("total_quantity")]
        public int TotalQuantity { get; set; }

        [JsonProperty("amount_borrow")]
        public int AmountBorrow { get; set; }

        [JsonProperty("tool_id")]
        public int ToolId { set; get; }
    }
}
