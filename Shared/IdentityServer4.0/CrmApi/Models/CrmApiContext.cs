using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrmApi.Models
{
    public class CrmApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CrmApiContext() : base("name=CrmApiContext")
        {
        }

        public System.Data.Entity.DbSet<CrmApi.Models.Brands> Brands { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CrmApiContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
