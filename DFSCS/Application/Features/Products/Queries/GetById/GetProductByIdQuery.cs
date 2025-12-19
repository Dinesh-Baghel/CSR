using Application.DTOs;
using MediatR;

namespace Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}
