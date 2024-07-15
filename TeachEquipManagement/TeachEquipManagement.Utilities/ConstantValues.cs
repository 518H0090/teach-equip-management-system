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

        public const string siteCollectionName = "FamilyTree";

        public const string documentLibraryName = "Avatars";

        public const string tenantName = $"trunghieu1204.sharepoint.com";

        public const string siteCollectionRelative = $"/sites/{siteCollectionName}";

        // Type for Process 

        public const string RequestBorrowType = "Borrow";

        public const string RequestReturnType = "Return";

        public const string RequestBuyType = "Buy";

        public const string RequestSellType = "Sell";

        // Status for Request

        public const string ApprovalPending = "Pending";

        public const string ApprovalAccept = "Accept";
    }
}
