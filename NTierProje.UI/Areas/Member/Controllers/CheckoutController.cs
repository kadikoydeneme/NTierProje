using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Areas.Member.Models;
using NTierProje.UI.Areas.Member.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Member.Controllers
{
    public class CheckoutController : Controller
    {
        OrderService _orderService;
        ProductService _productService;
        AppUserService _appUserService;
        public CheckoutController()
        {
            _appUserService = new AppUserService();
            _productService = new ProductService();
            _orderService = new OrderService();
        }

      
        public ActionResult Add()
        {
            Orders o = new Orders();

            AppUser user = _appUserService.GetById(new Guid(HttpContext.User.Identity.Name));
            o.AppUserID = user.Id;
            o.AppUser = user;
            //o.AppUserID = new Guid("44fcbd8a-07d1-e711-b841-3cf862c31f76");
            _appUserService.Dispose();

            ProductCart cart = Session["sepet"] as ProductCart;
            Product p = new Product();
            foreach (var item in cart.CartProductList)
            {
                p = _productService.GetById(item.Id);
                p.UnitsInStock -= item.Quantity;
                _productService.Update(p);
                o.OrderDetails.Add(new OrderDetails
                {
                    Product = p,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            _productService.Dispose();
            _orderService.Add(o);
            _productService.Indispose();

            return Redirect("/Home/Index");
        }
    }
}