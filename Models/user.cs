namespace ShoppingWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userid { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(20)]
        public string phone_number { get; set; }

        [StringLength(100)]
        public string full_name { get; set; }
    }
}
