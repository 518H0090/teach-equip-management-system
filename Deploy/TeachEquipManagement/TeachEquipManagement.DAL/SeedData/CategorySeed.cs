using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeachEquipManagement.DAL.Models;

namespace TeachEquipManagement.DAL.SeedData
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = 1,
                    Type = "Category"
                },
                 new Category
                 {
                     Id = 2,
                     Type = "Storage Sacks"
                 },
                  new Category
                  {
                      Id = 3,
                      Type = "Households"
                  },
                   new Category
                   {
                       Id = 4,
                       Type = "Maintenance Sprays "
                   },
                    new Category
                    {
                        Id = 5,
                        Type = "Tapes"
                    },
                    new Category
                    {
                        Id = 6,
                        Type = "Marker Pens"
                    },
                    new Category
                    {
                        Id = 7,
                        Type = "Hand Cleaners"
                    },
                    new Category
                    {
                        Id = 8,
                        Type = "Electronics"
                    },
                    new Category
                    {
                        Id = 9,
                        Type = "School suppliers"
                    }
            );
        }
    }
}
