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
            
            AppUser user = _appUserService.FindByUsername(HttpContext.User.Identity.Name);
            o.AppUserID = user.Id;
            o.AppUser = user;
            _appUserService.DetachEntity(user);

            ProductCart cart = Session["sepet"] as ProductCart;
            Product p = new Product();
            foreach (var item in cart.CartProductList)
            {
                p = _productService.GetById(item.Id);
                //Siparişteki ürün sayısını stoktan düşüyoruz.
                p.UnitsInStock -= item.Quantity;
                _productService.Update(p);
                o.OrderDetails.Add(new OrderDetails
                {
                    Product = p,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }




            _productService.DetachEntity(p);
            _orderService.Add(o);


            return Redirect("/Home/Index");
        }
    }
}