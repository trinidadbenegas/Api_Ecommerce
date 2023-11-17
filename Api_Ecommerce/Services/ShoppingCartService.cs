using Api_Ecommerce.Data;
using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;

namespace Api_Ecommerce.Services
{
    public class ShoppingCartService : IShoppingCart
    {
        private readonly AppDbContext _context;

        public ShoppingCartService( AppDbContext context)
        {
            _context= context;
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
                // Puedes manejar el caso donde el producto no se encuentra en la base de datos.
            }

            shoppingCart.Total = total;

            await _context.ShoppingCarts.AddAsync(shoppingCart);
            await _context.SaveChangesAsync();

            
        }
    }
}
