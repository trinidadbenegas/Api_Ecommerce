using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Ecommerce.Models
{
    public class ShoppingItem
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        public int ProductoId { get; set; }
        [ForeignKey(nameof(ProductoId))]
        public Producto Producto { get; set; }

        public double Subtotal { get; set; }

        public int ShoppingCartId { get; set; }
        [ForeignKey(nameof(ShoppingCartId))]

        public ShoppingCart ShoppingCart { get; set; }

        
    }
}
