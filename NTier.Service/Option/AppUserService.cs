using NTier.Model.Entities;
using NTier.Service.Base;

namespace NTier.Service.Option
{
    public class AppUserService:BaseService<AppUser>
    {
        public bool CheckCredentials(string userName, string password)
        {
            return Any(x => x.UserName == userName && x.Password==password);
        }
        public AppUser FindByUsername(string userName)
        {
            return GetByDefault(x => x.UserName == userName);
        }
    }
}