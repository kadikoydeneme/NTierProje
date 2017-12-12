using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Security;

namespace NTierProje.UI.Controllers
{
    public class HomeController : Controller
    {
        ProductService _productService;
        AppUserService _appUserService;
        CategoryService _categoryService;
        public HomeController()
        {
            _appUserService = new AppUserService();
            _productService = new ProductService();
            _categoryService = new CategoryService();
        }
        
        public ActionResult Index(Guid? id)
        {
            //ID API üzerinden gönderiliyor. Eğer boş ise authentication yapmıyoruz.
            if (id!=null)
            {
                AppUser user = new AppUser();
                user = _appUserService.GetById((Guid)id);
                string cookie = user.UserName.ToString();
                FormsAuthentication.SetAuthCookie(cookie, true);
                
                if (user.Role==Role.Admin) return Redirect("/Admin/Home/Index");
                
            }


            var model = _productService.GetDefault(x => x.UnitsInStock > 0).OrderByDescending(x => x.CreatedDate).Take(4);
             
            return View(model);


        }
        //Bu metot PartialView'i yönlendirmek için kullanılıyor. ChildActionOnly bu action'ın sadece bu durumlarda çağırılabileceğini belirtir.Opsiyoneldir... 
        [ChildActionOnly]
        public ActionResult CategoryList()
        {
            return PartialView("_CategoryList",_categoryService.GetActive());
        }


        public ActionResult Login()
        {
            return View();
        }

        //Bu metot API üzerinden yönlendirilerek ulaşılmaktadır.
        public RedirectResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
        
    }
}