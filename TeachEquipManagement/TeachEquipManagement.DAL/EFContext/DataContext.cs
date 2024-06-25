using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.EFContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(user => user.Id);

                user.HasIndex(x => x.Id).IsUnique();

                user.Property(u => u.Username).HasColumnType("nvarchar(255)").IsRequired();

                user.Property(u => u.PasswordHash).IsRequired();

                user.Property(u => u.PasswordSalt).IsRequired();
            });
        }
    }
}
