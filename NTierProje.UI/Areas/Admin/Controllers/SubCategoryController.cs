using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Areas.Admin.Models;
using NTierProje.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    [AllowAuthorized(Roles = Role.Admin)]
    public class SubCategoryController : Controller
    {
        SubCategoryService _subCategoryService;
        CategoryService _categoryService;
        public SubCategoryController()
        {
            _categoryService = new CategoryService();
            _subCategoryService = new SubCategoryService();
        }
            
        
        public ActionResult List()
        {
            List<SubCategory> model = _subCategoryService.GetActive();
            return View(model);
        }

        public ActionResult Add()
        {
            List<Category> model = _categoryService.GetActive();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(SubCategory data)
        {
            data.Id = Guid.NewGuid();
            _subCategoryService.Add(data);

            return Redirect("/Admin/SubCategory/List");
        }
        public ActionResult Update(Guid id)
        {
            SubCategoryVM model = new SubCategoryVM();
            model.Subcategory = _subCategoryService.GetById(id);
            model.Categories = _categoryService.GetActive();

            return View(model);

        }
        [HttpPost]
        public ActionResult Update(SubCategory data)
        {
            _subCategoryService.Update(data);
            return Redirect("/Admin/SubCategory/List");
        }
        public RedirectResult Delete(Guid id)
        {
            _subCategoryService.Remove(id);
            return Redirect("/Admin/SubCategory/List");
        }

    }
}