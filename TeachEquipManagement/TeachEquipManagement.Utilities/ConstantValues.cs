using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.Utilities
{
    public class ConstantValues
    {
        public const string ConnectionString = "DefaultConnection";

        // SPO
        public const string siteCollectionName = "ManageToolSite";
        public const string documentLibraryName = "StoreFile";

        public const string siteCollectionUrl = $"https://trunghieu1204.sharepoint.com/sites/{siteCollectionName}";
        public const string documentLibraryUrl = $"/sites/{siteCollectionName}/{documentLibraryName}";
       
        public const string fileUrl = "appsettings.Development.json";
        public const string addSiteMethod = "AddSiteCollectionRequestBody";
        public const string addDocMethod = "AddDocumentLibraryRequestBody";
    }
}
