using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using AutoMapper;
using Api_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        private readonly IMapper _mapper;
        public OrderController(IOrdersService ordersService, IMapper mapper)
        {
            _ordersService = ordersService;
            _mapper = mapper;
        }

        
        [HttpGet]
        
        public async Task<IActionResult> GetOrdersByRoleAndId()
        { 
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        string userRole = User.FindFirstValue(ClaimTypes.Role);

        var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

        return Ok(orders.Select(o => _mapper.Map<OrderResponseDto>(o)));
        }


        [HttpPost]

        public async Task<IActionResult> AddOrder([FromBody] OrderDto order)
        {
            if (order == null)  BadRequest("It is empty");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _ordersService.StoreOrderAsync(order);

            return Ok("Order added successfully");

        }

    
    }
}
