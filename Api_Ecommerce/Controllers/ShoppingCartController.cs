﻿using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCart _shoppingCartService;

        private readonly IMapper _mapper;

        public ShoppingCartController( IShoppingCart shoppingCartService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingCartByRoleAndId()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _shoppingCartService.GetShoppingCartbyUserIdandRole(userId, userRole);
           
            return Ok(orders.Select(o => _mapper.Map<ShoppingCartResponseDto>(o)));
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
