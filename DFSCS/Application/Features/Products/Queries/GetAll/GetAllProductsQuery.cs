using Application.DTOs;
using MediatR;

namespace Application.Features.Products.Queries.GetAll
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
}
