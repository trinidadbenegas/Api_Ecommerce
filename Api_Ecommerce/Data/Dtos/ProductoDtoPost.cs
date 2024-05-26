namespace Api_Ecommerce.Data.Dtos
{
    public class ProductoDtoPost
    {
        public string Descripcion { get; set; }

        public string ImageUrl { get; set; }

        public double Precio { get; set; }

        public int Stock { get; set; }

        public int MarcaId { get; set; }

        public int CategoriaId { get; set;}
    }
}
