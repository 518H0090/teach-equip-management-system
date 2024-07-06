using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.BLL.BusinessModels.Dtos
{
    public class UserDTOTest
    {
        public Guid Id { get; set; } = new Guid();

        public string Username { set; get; } = string.Empty;
    }
}
