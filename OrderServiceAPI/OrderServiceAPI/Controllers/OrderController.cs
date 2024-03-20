using Microsoft.AspNetCore.Mvc;
using OrderServiceAPI.DTO;
using OrderServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OrderServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<CartItemDTO> cartItems)
        {
            try
            {
                // Generate random order number
                string orderNumber = GenerateOrderNumber();

                // Save order details to a text file
                string filePath = Path.Combine(desktopPath, $"{orderNumber}.txt");
                await SaveOrderDetailsToFile(filePath, cartItems);

                return Ok(orderNumber);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GenerateOrderNumber()
        {
            // Generate a random order number
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

        private async Task SaveOrderDetailsToFile(string filePath, List<CartItemDTO> cartItems)
        {
            string cartItemsJson = Newtonsoft.Json.JsonConvert.SerializeObject(cartItems);

            await System.IO.File.WriteAllTextAsync(filePath, cartItemsJson, Encoding.UTF8);
        }
    }
}
