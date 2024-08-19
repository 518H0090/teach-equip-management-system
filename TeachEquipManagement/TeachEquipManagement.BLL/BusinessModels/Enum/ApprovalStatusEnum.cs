using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Enum
{
    public enum ApprovalStatusEnum
    {
        [Description("Pending")]
        ApprovalPending,

        [Description("Accept")]
        ApprovalAccept
    }
}
