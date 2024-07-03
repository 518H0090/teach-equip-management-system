using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        public int TotalQuantity { get; set; }  

        public int AmountBorrow {  get; set; }
      
    }
}
