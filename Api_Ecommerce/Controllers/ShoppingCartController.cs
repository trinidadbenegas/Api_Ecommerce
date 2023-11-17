using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart _shoppingCartService;
        private readonly IProductoService _productoService;

        public ShoppingCartController( IShoppingCart shoppingCartService, IProductoService productoService)
        {
            _shoppingCartService = shoppingCartService;
            _productoService = productoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingCart([FromBody] ShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null)
                return BadRequest("It is empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _shoppingCartService.StoreShoppingCartAsync(shoppingCartDto);

            return Ok("Cart successfully stored");
        }


    }
}
