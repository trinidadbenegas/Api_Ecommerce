using Api_Ecommerce.Data;
using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Ecommerce.Services
{
    public class ShoppingCartService : IShoppingCart
    {
        private readonly AppDbContext _context;

        public ShoppingCartService( AppDbContext context)
        {
            _context= context;
        }

        public async Task<List<ShoppingCart>> GetShoppingCartbyUserIdandRole(string userId, string userRole)
        {
            var orders = await _context.ShoppingCarts
                .Include(n => n.Items)
                .ThenInclude(n => n.Producto)
                .Include(n => n.Client)
                .ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.Client.Id == userId).ToList();
            }

            return orders;
        }

        public async Task StoreShoppingCartAsync(ShoppingCartDto shoppingCartDto)
        {
            var shoppingCart = new ShoppingCart
            {
                ClientId = shoppingCartDto.ClientId,
                Items = new List<ShoppingItem>()
            };

            double total = 0.0;

            foreach (var itemDto in shoppingCartDto.Items)
            {
                var producto = await _context.Productos.FindAsync(itemDto.ProductoId);

                if (producto != null)
                {
                    var shoppingItem = new ShoppingItem
                    {
                        Cantidad = itemDto.Cantidad,
                        ProductoId = itemDto.ProductoId,
                        Producto = producto,
                        ShoppingCartId = shoppingCart.Id,
                        Subtotal = itemDto.Cantidad * producto.Precio
                    };

                    total += shoppingItem.Subtotal;

                    shoppingCart.Items.Add(shoppingItem);
                }
                
            }

            shoppingCart.Total = total;

            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();

            
        }
    }
}
