using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response.ToolManageService
{
    public class InvoiceResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("invoice_price")]
        public double Price { get; set; }

        [JsonProperty("invoice_date")]
        public DateTime InvoiceDate { get; set; }

        [JsonProperty("tool_id")]
        public int ToolId { get; set; }
    }
}
