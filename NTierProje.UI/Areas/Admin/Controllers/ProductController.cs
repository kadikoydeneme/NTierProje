using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Areas.Admin.Models;
using NTierProje.UI.Attributes;
using NTierProje.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Role.Admin)]
    public class ProductController : Controller
    {
        ProductService _productService;
        SubCategoryService _subCategoryService;
        public ProductController()
        {
            _subCategoryService = new SubCategoryService();
            _productService = new ProductService();
        }

        public ActionResult List()
        {
            List<Product> model = _productService.GetActive();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<SubCategory> model = _subCategoryService.GetActive();
            return View(model);
        }

        [HttpPost]
        public RedirectResult Add(Product data, HttpPostedFileBase Image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", Image);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
                data.ImagePath = "~/Content/Images/TestPhoto.jpg";

            _productService.Add(data);
            return Redirect("/Admin/Product/List");
        }


        public ActionResult Update(Guid id)
        {
            ProductUpdateVM model = new ProductUpdateVM();
            model.Product = _productService.GetById(id);
            model.SubCategories = _subCategoryService.GetActive();
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Product data,HttpPostedFileBase Image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", Image);


            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                Product updated = _productService.GetById(data.Id);
                if (updated.ImagePath == null || updated.ImagePath == "~/Content/Images/TestPhoto.jpg")
                {
                    data.ImagePath = "~/Content/Images/TestPhoto.jpg";
                }
                else
                {
                    data.ImagePath = updated.ImagePath;
                }
            }

            _productService.Update(data);
            return Redirect("/Admin/Product/List");
        }

        public RedirectResult Delete(Guid id)
        {
            _productService.Remove(id);
            return Redirect("/Admin/Product/List");
        }
    }
}