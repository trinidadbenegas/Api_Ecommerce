using Api_Ecommerce.Data;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace Api_Ecommerce.Services
{
    public class MarcaService : IMarcaService
    {

        private readonly AppDbContext _context;

        public MarcaService( AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateMarca(Marca marca)
        {
           await  _context.Marcas.AddAsync(marca);
           await  _context.SaveChangesAsync();


        }

        public async Task DeleteMarca(int id, Marca marca)
        {
           
        _context.Remove(marca);
        await _context.SaveChangesAsync();
        }

        public async Task<List<Marca>> GetAllMarcas()
        {
           return await _context.Marcas.ToListAsync();
        }

        public async Task<Marca> GetMarcaById(int id)
        {
            return await _context.Marcas.FindAsync(id);
        }
        public async Task<Marca> GetMarcaByName(string marcaName)
        {
            var marcaNormalized= marcaName.Trim().ToUpper();
            var marcaEncontrada= await _context.Marcas.FirstOrDefaultAsync(m => m.Name.Trim().ToUpper() == marcaNormalized);
            return marcaEncontrada;
        }
        public async Task<List<Producto>> GetProductosPorMarca(int marcaId)
        {
            return await _context.Productos.Where(p => p.Marca.Id == marcaId).ToListAsync();
        }

        public async Task UpdateMarca(int id, Marca marca)
        {
           if( id == marca.Id ) { 
            _context.Marcas.Update(marca);
            await _context.SaveChangesAsync();
            }

        }
    }
}
