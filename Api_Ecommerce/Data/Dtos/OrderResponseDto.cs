using Api_Ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ecommerce.Data.Dtos
{
    public class OrderResponseDto
    {

        public int Id { get; set; }

        public string ClientId { get; set; }
        
        public List<OrderItemDto> Items { get; set; }

        public double Total { get; set; }
    }
}
