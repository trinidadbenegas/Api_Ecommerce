using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrderController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrdersByRoleAndId()
        { 
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string userRole = User.FindFirstValue(ClaimTypes.Role);

        var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return Ok(orders);
        }


        [HttpPost]

        public async Task<IActionResult> AddOrder(OrderDto order)
        {
            if (order == null)  BadRequest("It is empty");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _ordersService.StoreOrderAsync(order);

            return Ok("Order added successfully");



        }

    
    }
}
