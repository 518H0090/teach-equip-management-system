using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachEquipManagement.DAL.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public double Price { get; set; }
        
        public DateTime InvoiceDate { get; set; }   
    }
}
