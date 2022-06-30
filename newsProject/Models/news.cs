namespace newsProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        public int id { get; set; }

        [StringLength(200)]
        [Display(Name = "Title")]
        [Required(ErrorMessage = "*")]
        public string title { get; set; }

        [StringLength(300)]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Bref")]
        public string bref { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Description")]
        public string desc { get; set; }

        public DateTime? date { get; set; }
        
        [StringLength(150)]
        [Display(Name = "Photo")]
        public string photo { get; set; }

        [Display(Name ="Cataloge")]
        public int? cat_id { get; set; }

        public int? user_id { get; set; }

        public virtual catalog catalog { get; set; }

        public virtual user user { get; set; }
    }
}
