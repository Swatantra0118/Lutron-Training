using OrderServiceAPI.Models;

namespace OrderServiceAPI.DTO
{
    public class CartItemDTO
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

    }
}
