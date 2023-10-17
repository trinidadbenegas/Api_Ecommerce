using Api_Ecommerce.Models;

namespace Api_Ecommerce.Interfaces
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<OrderItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
