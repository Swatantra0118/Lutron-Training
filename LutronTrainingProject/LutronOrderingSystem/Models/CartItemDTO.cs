using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LutronOrderingSystem.Models
{
    public class CartItemDTO
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
