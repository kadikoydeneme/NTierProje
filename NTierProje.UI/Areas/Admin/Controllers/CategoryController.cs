using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService _categoryService;
        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        public ActionResult List()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Category data)
        {
            _categoryService.Add(data);

            return View();
        }
        public ActionResult Update(Guid id)
        {
            Category model = _categoryService.GetById(id);
            return View(model);

        }
        [HttpPost]
        public ActionResult Update(Category data)
        {
            _categoryService.Update(data);
            return Redirect("/Admin/Category/List");
        }
        public RedirectResult Delete(Guid id)
        {
            _categoryService.Remove(id);
            return Redirect("/Admin/Category/List");
        }
      
    }
}