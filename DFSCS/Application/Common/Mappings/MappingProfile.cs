using AutoMapper;
using  Application.DTOs;
using  Application.Features.Products.Commands.CreateProduct;
using  Application.Features.Products.Commands.UpdateProduct;
using  Domain.Entities.Model;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain → DTO
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AnswerList, AnswerListDto>().ReverseMap();
            // Command -> Entity
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateProductCommand, Product>();
        }
    }
}
