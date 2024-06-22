namespace Api_Ecommerce.Data.Dtos
{
    public class ShoppingCartResponseDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public List<ShoppingItemDto> Items { get; set; }
        public double Total { get; set; }
    }
}
