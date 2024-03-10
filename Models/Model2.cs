using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ShoppingWebsite.Models
{
    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Collection>()
                .Property(e => e.collectionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Collection>()
                .Property(e => e.collectionName)
                .IsUnicode(false);

            modelBuilder.Entity<Collection>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Collection>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Collection)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .Property(e => e.productID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.productName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.collectionID)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
