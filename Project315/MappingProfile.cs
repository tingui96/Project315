using Entities.Models;
using Entities.DataTransferObject;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<ShoppyCarForCreationDTO, ShoppyCar>();
            CreateMap<ShoppyCarForUpdateDTO, ShoppyCar>();
            CreateMap<ShoppyCar, ShoppyCarWithDetailDTO>();
            //User
            CreateMap<User, UserDTO>();
            CreateMap<User, UserWithShoppyCarDTO>();
            CreateMap<RegisterModel, User>();
            CreateMap<IdentityRole,RoleDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.NormalizedName));
            
        }
    }
}
