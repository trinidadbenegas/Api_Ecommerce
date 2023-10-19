using Api_Ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ecommerce.Data.Dtos
{
    public class OrderDto
    {
        

        public string ClientId { get; set; }
        

        

        public List<OrderItemDto> Items { get; set; }
    }
}
