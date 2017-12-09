using NTier.Core.Map;
using NTier.Model.Entities;

namespace NTier.Model.Maps
{
    public class AppUserMap:CoreMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("dbo.Users");
            Property(x => x.Address).IsOptional();
            Property(x => x.Birthdate).HasColumnType("datetime2").IsOptional();
            Property(x => x.Email).HasMaxLength(50).IsOptional();
            Property(x => x.ImagePath).IsOptional();
            Property(x => x.UserName).HasMaxLength(50).IsRequired();
            Property(x => x.Password).HasMaxLength(50).IsRequired();
            Property(x => x.PhoneNumber).IsOptional();
            Property(x => x.Role).IsOptional();
            Property(x => x.Name).HasMaxLength(50).IsOptional();
            Property(x => x.Surname).HasMaxLength(50).IsOptional();
        }
    }
}
