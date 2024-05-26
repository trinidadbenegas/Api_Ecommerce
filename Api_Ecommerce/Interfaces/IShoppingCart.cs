using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Models;

namespace Api_Ecommerce.Interfaces
{
    public interface IShoppingCart
    {
        Task  StoreShoppingCartAsync(ShoppingCartDto shoppingCart);

        Task <List<ShoppingCart> > GetShoppingCartbyUserIdandRole(string userId, string userRole);
    }
}
