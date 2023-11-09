using Api_Ecommerce.Models;

namespace Api_Ecommerce.Data.Dtos
{
    public class ProductoDto
    {
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string ImageUrl { get; set; }

        public double Precio { get; set; }

        public int Stock { get; set; }

        public MarcaDto Marca { get; set; }

        public CategoriaDto Categoria { get; set; }
    }
}
