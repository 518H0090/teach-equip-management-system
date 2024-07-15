using Microsoft.EntityFrameworkCore;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.EFContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountDetail> UserDetails { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

        public DbSet<Tool> Tools { set; get; }

        public DbSet<InventoryHistory> InventoryHistories { get; set; }

        public DbSet<ToolCategory> ToolCategories { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(account =>
            {
                account.HasKey(user => user.Id);

                account.HasIndex(x => x.Id).IsUnique();

                account.Property(u => u.Username).HasColumnType("nvarchar(255)").IsRequired();

                account.Property(u => u.PasswordHash).IsRequired();

                account.Property(u => u.PasswordSalt).IsRequired();

                account.Property(u => u.Email).IsRequired();
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.HasKey(role => role.Id);

                role.Property(role => role.Id).UseIdentityColumn<int>(1,1);

                role.Property(role => role.RoleName).IsRequired();

                role.Property(role => role.RoleDescription).IsRequired();

                role.HasMany<Account>(role => role.Accounts)
                    .WithOne(account => account.Role)
                    .HasForeignKey(account => account.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AccountDetail>(user_detail =>
            {
                user_detail.HasKey(user_detail => user_detail.UserId);

                user_detail.Property(user_detail => user_detail.FullName).HasDefaultValue(string.Empty).HasMaxLength(255);

                user_detail.Property(user_detail => user_detail.Address).HasDefaultValue(string.Empty).HasMaxLength(255);

                user_detail.Property(user_detail => user_detail.Phone).HasDefaultValue(string.Empty).HasMaxLength(255);

                user_detail.Property(user_detail => user_detail.Avatar).HasDefaultValue(string.Empty).HasMaxLength(255);
            });

            modelBuilder.Entity<Supplier>(supplier =>
            {
                supplier.HasKey(supplier => supplier.Id);

                supplier.Property(supplier => supplier.Id).UseIdentityColumn<int>(1, 1);

                supplier.Property(supplier => supplier.SupplierName).IsRequired();

                supplier.Property(supplier => supplier.ContactName).IsRequired();

                supplier.Property(supplier => supplier.Address).IsRequired();

                supplier.Property(supplier => supplier.Phone).IsRequired();

                supplier.HasMany<Tool>(supplier => supplier.Tools)
                    .WithOne(t => t.Supplier)
                    .HasForeignKey(s => s.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Tool>(tool =>
            {
                tool.HasKey(tool => tool.Id);

                tool.Property(tool => tool.Id).UseIdentityColumn(1, 1);

                tool.Property(tool => tool.ToolName).IsRequired();

                tool.Property(tool => tool.Description).IsRequired();

                tool.HasOne<Supplier>(tool => tool.Supplier)
                    .WithMany(s => s.Tools)
                    .HasForeignKey(s => s.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);

                tool.HasMany<ToolCategory>(tool => tool.ToolCategories)
                        .WithOne(tool => tool.Tool)
                        .HasForeignKey(t => t.ToolId)
                        .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(category => category.Id);

                category.Property(category => category.Id).UseIdentityColumn(1, 1);

                category.Property(category => category.Type).IsRequired();

                category.Property(category => category.Unit).IsRequired();

                category.HasMany<ToolCategory>(category => category.ToolCategories)
                        .WithOne(category => category.Category)
                        .HasForeignKey(t => t.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<ToolCategory>(tool_category =>
            {
                tool_category.HasKey(tool_category => new {tool_category.ToolId, tool_category.CategoryId});
            });

            modelBuilder.Entity<Inventory>(inventory =>
            {
                inventory.HasKey(inventory => inventory.Id);

                inventory.Property(inventory => inventory.TotalQuantity).HasDefaultValue(0).IsRequired();

                inventory.Property(inventory => inventory.AmountBorrow).HasDefaultValue(0).IsRequired();

                inventory.HasOne<Tool>(inventory => inventory.Tool)
                         .WithOne(t => t.Inventory)
                         .HasForeignKey<Inventory>(inventory => inventory.ToolId)
                         .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Invoice>(invoice =>
            {
                invoice.HasKey(invoice => invoice.Id);

                invoice.Property(invoice => invoice.Id).UseIdentityColumn(1, 1);

                invoice.Property(invoice => invoice.Price).HasDefaultValue(0).IsRequired();

                invoice.Property(invoice => invoice.InvoiceDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                invoice.HasOne<Tool>(invoice => invoice.Tool)
                       .WithMany(t => t.Invoices)
                       .HasForeignKey(invoice => invoice.ToolId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApprovalRequest>(approval_request =>
            {
                approval_request.HasKey(approval_request => new { approval_request.AccountId, approval_request.InventoryId });

                approval_request.Property(approval_request => approval_request.Quantity).HasDefaultValue(0).IsRequired();

                approval_request.Property(approval_request => approval_request.RequestType).IsRequired();

                approval_request.Property(approval_request => approval_request.RequestType).IsRequired();

                approval_request.Property(approval_request => approval_request.RequestDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                approval_request.Property(approval_request => approval_request.Status).IsRequired();

                approval_request.Property(approval_request => approval_request.ManagerApprove).IsRequired();

                approval_request.Property(approval_request => approval_request.ApproveDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                approval_request.Property(approval_request => approval_request.IsApproved).HasDefaultValue<bool>(false).IsRequired();

                approval_request.HasOne<Account>(approval_request => approval_request.Account)
                      .WithMany(user => user.ApprovalRequests)
                      .HasForeignKey(approval_request => approval_request.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);

                approval_request.HasOne<Inventory>(approval_request => approval_request.Inventory)
                      .WithMany(inventory => inventory.ApprovalRequests)
                      .HasForeignKey(approval_request => approval_request.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InventoryHistory>(inventory_history =>
            {
                inventory_history.HasKey(inventory_history => new { inventory_history.UserId, inventory_history.InventoryId });

                inventory_history.Property(inventory_history => inventory_history.Quantity).HasDefaultValue(0).IsRequired();

                inventory_history.Property(inventory_history => inventory_history.ActionDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                inventory_history.Property(inventory_history => inventory_history.ActionType).IsRequired();

                inventory_history.HasOne<Account>(approval_request => approval_request.User)
                      .WithMany(user => user.InventoryHistories)
                      .HasForeignKey(approval_request => approval_request.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                inventory_history.HasOne<Inventory>(approval_request => approval_request.Inventory)
                      .WithMany(inventory => inventory.InventoryHistories)
                      .HasForeignKey(approval_request => approval_request.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
