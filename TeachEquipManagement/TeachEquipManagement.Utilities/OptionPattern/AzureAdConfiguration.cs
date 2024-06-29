using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.Utilities.OptionPattern
{
    public class AzureAdConfiguration
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Instance { get; set; }
        public string GraphResource { get; set; }
        public string GraphResourceEndPoint { get; set; }
    }
}
