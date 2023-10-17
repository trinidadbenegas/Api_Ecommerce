using Api_Ecommerce.Data;
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
            var orders = await _context.Orders.Include(n => n.Items).ThenInclude(n => n.Producto).Include(n => n.Client).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.Client.Id == userId).ToList();
            }

            return orders;
        }

        public Task StoreOrderAsync(List<OrderItem> items, string userId, string userEmailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
