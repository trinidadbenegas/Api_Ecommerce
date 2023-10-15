namespace Api_Ecommerce.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
