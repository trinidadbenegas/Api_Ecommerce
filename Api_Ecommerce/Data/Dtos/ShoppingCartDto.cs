using Api_Ecommerce.Models;

namespace Api_Ecommerce.Data.Dtos
{
    public class ShoppingCartDto
    {
        public string ClientId { get; set; }
        public List<ShoppingItemDto> Items { get; set; }
    }
}
