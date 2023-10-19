using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Models;

namespace Api_Ecommerce.Interfaces
{
    public interface IOrdersService
    {
        Task StoreOrderAsync( OrderDto order);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);


    }
}
