namespace newsProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            news = new HashSet<news>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="*")]
        public string username { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        //[RegularExpression(@"^\w +@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage ="Invalid Email")]
        public string email { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "*")]
        public string password { get; set; }

        [NotMapped]
        [Display(Name = "confirm password")]
        [Compare("password" , ErrorMessage ="password not match")]
        public string confirm_password { get; set; }

        [Range(20,60,ErrorMessage ="age must be between 20 and 60")]
        public int? age { get; set; }

        public string address { get; set; }

        [StringLength(250)]
        public string photo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }
    }
}
