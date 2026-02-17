using Microsoft.AspNetCore.Mvc;
using OrderInventory.DTOs;
using OrderInventory.Services.Orders;

namespace OrderInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder([FromBody] OrderRequest request)
        {
            try
            {
                _orderService.placeOrder(request.Sku, request.Quantity);
                return Ok(new { Message = "Order placed successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
