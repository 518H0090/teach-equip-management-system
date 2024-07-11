using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.Utilities.OptionPattern
{
    public class AzureAdConfiguration
    {
        public string ClientId { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;
        public string GraphResource { get; set; } = string.Empty;
        public string GraphResourceEndPoint { get; set; } = string.Empty;
    }
}
