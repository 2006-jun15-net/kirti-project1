﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CarStore.Library.Model;

namespace CarStore.WebApp.Models
{
    public class OrderViewModel
    {
        [Display(Name = "Order ID")]
        [Required]
        public int OrderId { get; set; }

        [Display(Name = "Location ID")]
        [Required]
        public int LocationId { get; set; }

        [Display(Name = "Cusotmer ID")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Order Date")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        public Dictionary<Product, int> OrderLine { get; set; }
    }
}