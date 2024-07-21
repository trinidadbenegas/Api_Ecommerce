using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService= categoriaService;
            _mapper= mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            try
            {
                var categorias = await _categoriaService.GetAllCategorias();

                return Ok(categorias.Select(categoria => _mapper.Map<CategoriaDtoId>(categoria)));
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request" });
            }

        }


        [HttpPost]  
        public async Task<IActionResult> CreateCategoria(CategoriaDto categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var newCategoria = _mapper.Map<Categoria>(categoria);

                    await _categoriaService.CreateCategoria(newCategoria);
                    return Ok("Category successfully created");
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = $"An error occurred while processing your request" });
            }
            
        }


        [HttpDelete]
        public async Task<IActionResult> BorrarCategoria(int id)
        {
            Categoria categoriaBorrar = await _categoriaService.GetCategoriaById(id);
            if (categoriaBorrar != null)
            {
                await _categoriaService.DeleteCategoria(id, categoriaBorrar);
                return Ok("Category was deleted");
            }
            
            return NotFound(new { message = "Category does not exist" });
        }

   
        [HttpPut]
        public async Task<IActionResult> EditarCategoria(int id, [FromBody] CategoriaDto categoria)
        {

            var categoriaEditar = await _categoriaService.GetCategoriaById(id);
            
            if (categoriaEditar != null)
            {
                
                categoriaEditar.Name = categoria.Name;
                await _categoriaService.UpdateCategoria(id, categoriaEditar);

                return Ok("Category updated sucessfully ");
            }

            return NotFound( new {message = "Category does not exist"});

        }

    }
}
