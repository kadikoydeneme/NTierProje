using NTier.AuthService.Models;
using NTier.Model.Entities;
using NTier.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Security;

namespace NTier.AuthService.Controllers
{
    public class AuthController : ApiController
    {
        AppUserService _userService;

        public AuthController()
        {
            _userService = new AppUserService();
        }

        [HttpPost]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Login(Credentials model)
        {
            var url = "";
            if(model.username==null || model.password == null)
            {
                url = "http://localhost:60267/Home/login";
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Success = true, RedirectUrl = url });
            }
            if (_userService.CheckCredentials(model.username, model.password))
            {
                AppUser p = new AppUser();
                p = _userService.FindByUsername(model.username);

                if (p.Role == Role.Admin||p.Role==Role.Member)
                {
                    url = "http://localhost:60267/Home/Index/"+p.Id;
                    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = url });
                }
                //else if (p.Role == Role.Member)
                //{
                //    var newUrl = "http://localhost:60267/Home/Index/"+p.Id;
                //    return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                //}
                else
                {
                    url = "http://localhost:60267/Home/Index";
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Success = true, RedirectUrl = url });
                }
            }
            url = "http://localhost:60267/Home/login";
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { Success = true, RedirectUrl = url });
        }

        [HttpGet]
        public HttpResponseMessage Logout()
        {
            var newUrl = "http://localhost:60267/Home/logout";
            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
        }
    }
}