using NTier.Model.Entities;
using NTier.Service.Option;
using NTierProje.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Areas.Member.Controllers
{
    public class RegisterController : Controller
    {
        AppUserService _appUserService;
        public RegisterController()
        {
            _appUserService = new AppUserService();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AppUser data,HttpPostedFileBase Image)
        {
            data.ImagePath = ImageUploader.UploadSingleImage("~/Uploads/", Image);   

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
                data.ImagePath = "~/Content/Images/TestPhoto.jpg";
            
            data.Role = Role.Member;
            
            _appUserService.Add(data);
            return View();
        }
    }
}