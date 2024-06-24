using Microsoft.EntityFrameworkCore;

namespace TeachEquipManagement.DAL.EFContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
