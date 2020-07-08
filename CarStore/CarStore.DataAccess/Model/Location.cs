﻿using System;
using System.Collections.Generic;

namespace CarStore.DataAccess.Model
{
    public partial class Location
    {
        public Location()
        {
            Orders = new HashSet<Orders>();
            Stock = new HashSet<Stock>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
