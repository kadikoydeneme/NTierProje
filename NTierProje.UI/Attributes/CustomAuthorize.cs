using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTierProje.UI.Attributes
{
    //Auth işlemlerinin Enum ile gerçekleşebilmesi için bu sınıfı kullanıyoruz.

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]//Allow Multiple ile birden fazla rol giriş olabiliyor.
    public class CustomAuthorize: AuthorizeAttribute
    {
        //String dizi rolleri tutmak için.
        private string[] UserProfilesRequired { get; set; }

        public CustomAuthorize(params object[] userProfilesRequired)
        {
            if (userProfilesRequired.Any(p => p.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("userProfilesRequired");

            this.UserProfilesRequired = userProfilesRequired.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
        }

        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;
            
            //Kullanıcının rolü yakalanıyor.
            AppUserService service = new AppUserService();

            AppUser user = service.FindByUsername(HttpContext.Current.User.Identity.Name);
            string userRole = Enum.GetName(typeof(Role), user.Role);
           
            //Kullanıcı belirtilen rollerden birine uyuyorsa devam edebilir.
            foreach (var role in this.UserProfilesRequired)
                if (userRole==role)
                {
                    authorized = true;
                    break;
                }

            //Eğer uymuyorsa Home/index sayfasına yönlendirilir.
            if (!authorized)
            {
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = url.Action("Index", "Home", new { Id = 302, Area = "" });
                context.Result = new RedirectResult(logonUrl);

                return;
            }
        }
    }
}