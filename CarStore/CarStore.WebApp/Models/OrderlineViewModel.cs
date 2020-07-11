using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class OrderlineViewModel
    {
        [Display(Name = "Order ID")]
        [Required]
        public int OrderId { get; set; }

        [Display(Name = "Product ID")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "product quantity")]
        [Required]
        public int Quantity { get; set; }

    }
}