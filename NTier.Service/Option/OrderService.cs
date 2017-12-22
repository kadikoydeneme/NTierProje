using NTier.Model.Entities;
using NTier.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Service.Option
{
    public class OrderService : BaseService<Orders>
    {
        public List<Orders> ListPendingOrders()
        {
            return GetDefault(x => x.Confirmed==false && x.Status==Core.Entity.Enum.Status.Active).OrderByDescending(x=>x.CreatedDate).ToList();
        }
        public int PendingOrderCount()
        {
            return GetDefault(x => x.Confirmed == false && x.Status == Core.Entity.Enum.Status.Active).Count;
        }
    }
}
