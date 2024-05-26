using Api_Ecommerce.Data;
using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Interfaces;
using Api_Ecommerce.Models;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api_Ecommerce
{
    public class AutoMapperProfile : Profile
    {
        
        private readonly IMarcaService _marcaService;
        private readonly ICategoriaService _categoriaService;

        // Constructor sin parámetros requerido por AutoMapper

        public AutoMapperProfile()
        {
            // Aquí podrías inicializar _context si es necesario
            ConfigureMappings();
        }

        // Constructor que acepta el contexto de la base de datos como parámetro
        public AutoMapperProfile(IMarcaService marcaService, ICategoriaService categoriaService)
        {
            _marcaService= marcaService;
            _categoriaService= categoriaService;
            
            ConfigureMappings();
        }

       
        

        // Método para configurar los mapeos
        private void ConfigureMappings()
        {
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaDto, Categoria>();
            CreateMap<CategoriaDtoId, Categoria>();
            CreateMap<Categoria, CategoriaDtoId>();
            CreateMap<Marca, MarcaDto>();
            CreateMap<MarcaDto, Marca>();
            CreateMap<Marca, MarcaDtoId>();
            CreateMap<MarcaDtoId, Marca>();
            CreateMap<Producto, ProductoDto>()
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => new CategoriaDto { Name = src.Categoria.Name }))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => new MarcaDto { Name = src.Marca.Name }));
            CreateMap<ProductoDto, Producto>()
               .ForMember(dest => dest.Categoria, opt => opt.Ignore())
                .ForMember(dest => dest.Marca, opt => opt.Ignore())
                .AfterMap(async (src, dest) =>
                {
                    // Aquí realizas la lógica para obtener las entidades existentes y asignarlas a dest.Categoria y dest.Marca
                    // Utilizas _context para recuperar las entidades existentes
                    Categoria categoriaExistente = await _categoriaService.GetCategoriaByName(src.Categoria.Name);
                    Marca marcaExistente = await _marcaService.GetMarcaByName(src.Marca.Name);

                    dest.Categoria = categoriaExistente ?? new Categoria { Name = src.Categoria.Name };
                    dest.Marca = marcaExistente ?? new Marca { Name = src.Marca.Name };
                });
        }
    }
}
