using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;
using TeachEquipManagement.Utilities.OptionPattern;

namespace TeachEquipManagement.BLL.Services
{
    public class GraphService : IGraphService
    {
        private readonly AzureAdConfiguration _azureConfiguration;

        public GraphService(IOptionsSnapshot<AzureAdConfiguration> azureConfiguration)
        {
            _azureConfiguration = azureConfiguration.Value;
        }

    }
}
