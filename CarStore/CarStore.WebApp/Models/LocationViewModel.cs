using System;
using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class LocationViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int LocationId { get; set; }

        [Display(Name = "Location")]
        [MaxLength(255)]
        [Required]
        public string LocationName { get; set; }
    }
}
