using Api_Ecommerce.Models;

namespace Api_Ecommerce.Interfaces
{
    public interface IMarcaService
    {
        //Crear marca
        Task<List<Marca>> GetAllMarcas();

        Task CreateMarca(Marca marca);

        Task DeleteMarca(int id, Marca marca);

        Task UpdateMarca(int id, Marca marca);

        Task<List<Producto>> GetProductosPorMarca(int marcaId);

        Task <Marca> GetMarcaById(int id);

        Task<Marca> GetMarcaByName(string marcaName);






    }
}
