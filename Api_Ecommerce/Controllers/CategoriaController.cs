using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;
using Api_Ecommerce.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService= categoriaService;
            _mapper= mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllCategorias()
        {

            var categorias = await _categoriaService.GetAllCategorias();

            return Ok(categorias.Select( categoria=> _mapper.Map<CategoriaDtoId>(categoria)));

        }


        [HttpPost]

        public async Task<IActionResult> CreateCategoria(CategoriaDto categoria)
        {
            if (ModelState.IsValid)
            {
                
                var newCategoria = _mapper.Map<Categoria>(categoria);

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
        
        public async Task< IActionResult> EditarCategoria(int id, [FromBody] CategoriaDtoId categoria)
        {
            //var categoriaEditar = new Categoria
            //{
            //    Id = id,
            //    Name = categoria.Name,
            //};

            var categoriaEditar = _mapper.Map<Categoria>(categoria);
           await _categoriaService.UpdateCategoria(id, categoriaEditar);

            return Ok("Product updated  sucessfully ");

            //si  se ingresa una id que no matchea con alguna id igual sale mje positivo. manejar posibles errores.
        }

    }
}
