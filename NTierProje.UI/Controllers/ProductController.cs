using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Controllers
{
    public class ProductController : Controller
    {
        ProductService _productService;
        SubCategoryService _subcategoryService;
        public ProductController()
        {

            _productService = new ProductService();
            _subcategoryService = new SubCategoryService();
        }
        public ActionResult Detail(Guid? id)
        {
            if (id != null)
            {
                Product model = _productService.GetById((Guid)id);
                return View(model);
            }

            return Redirect("~/Home/index");
        }

        
        public ActionResult List(Guid? id)
        {
            if (id!=null)
            {
                IEnumerable<Product> productListbyCategory = _productService.GetDefault(x => x.SubCategoryID == id);
                return View(productListbyCategory); 
            }
            return Redirect("~/Home/Index");
        }
    }
}