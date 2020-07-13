using System.ComponentModel.DataAnnotations;

namespace CarStore.WebApp.Models
{
    public class OrderlineViewModel
    {

        [Display(Name = "Product ID")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "quantity")]
        [Required]
        public int Quantity { get; set; }

    }
}