using System;
using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(26)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(26)]
        [Required]
        public string LastName { get; set; }
    }
}
