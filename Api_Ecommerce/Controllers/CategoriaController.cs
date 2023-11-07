using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService= categoriaService;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllMarcas()
        {

            var categorias = await _categoriaService.GetAllCategorias();

            return Ok(categorias);

        }


        [HttpPost]

        public async Task<IActionResult> CreateCategoria(CategoriaDto categoria)
        {
            if (ModelState.IsValid)
            {
                Categoria newCategoria = new Categoria()
                {
                    Name = categoria.Name,
                };

                await _categoriaService.CreateCategoria(newCategoria);
                return Ok("Categoria creada con éxito");
            }
            return BadRequest();
        }


        [HttpDelete]
        

        public async Task<IActionResult> BorrarCategoria(int id)
        {
            Categoria categoriaBorrar = await _categoriaService.GetCategoriaById(id);
            await _categoriaService.DeleteCategoria(id, categoriaBorrar);
            return Ok("The product was deleted");
        }

        [HttpPut]
        
        public async Task< IActionResult> EditarCategoria(int id, [FromBody] CategoriaDto categoria)
        {
            var categoriaEditar = new Categoria
            {
                Id = id,
                Name = categoria.Name,
            };
           await _categoriaService.UpdateCategoria(id, categoriaEditar);
            return Ok("Product updated  sucessfully ");
        }

    }
}
