using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Data.Static;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly IMapper _mapper;
        private readonly IMarcaService _marcaService;
        private readonly ICategoriaService _categoriaService;

        public ProductoController(IProductoService productoService, IMapper mapper, IMarcaService marcaService,ICategoriaService categoriaService ) 
        {
            _productoService = productoService;
            _marcaService = marcaService;
            _categoriaService = categoriaService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProductos()
        {

            var productos = await _productoService.ObtenerProductos();

            return Ok(productos.Select(producto => _mapper.Map<ProductoDtoRequest>(producto)));

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

            var productoDto = _mapper.Map<ProductoDtoRequest>(producto);
            return Ok(productoDto);
        }
        [HttpGet]
        [Route("Categoria")]

        public async Task<IActionResult> GetProductoporCategoria(string categoriaName)
        {

            var productosPorCategoria = await _productoService.FiltrarProductoByCategoria(categoriaName);

            if(productosPorCategoria == null) { return NotFound(); }

            return Ok(productosPorCategoria.Select(producto => _mapper.Map<ProductoDtoRequest>(producto)));

            
        }

        [HttpGet]
        [Route("Marca")]

        public async Task<IActionResult> GetProductoPorMarca(string marcaName)
        {

            var productosPorMarca = await _productoService.FiltrarProductosByMarca(marcaName);

            if (productosPorMarca == null) { return NotFound(); }

            return Ok(productosPorMarca.Select(producto => _mapper.Map<ProductoDtoRequest>(producto)));

           
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CrearProducto(ProductoDto productoDto)
        {
            if (!ModelState.IsValid || productoDto == null)
            {
                return BadRequest();
            }

            Marca marcaExistente = await _marcaService.GetMarcaByName(productoDto.Marca.Name);
            Categoria categoriaExistente = await _categoriaService.GetCategoriaByName(productoDto.Categoria.Name);


            if (marcaExistente == null || categoriaExistente == null)
            {
                return BadRequest("La Marca o Categoria especificada no existe.");
            }

            Producto productoNuevo = _mapper.Map<Producto>(productoDto);

            productoNuevo.Marca = marcaExistente;
            productoNuevo.Categoria = categoriaExistente;

            await _productoService.AddProducto(productoNuevo);
            return Ok("Producto Creado con èxito");


        }

        [HttpDelete]

        public async Task<IActionResult> EliminarProducto( int productoId)
        {
            Producto productoBorrar = await _productoService.ObtenerProductoById(productoId);
            await _productoService.DeleteProducto(productoId, productoBorrar);
            return Ok("The product was deleted");
        }   

        [HttpPut]
        public async Task<IActionResult> EditarProducto(int productoId,[FromBody] ProductoDto producto)
        {
            var marca = new Marca
            { Name = producto.Marca.Name };
            var categoria = new Categoria
            { Name = producto.Categoria.Name };
            var productoEditado = new Producto
            {
                Id = productoId,
                Nombre= producto.Nombre,
                Descripcion= producto.Descripcion,
                ImageUrl = producto.ImageUrl,
                Precio= producto.Precio,
                Stock = producto.Stock,
                Marca= marca,
                Categoria= categoria,
            };
            await _productoService.UpdateProducto(productoId, productoEditado);
            return Ok("Product updated  sucessfully ");
        }
    }
}
