namespace Api_Ecommerce.Models
{
    public class ShoppingCart
    {

        public int Id { get; set; }
        public AppUser Client { get; set; }

        public List<ShoppingItem> Items { get; set; }

        public double Total { get; set; }
    }
}
