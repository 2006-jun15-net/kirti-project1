using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class StockViewModel
    {
        [Display(Name = "Location ID")]
        [Required]
        public int LocationId { get; set; }


        [Display(Name = "Product ID")]
        [Required]
        public int ProductId { get; set; }


        [Display(Name = "Quantity In Stock")]
        [Required]
        public int Quantity { get; set; }
    }
}