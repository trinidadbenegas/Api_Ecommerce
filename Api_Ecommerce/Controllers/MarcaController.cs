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
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController( IMarcaService marcaService)
        {
            _marcaService= marcaService;
            
        }


        [HttpGet]

        public async Task<IActionResult> GetAllMarcas() {
        
           var marcas = await _marcaService.GetAllMarcas();

            return Ok(marcas);
        
        }


        [HttpPost]

        public async Task<IActionResult> CreateMarca(MarcaDto marca)
        {
            if (ModelState.IsValid) { 
            Marca newMarca= new Marca()
            {
                Name = marca.Name,
            };

           await _marcaService.CreateMarca(newMarca);
           return Ok("Marca creada con éxito");
            }
           return BadRequest();
        }

        [HttpDelete]


        public async Task<IActionResult> BorrarMarca(int id)
        {
            Marca marcaBorrar = await _marcaService.GetMarcaById(id);
            await _marcaService.DeleteMarca(id, marcaBorrar);
            return Ok("The product was deleted");
        }

        [HttpPut]

        public async Task<IActionResult> EditarMarca(int id, [FromBody] MarcaDto marca)
        {
            var marcaEditar = new Marca
            {
                Id = id,
                Name = marca.Name,
            };
            await _marcaService.UpdateMarca(id, marcaEditar);
            return Ok("Product updated  sucessfully ");
        }
    }
}
