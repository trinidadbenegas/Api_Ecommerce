using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ecommerce.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        public int ProductoId { get; set; }
        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        public double Subtotal { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }



    }
}
