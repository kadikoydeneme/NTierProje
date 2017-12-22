using NTier.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Model.Entities
{
    public class ProductImage : CoreEntity
    {
        public string ImagePath { get; set; }
        public virtual Product Products { get; set; }
        public Guid ProductID { get; set; }

    
    }
}
