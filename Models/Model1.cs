using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ShoppingWebsite.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<admin> admins { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<admin>()
                .Property(e => e.full_name)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.full_name)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<ShoppingWebsite.Models.Collection> Collections { get; set; }
    }
}
