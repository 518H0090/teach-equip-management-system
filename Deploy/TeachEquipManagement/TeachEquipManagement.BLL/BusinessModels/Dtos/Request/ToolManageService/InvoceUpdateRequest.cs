using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService
{
    public class InvoceUpdateRequest
    {
        [JsonProperty("invoice_id")]
        public int Id { get; set; }

        [JsonProperty("invoice_price")]
        public double Price { get; set; }
    }
}
