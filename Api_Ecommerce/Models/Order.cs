namespace Api_Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }

        public double Total { get; set; }
        public AppUser Client { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
