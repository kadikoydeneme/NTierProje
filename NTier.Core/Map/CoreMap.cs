using NTier.Core.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace NTier.Core.Map
{
    public class CoreMap<T> : EntityTypeConfiguration<T> where T : CoreEntity
    {
        public CoreMap()
        {
            Property(x => x.Id).HasColumnName("Id")
                               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Status).HasColumnName("Status")
                                   .IsOptional();

            Property(x => x.CreatedDate).HasColumnName("CreatedDate")
                                        .IsOptional();

            Property(x => x.CreatedADUserName).HasColumnName("CreatedADUsername")
                                              .IsOptional();

            Property(x => x.CreatedBy).HasColumnName("CreatedBy")
                                      .IsOptional();

            Property(x => x.CreatedComputerName).HasColumnName("CreatedComputerName")
                                                .IsOptional();

            Property(x => x.CreatedIp).HasColumnName("CreatedIp")
                                      .IsOptional();

            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate")
                                         .IsOptional();

            Property(x => x.ModifiedADUserName).HasColumnName("ModifiedADUsername")
                                               .IsOptional();

            Property(x => x.ModifiedBy).HasColumnName("ModifiedBy")
                                       .IsOptional();

            Property(x => x.ModifiedComputerName).HasColumnName("ModifiedComputerName")
                                                 .IsOptional();

            Property(x => x.ModifiedIp).HasColumnName("ModifiedIp")
                                       .IsOptional();

        }
    }

}

