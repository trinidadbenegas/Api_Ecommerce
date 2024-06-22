using Api_Ecommerce.Data;
using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Ecommerce.Services
{
    public class OrdersService : IOrdersService

    {
        private readonly AppDbContext _context;

        public OrdersService( AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders
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

        public async Task StoreOrderAsync(OrderDto order)
        {
            //    var newOrder = new Order()
            //    {
            //        ClientId= order.ClientId,
            //        Items= new List<OrderItem>()

            //    };

            //    double total = 0.0;

            //    foreach (var item in order.Items)
            //    {
            //        var producto = await _context.Productos.FindAsync(item.ProductoId);
            //        if(producto != null)
            //        {
            //            var orderItem = new OrderItem()
            //            {
            //                Cantidad = item.Cantidad,
            //                ProductoId = item.ProductoId,
            //                Producto= producto,
            //                OrderId = newOrder.Id,
            //                Subtotal = item.Cantidad * producto.Precio
            //            };

            //            total += orderItem.Subtotal;
            //            newOrder.Items.Add(orderItem);
            //        }              
            //    }

            //    newOrder.Total = total;

            //    await _context.Orders.AddAsync(newOrder);
            //    await _context.SaveChangesAsync();

            var newOrder = new Order
            {
                ClientId = order.ClientId,
                Items = new List<OrderItem>()
            };

            double total = 0.0;

            foreach (var itemDto in order.Items)
            {
                var producto = await _context.Productos.FindAsync(itemDto.ProductoId);

                if (producto != null)
                {
                    var shoppingItem = new OrderItem
                    {
                        Cantidad = itemDto.Cantidad,
                        ProductoId = itemDto.ProductoId,
                        Producto = producto,
                        OrderId = newOrder.Id,
                        Subtotal = itemDto.Cantidad * producto.Precio
                    };

                    total += shoppingItem.Subtotal;

                    newOrder.Items.Add(shoppingItem);
                }

            }

            newOrder.Total = total;

            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

        }
    }
}
