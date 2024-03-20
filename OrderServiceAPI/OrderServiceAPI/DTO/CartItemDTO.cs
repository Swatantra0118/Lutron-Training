using OrderServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServiceAPI.DTO
{
    public class CartItemDTO
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

    }
}
