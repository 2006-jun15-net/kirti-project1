using System;
using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class ProductViewModel
    {
        [Display(Name = "Product ID")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Product")]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }
    }
}
