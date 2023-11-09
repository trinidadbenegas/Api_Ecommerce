using Api_Ecommerce.Data;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Api_Ecommerce.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService( AppDbContext context)
        {
            _context=context;
        }
        public async Task AddProducto(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProducto(int id, Producto producto)
        {
            

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            
        }

        public async Task<List<Producto>> FiltrarProductoByCategoria(string categoriaName)
        {
            var productosPorCategoria = await _context.Productos.Where(p => p.Categoria.Name.Trim().ToUpper() == categoriaName).ToListAsync();
            return productosPorCategoria;
        }

        public async Task<List<Producto>> FiltrarProductosByMarca(int marcaId)
        {
            var productosPorMarca = await _context.Productos.Where(p => p.Marca.Id == marcaId).ToListAsync();
            return productosPorMarca;
        }

        public async Task<Producto> ObtenerProductoById(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
          return await _context.Productos.ToListAsync();

         
        }

        public async Task UpdateProducto(int id, Producto producto)
        {

            if (id == producto.Id)
            {
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
            }
                
        }
    }
}
