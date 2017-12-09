using NTier.Core.Map;
using NTier.Model.Entities;

namespace NTier.Model.Maps
{
    public class ProductMap:CoreMap<Product>
    {
        public ProductMap()
        {
            ToTable("dbo.Products");
            Property(x => x.Name).HasMaxLength(50).IsOptional();
            Property(x => x.Price).IsOptional();
            Property(x => x.Quantity).IsOptional();
            Property(x => x.UnitsInStock).IsOptional();
            Property(x => x.ImagePath).IsOptional();

            HasRequired(x => x.SubCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SubCategoryID)
                .WillCascadeOnDelete(false);
        }
    }
}