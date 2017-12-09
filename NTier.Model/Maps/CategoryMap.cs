using NTier.Core.Map;
using NTier.Model.Entities;

namespace NTier.Model.Maps
{
    public class CategoryMap:CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(x => x.Name).IsOptional();
            Property(x => x.Description).IsOptional();


            HasMany(x => x.SubCategories)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryID);
        }

    }
}
