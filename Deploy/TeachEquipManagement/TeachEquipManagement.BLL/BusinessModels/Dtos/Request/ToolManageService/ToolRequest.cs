using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Request.ToolManageService
{
    public class ToolRequest
    {
        [JsonProperty("tool_name")]
        public string ToolName { get; set; }

        [JsonProperty("tool_description")]
        public string Description { get; set; }

        [JsonProperty("tool_supplierid")]
        public int SupplierId { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("file_upload")]
        public IFormFile? FileUpload { get; set; }
    }
}
