using Api_Ecommerce.Models;

namespace Api_Ecommerce.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductos();

        Task<Producto> ObtenerProductoById(int id);

        Task AddProducto(Producto producto);

        Task DeleteProducto(int id, Producto producto);

        Task UpdateProducto( int id, Producto producto);

        Task<List<Producto>> FiltrarProductoByCategoria(string categoriaName);

        Task<List<Producto>> FiltrarProductosByMarca(string marcaName);
    }
}
