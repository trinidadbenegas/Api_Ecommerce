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
            CreateMap<MarcaDto, Marca>();
            CreateMap<Producto, ProductoDto>()
               .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
               .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca));
            CreateMap<ProductoDto, Producto>()
               .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria))
               .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca));
            CreateMap<Producto, ProductoDtoRequest>()
               .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.Categoria.Id))
               .ForMember(dest => dest.MarcaId, opt => opt.MapFrom(src => src.Marca.Id));
            CreateMap<ProductoDtoRequest, Producto>()
               .ForPath(dest => dest.Categoria.Id, opt => opt.MapFrom(src => src.CategoriaId))
               .ForPath(dest => dest.Marca.Id, opt => opt.MapFrom(src => src.MarcaId));

            CreateMap<ShoppingCart, ShoppingCartResponseDto>();
            CreateMap<ShoppingItem, ShoppingItemDto>();
            CreateMap<AppUser, ClientDto>();

        }
    }
}
