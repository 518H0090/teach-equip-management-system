using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos.Response
{
    public class PaginationResponse<TEntity>
    {
        public int TotalCount { set; get; }

        public int TotalPages { set; get; }

        public int CurrentPage {  set; get; }

        public int PageSize {  set; get; }

        public List<TEntity>? Data {  set; get; }
    }
}
