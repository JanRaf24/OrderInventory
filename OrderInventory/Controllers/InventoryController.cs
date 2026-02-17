using Microsoft.AspNetCore.Mvc;
using OrderInventory.DTOs;
using OrderInventory.Services.Inventories;

namespace OrderInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public IActionResult SetStock([FromBody] InventoryRequest request)
        {
            _inventoryService.SetStock(request.Sku, request.Quantity);
            return Ok(new { Message = "Stock updated successfully." });
        }
    }
}
