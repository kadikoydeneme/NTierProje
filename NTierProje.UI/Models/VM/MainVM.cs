using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTierProje.UI.Models.VM
{
    public class MainVM
    {
        public IEnumerable<Product>Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}