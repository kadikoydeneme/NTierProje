﻿using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Attributes;
using NTierProje.UI.Helpers;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Role.Admin)]
    public class AppUserController : Controller
    {

        AppUserService _appUserService;

        public AppUserController()
        {
            _appUserService = new AppUserService();
        }

        public ActionResult List(int page=1)
        {
            List<AppUser> model = _appUserService.GetActive();
            return View(model.ToPagedList(page,10));
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AppUser data, HttpPostedFileBase Image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", Image);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
                data.ImagePath = "~/Content/Images/TestPhoto.jpg";

            
            _appUserService.Add(data);
            return View();
        }


        public ActionResult Update(Guid id)
        {
            AppUser model = _appUserService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Update(AppUser data, HttpPostedFileBase Image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", Image);

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                //Elimizdeki imajı güncelleme aşamasında kaybetmemek için bir kontrol daha uyguluyoruz.
                AppUser updated = _appUserService.GetById(data.Id);
                if (updated.ImagePath == null || updated.ImagePath == "~/Content/Images/TestPhoto.jpg")
                {
                    data.ImagePath = "~/Content/Images/TestPhoto.jpg";
                }
                else
                {
                    data.ImagePath = updated.ImagePath;
                }
            }

            _appUserService.Update(data);
            return Redirect("/Admin/AppUser/List");
        }

        public RedirectResult Delete(Guid id)
        {
            _appUserService.Remove(id);
            return Redirect("/Admin/AppUser/List");
        }

    }
}