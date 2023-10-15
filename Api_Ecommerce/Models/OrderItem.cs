namespace Api_Ecommerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        public Producto Producto { get; set; }

        public double Subtotal { get; set; }


    }
}
