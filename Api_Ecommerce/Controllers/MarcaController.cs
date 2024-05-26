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
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        private readonly IMapper _mapper;

        public MarcaController( IMarcaService marcaService, IMapper mapper)
        {
            _marcaService= marcaService;
            _mapper = mapper;
            
        }


        [HttpGet]

        public async Task<IActionResult> GetAllMarcas() {
        
           var marcas = await _marcaService.GetAllMarcas();

            return Ok(marcas.Select(marca => _mapper.Map<MarcaDtoId>(marca)));

        }


        [HttpPost]

        public async Task<IActionResult> CreateMarca(MarcaDto marcaDto)
        {
            var marcaExistente= _marcaService.GetMarcaByName(marcaDto.Name);

            if (!ModelState.IsValid && marcaExistente != null)
            {

                return BadRequest("No se pudo crear la marca");
               
            }

            Marca newMarca = _mapper.Map<Marca>(marcaDto);

            await _marcaService.CreateMarca(newMarca);
            return Ok("Marca creada con éxito");


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
