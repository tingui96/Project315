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
            //Producto
            CreateMap<Producto, ProductoDTO>();
            CreateMap<ProductoForCreationDTO,Producto>();
            CreateMap<ProductoForUpdateDTO,Producto>();
            //Pedido
            CreateMap<Pedido, PedidoDTO>();
            CreateMap<PedidoForCreationDTO, Pedido>();
            CreateMap<PedidoForUpdateDTO, Pedido>();
            CreateMap<Pedido,PedidoWithProductDTO>();
            //ShoppyCar
            CreateMap<ShoppyCar, ShoppyCarDTO>();
            CreateMap<ShoppyCar, ShoppyCarWithDetailDTO>();

            
        }
    }
}
