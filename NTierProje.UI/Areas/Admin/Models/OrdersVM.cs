using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTierProje.UI.Areas.Admin.Models
{
    public class OrdersVM
    {
        public OrdersVM()
        {
            Orders = new List<NTier.Model.Entities.Orders>();
            OrderDetails = new List<NTier.Model.Entities.OrderDetails>();
        }
        public List<Orders> Orders { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}