using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Models.VM;
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
            if (id!=null)
            {
                AppUser user = new AppUser();
                user = _appUserService.GetById((Guid)id);
                string cookie = user.Id.ToString();
                FormsAuthentication.SetAuthCookie(cookie, true);

                if (user.Role==Role.Admin)
                    return Redirect("/Admin/Home/Index");
                
            }
            MainVM model = new MainVM();
            
            IEnumerable<Product> productList = _productService.GetDefault(x => x.UnitsInStock > 0).OrderByDescending(x => x.CreatedDate).Take(8);
            model.Products = productList;
            IEnumerable<Category> catList = _categoryService.GetActive();
            model.Categories = catList;
            return View(model);


        }

        [ChildActionOnly]
        public ActionResult CategoryList()
        {
            return PartialView("_CategoryList",_categoryService.GetActive());
        }


        public ActionResult Login()
        {
            return View();
        }

        public RedirectResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
        
    }
}