using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(int Id, string Name, decimal Price) : IRequest<bool>;
}
