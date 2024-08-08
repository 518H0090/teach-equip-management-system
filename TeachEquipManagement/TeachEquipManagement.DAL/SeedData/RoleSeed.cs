using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.SeedData
{
    public class RoleSeed : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData
            (
                new Role
                {
                    Id = 1,
                    RoleName = "admin",
                    RoleDescription = "This role can manage all system",
                },
                 new Role
                 {
                     Id = 2,
                     RoleName = "manager",
                     RoleDescription = "This role can manage inventory, approve request",
                 },
                new Role
                {
                    Id = 3,
                    RoleName = "user",
                    RoleDescription = "This role just can view inventory, create request",
                }
            );
        }
    }
}
