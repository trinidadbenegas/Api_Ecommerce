using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ecommerce.Models
{
    public class ShoppingCart
    {

        public int Id { get; set; }

        public string ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public AppUser Client { get; set; }

        public List<ShoppingItem> Items { get; set; }

        public double Total { get; set; }
    }
}
