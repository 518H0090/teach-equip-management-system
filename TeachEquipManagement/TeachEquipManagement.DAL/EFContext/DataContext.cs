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

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Tool> Tools { set; get; }

        public DbSet<InventoryHistory> InventoryHistories { get; set; }


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

                user.Property(u => u.Email).IsRequired();

                user
                .HasOne(u => u.RefreshToken)
                .WithOne(rt => rt.User)
                .HasForeignKey<User>(u => u.RefreshTokenId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Permission>(permission =>
            {
                permission.HasKey(permission => permission.Id);

                permission.Property(permission => permission.Name).IsRequired();

                permission.Property(permission => permission.Description).IsRequired();
            });

            modelBuilder.Entity<UserPermission>(user_permission =>
            {
                user_permission.HasKey(up => new { up.UserId, up.PermissionId });

                user_permission.HasOne<User>(up => up.User)
                                .WithMany(u => u.UserPermissions)
                                .HasForeignKey(up => up.UserId)
                                .OnDelete(DeleteBehavior.Cascade);

                user_permission.HasOne<Permission>(up => up.Permission)
                                .WithMany(u => u.UserPermissions)
                                .HasForeignKey(up => up.PermissionId)
                                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserDetail>(user_detail =>
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
            });

            modelBuilder.Entity<Tool>(tool =>
            {
                tool.HasKey(tool => tool.Id);

                tool.Property(tool => tool.ToolName).IsRequired();

                tool.Property(tool => tool.Description).IsRequired();

                tool.Property(tool => tool.SubjectRelative).IsRequired();

                tool.HasOne<Supplier>(tool => tool.Supplier)
                    .WithMany(s => s.Tools)
                    .HasForeignKey(s => s.SupplierId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(category => category.Id);

                category.Property(category => category.Id).UseIdentityColumn(1, 1);

                category.Property(category => category.Type).IsRequired();

                category.Property(category => category.Unit).IsRequired();

                category.HasMany<Tool>(category => category.Tools)
                        .WithOne(tool => tool.Category)
                        .HasForeignKey(t => t.CategoryId)
                        .OnDelete(DeleteBehavior.Cascade);

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

                invoice.Property(invoice => invoice.Price).HasDefaultValue(0).IsRequired();

                invoice.Property(invoice => invoice.InvoiceDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                invoice.HasOne<Tool>(invoice => invoice.Tool)
                       .WithMany(t => t.Invoices)
                       .HasForeignKey(invoice => invoice.ToolId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ApprovalRequest>(approval_request =>
            {
                approval_request.HasKey(approval_request => new { approval_request.UserId, approval_request.InventoryId });

                approval_request.Property(approval_request => approval_request.Quantity).HasDefaultValue(0).IsRequired();

                approval_request.Property(approval_request => approval_request.RequestType).IsRequired();

                approval_request.Property(approval_request => approval_request.RequestType).IsRequired();

                approval_request.Property(approval_request => approval_request.RequestDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                approval_request.Property(approval_request => approval_request.Status).IsRequired();

                approval_request.Property(approval_request => approval_request.ManagerApprove).IsRequired();

                approval_request.Property(approval_request => approval_request.ApproveDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                approval_request.Property(approval_request => approval_request.IsApproved).HasDefaultValue<bool>(false).IsRequired();

                approval_request.HasOne<User>(approval_request => approval_request.User)
                      .WithMany(user => user.ApprovalRequests)
                      .HasForeignKey(approval_request => approval_request.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                approval_request.HasOne<Inventory>(approval_request => approval_request.Inventory)
                      .WithMany(inventory => inventory.ApprovalRequests)
                      .HasForeignKey(approval_request => approval_request.InventoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<RefreshToken>(refresh_token =>
            {
                refresh_token.HasKey(refresh_token => refresh_token.Id);

                refresh_token.Property(refresh_token => refresh_token.Token).IsRequired();

                refresh_token.Property(refresh_token => refresh_token.Created).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                refresh_token.Property(refresh_token => refresh_token.Expires).IsRequired();
            });

            modelBuilder.Entity<InventoryHistory>(inventory_history =>
            {
                inventory_history.HasKey(inventory_history => new { inventory_history.UserId, inventory_history.InventoryId });

                inventory_history.Property(inventory_history => inventory_history.Quantity).HasDefaultValue(0).IsRequired();

                inventory_history.Property(inventory_history => inventory_history.ActionDate).HasDefaultValue<DateTime>(DateTime.Now).IsRequired();

                inventory_history.Property(inventory_history => inventory_history.ActionType).IsRequired();

                inventory_history.HasOne<User>(approval_request => approval_request.User)
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
