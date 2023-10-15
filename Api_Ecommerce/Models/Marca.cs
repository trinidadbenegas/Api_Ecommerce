namespace Api_Ecommerce.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string Name { get; set; }

       public List<Producto> Productos { get; set; }
    }
}
