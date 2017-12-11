using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    [AllowAuthorized(Roles = Role.Admin)]
    public class HomeController : Controller
    {

        OrderService _orderService;
        public HomeController()
        {
            _orderService = new OrderService();
        }

        public ActionResult Index()
        {
            //Onaylanmamış tüm siparişler admin'e gönderiliyor.
            List<Orders> model = _orderService.GetDefault(x => x.Confirmed == false && x.Status==NTier.Core.Entity.Enum.Status.Active);

            //Sipariş sayısı view içerisinde görüntülenecek.
            ViewBag.Siparis = model.Count;

            return View();


        }
    }
}