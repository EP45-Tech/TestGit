﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Barn_Mart
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? ScheduleId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
