using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService) 
        {
            _productoService = productoService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {

            var productos = await _productoService.ObtenerProductos();

            return Ok(productos);

        }

        [HttpGet]
        [Route("Producto")]

        public async Task<IActionResult> GetProducto(int id)
        {

            var producto = await _productoService.ObtenerProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }
        [HttpGet]
        [Route("Categoria")]

        public async Task<IActionResult> GetProductoporCategoria(string categoriaName)
        {

            var productosPorCategoria = await _productoService.FiltrarProductoByCategoria(categoriaName);

            if(productosPorCategoria == null) { return NotFound(); }
            
            return Ok(productosPorCategoria);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto(ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (producto == null)
            {
                return BadRequest();
            }

            Marca marcaProducto = new Marca()
            {
                Name = producto.Marca.Name,
            };

            Categoria categoriaProducto = new Categoria()
            {
                Name = producto.Categoria.Name,
            };
            Producto productoNuevo = new Producto()
            {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                ImageUrl = producto.ImageUrl,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Marca = marcaProducto,
                Categoria = categoriaProducto,
            };

            await _productoService.AddProducto(productoNuevo);
            return Ok("Producto Creado con èxito");


        }
    }
}
