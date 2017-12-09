using NTier.Core.Entity;
using System;
using System.Collections.Generic;

namespace NTier.Model.Entities
{
    public class Product:CoreEntity
    {
        public decimal? Price { get; set; }
        public short? UnitsInStock { get; set; }
        public string Quantity { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public Guid SubCategoryID { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}