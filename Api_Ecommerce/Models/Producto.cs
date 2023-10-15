namespace Api_Ecommerce.Models
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string ImageUrl { get; set; }

        public double Precio { get; set; }

        public int Stock { get; set; }

        public Marca Marca { get; set; }

        public Categoria Categoria { get; set; }



    }
}
