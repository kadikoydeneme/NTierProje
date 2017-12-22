using NTier.Core.Map;
using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Maps
{
    public class ProductImageMap : CoreMap<ProductImage>
    {
        public ProductImageMap()
        {
            ToTable("dbo.Images");
            Property(x => x.ImagePath).IsRequired();

            HasRequired(x => x.Products)
              .WithMany(x => x.Images)
              .HasForeignKey(x => x.ProductID)
              .WillCascadeOnDelete(false);

            HasRequired(x => x.Products)
              .WithMany(x => x.Images)
              .HasForeignKey(x => x.ProductID)
              .WillCascadeOnDelete(false);
        }
       
    }
}
