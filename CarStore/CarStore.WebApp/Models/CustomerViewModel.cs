using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer ID")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "First Name")]
        [StringLength(26, MinimumLength = 1)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(26, MinimumLength = 1)]
        [Required]
        public string LastName { get; set; }

        public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
