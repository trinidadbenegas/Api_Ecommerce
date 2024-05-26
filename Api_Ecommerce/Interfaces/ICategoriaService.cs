using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Models;

namespace Api_Ecommerce.Interfaces
{
    public interface ICategoriaService
    {
        //Crear marca
        Task<List<Categoria>> GetAllCategorias();

        Task CreateCategoria(Categoria categoria);

        Task DeleteCategoria(int id, Categoria categoria);

        Task UpdateCategoria(int id,Categoria categoria);

        Task<List<Producto>> GetProductosPorCategoria(int categoriaId);

        Task <Categoria> GetCategoriaById(int id);

        Task<Categoria> GetCategoriaByName(string categoriaName);





    }
}
