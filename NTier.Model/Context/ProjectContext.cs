using NTier.Core.Entity;
using NTier.Model.Entities;
using NTier.Model.Maps;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace NTier.Model.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()/*:base("NTierProject")*/
        {
            Database.Connection.ConnectionString = "Server=.;Database=NTier;uid=sa;pwd=123;";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Konfigurasyonlara hazırladığımız map sınıflarını ekliyoruz.
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new SubCategoryMap());
            modelBuilder.Configurations.Add(new OrderDetailsMap());
            modelBuilder.Configurations.Add(new OrdersMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        //Savechanges içerisinde ekleme ve güncelleme bilgilerinin otomatik girişini sağlıyoruz.

        public override int SaveChanges()
        {

            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();

            string Identity = WindowsIdentity.GetCurrent().Name;
            string ComputerName = Environment.MachineName;
            DateTime dateTime = DateTime.Now;
            int User = 1;
            string ip = "1";
            foreach (var item in modifiedEntries)
            {
                CoreEntity entity = item.Entity as CoreEntity;
                if (item != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedADUserName = Identity;
                        entity.CreatedComputerName = ComputerName;
                        entity.CreatedDate = dateTime;
                        entity.CreatedBy = User;
                        entity.CreatedIp = ip;
                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.ModifiedADUserName = Identity;
                        entity.ModifiedBy = User;
                        entity.ModifiedComputerName = ComputerName;
                        entity.ModifiedDate = dateTime;
                        entity.ModifiedIp = ip;

                    }
                }
            }
            return base.SaveChanges();

            //try
            //{
            //    return base.SaveChanges();
            //}
            //catch (DbUpdateException e)
            //{

            //    throw HandleDbUpdateException(e);
            //}
        }


        //private Exception HandleDbUpdateException(DbUpdateException dbu)
        //{
        //    var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

        //    try
        //    {
        //        foreach (var result in dbu.Entries)
        //        {
        //            builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        builder.Append("Error parsing DbUpdateException: " + e.ToString());
        //    }

        //    string message = builder.ToString();
        //    return new Exception(message, dbu);
        //}

    }

}

