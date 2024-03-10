namespace ShoppingWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Collection")]
    public partial class Collection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Collection()
        {
            Products = new HashSet<Product>();
        }

        [Required(ErrorMessage="Collection ID must not be empty!")]
        [StringLength(5)]
        public string collectionID { get; set; }

        [Required(ErrorMessage = "Collection name must not be empty!")]
        [StringLength(50)]
        public string collectionName { get; set; }

        [Required(ErrorMessage = "Description must not be empty!")]
        [StringLength(100)]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
