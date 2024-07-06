using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachEquipManagement.BLL.IServices;

namespace TeachEquipManagement.BLL.ManageServices
{
    public interface IToolManageService
    {
        IToolService ToolService { get; }

        ISupplierService SupplierService { get; }

        IInvoiceService InvoiceService { get; }

        ICategoryService CategoryService { get; }
    }
}
