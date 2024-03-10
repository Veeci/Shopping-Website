namespace ShoppingWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Required(ErrorMessage ="Product ID must not be empty!")]
        [DisplayName("Product ID")]
        [StringLength(5)]
        public string productID { get; set; }

        [Required(ErrorMessage = "Product name must not be empty!")]
        [DisplayName("Product name")]
        [StringLength(50)]
        public string productName { get; set; }

        [Required(ErrorMessage = "Description must not be empty!")]
        [DisplayName("Description")]
        [StringLength(100)]
        public string description { get; set; }

        [Required(ErrorMessage = "Price must not be empty!")]
        [DisplayName("Price")]
        public decimal? price { get; set; }

        [Required(ErrorMessage = "Quantity must not be empty!")]
        [DisplayName("Quantity")]
        public int? quantity { get; set; }

        [DisplayName("Image")]
        [StringLength(50)]
        public string image { get; set; }

        [Required(ErrorMessage = "Stock date must not be empty!")]
        [DisplayName("Stock date")]
        [Column(TypeName = "date")]
        public DateTime? stockDate { get; set; }

        [Required(ErrorMessage = "Gender type must not be empty!")]
        [DisplayName("Gender")]
        [StringLength(50)]
        public string gender { get; set; }

        [StringLength(5)]
        public string collectionID { get; set; }

        public virtual Collection Collection { get; set; }
    }
}
