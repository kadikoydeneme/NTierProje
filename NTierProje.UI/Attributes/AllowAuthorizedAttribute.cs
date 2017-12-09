using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NTierProje.UI.Attributes
{
    public class AllowAuthorizedAttribute : AuthorizeAttribute
    {

        public new Role Roles;

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            
            if (HttpContext.Current.User == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            IPrincipal user = HttpContext.Current.User;

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            AppUserService _service = new AppUserService();
            AppUser currentUser = new AppUser();
            currentUser = _service.FindByUsername(HttpContext.Current.User.Identity.Name);

            Role role = (Role)currentUser.Role;

            if ((Roles & role) != role)
            {
                return false;
            }
            return true;
        }
    }
}