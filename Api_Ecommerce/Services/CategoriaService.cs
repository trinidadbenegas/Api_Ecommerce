using Api_Ecommerce.Data;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;


namespace Api_Ecommerce.Services
{
    public class CategoriaService : ICategoriaService
    {

        private readonly AppDbContext _context;

        public CategoriaService( AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategoria(Categoria categoria)
        {
           await  _context.Categorias.AddAsync(categoria);
           await  _context.SaveChangesAsync();


        }

        public async Task DeleteCategoria(int id, Categoria categoria)
        {
           
        _context.Remove(categoria);
        await _context.SaveChangesAsync();
        }

        public async Task<List<Categoria>> GetAllCategorias()
        {
           return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetCategoriaById(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<List<Producto>> GetProductosPorCategoria(int categoriaId)
        {
            return await _context.Productos.Where(p => p.Categoria.Id == categoriaId).ToListAsync();
        }

        public async Task UpdateCategoria(int id, Categoria categoria)
        {
           if( id == categoria.Id ) { 
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
            }

        }
    }
}
