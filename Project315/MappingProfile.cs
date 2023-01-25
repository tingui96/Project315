using Entities.Models;
using Entities.DataTransferObject;
using AutoMapper;

namespace Project315
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<Categoria,CategoriaWithProductDTO>();
            CreateMap<CategoriaForCreationDTO, Categoria>();
            CreateMap<CategoriaForUpdateDTO, Categoria>();
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoForCreationDTO,Producto>();
        }
    }
}
